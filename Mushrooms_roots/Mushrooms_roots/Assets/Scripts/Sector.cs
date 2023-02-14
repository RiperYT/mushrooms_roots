using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sector : MonoBehaviour
{
    public int _water;
    public int _food;
    public int _mushrooms_present;
    public int _mushrooms_max;
    public int _energy_for_colonise;

    public int _energy_start;
    public int _energy_upgrade;

    public int _time_start;
    public int _time_upgrade;

    private bool _isNear;
    public bool _isUpgrading;
    private long _start_of_upgrading;

    private bool _isChoosed;
    public bool _isColonised;
    public bool _isColonising;

    public int _time_of_colonisation;
    private long _start_of_colonisation;
    private float _percent_of_colonisation;

    public List<GameObject> mushrooms;
    public GameObject winter;
    public GameObject summer;

    private Main _main;
    private bool _isSummer;

    public int i, j;
    public bool IsNear()
    {
        return _isNear;
    }
    public void SetNear(bool p)
    {
        _isNear = p;
    }

    public int GetWater()
    {
        return _water;
    }

    public int GetFood()
    {
        return _food;
    }

    public int GetMushroomsPresent()
    {
        return _mushrooms_present;
    }

    public int GetMushroomsMax()
    {
        return _mushrooms_max;
    }

    public int GetEnergyForColonise()
    {
        return _energy_for_colonise;
    }

    public int GetTimeUpgrade()
    {
        if (_mushrooms_present < _mushrooms_max)
        {
            return _time_start + _time_upgrade * (_mushrooms_present - 1);
        }
        else
        {
            throw new Exception("Cannot be more mushrooms in sector");
        }
    }
    public int GetEnergyUpgrade()
    {
        if (_mushrooms_present < _mushrooms_max)
        {
            return _energy_start + _energy_upgrade * (_mushrooms_present - 1);
        }
        else
        {
            throw new Exception("Cannot be more mushrooms in sector");
        }
    }

    public int GetTimeToEndUpgrade()
    {
        if (_isUpgrading)
        {
            return Convert.ToInt32((_start_of_upgrading + (_time_start + _time_upgrade * (_mushrooms_present - 1)) * 10000000 - _main.TimeNow()) / 10000000);
        }
        else
        {
            throw new Exception("Is not upgrading");
        }
    }

    public bool IsUpgrading()
    {
        return _isUpgrading;
    }
    public bool IsColonising()
    {
        return _isColonising;
    }

    public void StartUpgrading()
    {
        Debug.Log("Sector");
        if (_main.GetEnergy() >= _energy_start + _energy_upgrade * (_mushrooms_present - 1) && !_isUpgrading && _mushrooms_max > _mushrooms_present)
        {
            _main.AddEnergy(-_energy_start + _energy_upgrade * (_mushrooms_present - 1));
            _isUpgrading = true;
            _start_of_upgrading = _main.TimeNow();
        }
        else
        {
            throw new Exception("Low energy");
        }
    }

    public void Choose()
    {
        if (!_isChoosed)
        {
            _isChoosed = true;
            transform.Translate(0, 0.5f, 0);
        }
    }

    public void EndChoose()
    {
        if (_isChoosed)
        {
            _isChoosed = false;
            transform.Translate(0, -0.5f, 0);
        }

    }

    public int GetTimeToColonise()
    {
        return _time_of_colonisation;
    }

    public void StartColonisatining()
    {
        if (!_isColonised && _main.GetEnergy() >= _energy_for_colonise && !_isColonising)
        {
            _main.AddEnergy(-_energy_for_colonise);
            _isColonising = true;
            _start_of_colonisation = _main.TimeNow();
        }
        else
        {
            throw new Exception("Is not upgrading");
        }
    }
    public float GetInterest()
    {
        return _percent_of_colonisation;
    }
    public int GetTimeToEndColonise()
    {
        if (_isColonising)
        {
            return Convert.ToInt32((_start_of_colonisation + _time_of_colonisation * 10000000 - _main.TimeNow()) / 10000000);
        }
        else
        {
            throw new Exception("Is not colonising");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _isUpgrading = false;
        _isChoosed = false;
        _isColonising = false;
        _main = Camera.main.GetComponent<Main>();

        if (_isColonised)
        {
            for(int n = 0; n < _mushrooms_present; n++)
            {
                var p = true;
                while (p)
                {
                    var mush = mushrooms[new System.Random().Next(0, mushrooms.Count)];
                    if (mush.active == false)
                    {
                        mush.active = true;
                        p = false;
                    }
                }
            }
            _main.AddMushrooms(_mushrooms_present);
            _main.AddMushroomsMax(_mushrooms_max);

            _main.AddFoodCosts(_mushrooms_present);
            _main.AddWaterCosts(_mushrooms_present);
            _main.AddEnergyCosts(_mushrooms_present);

            _main.AddEnergyProduce(_mushrooms_present * 2);
            _main.AddFoodProduce(_mushrooms_present * _food);
            _main.AddWaterProduce(_mushrooms_present * _water);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_isSummer && !_main.isSummer) { _isSummer = false; SetWinter(); }
        if (!_isSummer && _main.isSummer) { _isSummer = true; SetSummer(); }
        if (_isColonising) Colonising();
        if (_isUpgrading) Upgrading();
    }
    private void SetWinter()
    {
        summer.active = false;
        winter.active = true;
    }

    private void SetSummer()
    {
        summer.active = true;
        winter.active = false;
    }

    private void Colonising()
    {
        Debug.Log("Colonising");
        _percent_of_colonisation = (_main.TimeNow() - _start_of_colonisation) / (_time_of_colonisation * 10000000) * 100;
        if (_percent_of_colonisation >= 100)
        {
            _main.OpenNear(i, j);

            _isColonised = true;
            _isColonising = false;
            _main.AddMushrooms(_mushrooms_present);
            _main.AddMushroomsMax(_mushrooms_max);

            _main.AddFoodCosts(_mushrooms_present);
            _main.AddWaterCosts(_mushrooms_present);
            _main.AddEnergyCosts(_mushrooms_present);

            _main.AddEnergyProduce(_mushrooms_present * 2);
            _main.AddFoodProduce(_mushrooms_present * _food);
            _main.AddWaterProduce(_mushrooms_present * _water);

            for (int n = 0; n < _mushrooms_present; n++)
            {
                var p = true;
                while (p)
                {
                    var mush = mushrooms[new System.Random().Next(0, mushrooms.Count)];
                    if (mush.active == false)
                    {
                        mush.active = true;
                        p = false;
                    }
                }
            }
        }
    }

    private void Upgrading()
    {
        if (_main.TimeNow() - _start_of_upgrading >= (_time_start + _time_upgrade * (_mushrooms_present - 1)) * 10000000)
        {
            _isUpgrading = false;
            _mushrooms_present += 1;

            _main.AddMushrooms(1);
            
            _main.AddFoodCosts(1);
            _main.AddWaterCosts(1);
            _main.AddEnergyCosts(1);

            _main.AddEnergyProduce(2);
            _main.AddFoodProduce(_food);
            _main.AddWaterProduce(_water);

            var p = true;
            while (p)
            {
                var mush = mushrooms[new System.Random().Next(0, mushrooms.Count)];
                if (mush.active == false)
                {
                    mush.active = true;
                    p = false;
                }
            }
        }
    }
}