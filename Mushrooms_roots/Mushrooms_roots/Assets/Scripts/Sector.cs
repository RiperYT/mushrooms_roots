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

    private int _isUpgrading;
    private long _start_of_upgrading;
    private float _percent_of_upgrading;

    private bool _isChoosed;
    public bool _isColonised;
    private bool _isColonising;

    private long _time_of_colonisation;
    private long _start_of_colonisation;
    private float _percent_of_colonisation;

    private Main _main;

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
        //ToDo
        return _time_upgrade;
    }

    public long GetTimeToEndUpgrade()
    {
        //ToDo
        return 1;
    }

    public bool IsUpgrading()
    {
        //ToDo
        return false;
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

    public void StartColonisatining()
    {
        if (!_isColonised)
        {
            _isColonising = true;
            _start_of_colonisation = _main.TimeNow();
        }
    }
    public float GetInterest()
    {
        return _percent_of_colonisation;
    }

    // Start is called before the first frame update
    void Start()
    {
        _isChoosed = false;
        _isColonising = false;
        _main = Camera.main.GetComponent<Main>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isColonising) Colonising();
    }

    private void Colonising()
    {
        _percent_of_colonisation = (_main.TimeNow() - _start_of_colonisation) / _time_of_colonisation * 100;
        if (_percent_of_colonisation >= 100)
        {
            _isColonised = true;
            _isColonising = false;
            _main.AddMushrooms(_mushrooms_present);
            _main.AddMushroomsMax(_mushrooms_max);
        }
    }
}