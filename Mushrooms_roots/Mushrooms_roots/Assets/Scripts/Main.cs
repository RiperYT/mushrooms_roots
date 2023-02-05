using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public float _water;
    public int _water_produce;
    public float _water_produce_x;
    public int _water_costs;

    public float _food;
    public int _food_produce;
    public float _food_produce_x;
    public int _food_costs;

    public float _energy;
    public int _energy_produce;
    public float _energy_produce_x;
    public int _energy_costs;

    public int _mushrooms;
    public int _mushrooms_max;

    public long _ticks_from_update;
    public long _ticks_from_start;
    public long _last_ticks;
    public bool _pause;
    public int _speed;

    public int timeS;

    public bool isSummer;
    public int foodCost;
    public int waterCost;
    public int energyCost;

    public bool _lastIsSummer;
    public int _dopFoodCost;
    public int _dopWaterCost;
    public int _dopEnergyCost;

    public int sizeX, sizeZ;
    private GameObject[,] listSector;

    public List<GameObject> green;
    public List<GameObject> blue;

    public void StartPause()
    {
        _pause = true;
    }

    public void EndPause()
    {
        _last_ticks = DateTime.Now.Ticks;
        _pause = false;
    }

    public bool IsPaused()
    {
        return _pause;
    }

    public void SetSpeed(int x)
    {
        _speed = x;
    }

    public long TimeNow()
    {
        return _ticks_from_start;
    }

    public void SetVolumeSound(float k)
    {
        //ToDo
    }

    public int GetMushrooms()
    {
        return _mushrooms;
    }
    public int GetMushroomsMax()
    {
        return _mushrooms_max;
    }

    public void AddMushrooms(int k)
    {
        _mushrooms += k;
    }

    public void AddMushroomsMax(int k)
    {
        _mushrooms_max += k;
    }

    public float GetWater()
    {
        return _water;
    }

    public float GetWaterX()
    {
        return _water_produce_x;
    }

    public int GetWaterCosts()
    {
        return _water_costs - waterCost * _mushrooms;
    }

    public int GetWaterProduce()
    {
        return _water_produce;
    }

    public void MultiplyWaterX(float k)
    {
        _water_produce_x *= k;
    }

    public void AddWater(int k)
    {
        _water += k;
    }

    public void AddWaterCosts(int k)
    {
        _water_costs += k;
    }

    public void AddWaterProduce(int k)
    {
        _water_produce += k;
    }

    public float GetFood()
    {
        return _food;
    }

    public float GetFoodX()
    {
        return _food_produce_x;
    }

    public int GetFoodCosts()
    {
        return _food_costs - foodCost * _mushrooms;
    }

    public int GetFoodProduce()
    {
        return _food_produce;
    }

    public void MultiplyFoodX(float k)
    {
        _food_produce_x *= k;
    }

    public void AddFood(int k)
    {
        _food += k;
    }

    public void AddFoodCosts(int k)
    {
        _food_costs += k;
    }

    public void AddFoodProduce(int k)
    {
        _food_produce += k;
    }

    public float GetEnergy()
    {
        return _energy;
    }

    public float GetEnergyX()
    {
        return _energy_produce_x;
    }

    public int GetEnergyCosts()
    {
        return _energy_costs - energyCost * _mushrooms;
    }

    public int GetEnergyProduce()
    {
        return _energy_produce;
    }

    public void MultiplyEnergyX(float k)
    {
        _energy_produce_x *= k;
    }

    public void AddEnergy(int k)
    {
        _energy += k;
    }

    public void AddEnergyCosts(int k)
    {
        _energy_costs += k;
    }

    public void AddEnergyProduce(int k)
    {
        _energy_produce += k;
    }

    public int GetTimeS()
    {
        return timeS;
    }

    // Start is called before the first frame update
    void Start()
    {
        _water_produce_x = 1;
        _food_produce_x = 1;
        _energy_produce_x = 1;
        _speed = 1;
        _pause = false;
        _ticks_from_start = 0;
        _ticks_from_update = 0;
        _last_ticks = DateTime.Now.Ticks;
        listSector = new GameObject[sizeZ,sizeX];
        CreateWorld();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_pause)
        {
            UpdateTicks();
            Mathemetic();
            CheckEnd();
        }
    }

    private void ChangeWeather()
    {
        if (_lastIsSummer != isSummer)
        {

        }
    }

    private void UpdateTicks()
    {
        var now = DateTime.Now.Ticks;
        _ticks_from_start = _ticks_from_start + (now - _last_ticks) * _speed;
        _ticks_from_update = _ticks_from_update + (now - _last_ticks) * _speed;
        _last_ticks = now;
    }

    private void Mathemetic()
    {
        if (_ticks_from_update >= timeS * 10000000)
        {
            _water = _water + _water_produce * _water_produce_x - _water_costs + waterCost * _mushrooms;
            _food = _food + _food_produce * _food_produce_x - _food_costs + foodCost * _mushrooms;
            _energy = _energy + _energy_produce * _energy_produce_x - _energy_costs + energyCost * _mushrooms;
            _ticks_from_update = 0;
        }
    }

    private void CheckEnd()
    {
        if (_energy < 0)
            Debug.Log("End");
    }

    private void CreateWorld()
    {
        Debug.Log(listSector[2, 2] == null);
        var centalZ = sizeZ / 2;
        var centalX = sizeX / 2;
        for (int i = 0; i < sizeZ; i++)
            for (int j = 0; j < sizeX; j++)
                if (listSector[i, j] == null)
                {
                    var random = new System.Random();
                    var r = random.Next(1, 3);
                    if (r == 1 || !isNearWater(i, j))
                    {
                        listSector[i, j] = Instantiate(green[random.Next(0, green.Count)], new Vector3(6.4f * j, 0, -6.4f * i), new Quaternion(0,0,0,0)) as GameObject;
                        var sec = listSector[i, j].GetComponent<Sector>();
                        sec._water = random.Next(1, 3);
                        sec._food = random.Next(3, 5);
                        sec._mushrooms_present = random.Next(1, 3);
                        sec._mushrooms_max = 5;

                        sec._isColonised = false;
                        sec.SetNear(false);

                        sec._time_of_colonisation = (int)Math.Round(Math.Sqrt(Math.Abs(centalZ - i) ^ 2 + Math.Abs(centalX - j) ^ 2)) * 10;
                        sec._energy_for_colonise = (int) Math.Round(Math.Sqrt(Math.Abs(centalZ - i) ^ 2 + Math.Abs(centalX - j) ^ 2)) * 10;

                        sec._energy_start = (int)Math.Round(Math.Sqrt(Math.Abs(centalZ - i) ^ 2 + Math.Abs(centalX - j) ^ 2)) * 5;
                        sec._energy_upgrade = 5;
                        sec._time_start = (int)Math.Round(Math.Sqrt(Math.Abs(centalZ - i) ^ 2 + Math.Abs(centalX - j) ^ 2)) * 5;
                        sec._time_upgrade = 5;
                    }
                    else
                    {
                        listSector[i, j] = Instantiate(blue[random.Next(0, blue.Count)], new Vector3(6.4f * j, 0, -6.4f * i), new Quaternion(0, 0, 0, 0)) as GameObject;
                        var sec = listSector[i, j].GetComponent<Sector>();
                        sec._water = random.Next(3, 5);
                        sec._food = random.Next(1, 3);
                        sec._mushrooms_present = random.Next(1, 3);
                        sec._mushrooms_max = 5;

                        sec._isColonised = false;
                        sec.SetNear(false);

                        sec._time_of_colonisation = (int)Math.Round(Math.Sqrt(Math.Abs(centalZ - i) ^ 2 + Math.Abs(centalX - j) ^ 2)) * 10;
                        sec._energy_for_colonise = (int)Math.Round(Math.Sqrt(Math.Abs(centalZ - i) ^ 2 + Math.Abs(centalX - j) ^ 2)) * 10;

                        sec._energy_start = (int)Math.Round(Math.Sqrt(Math.Abs(centalZ - i) ^ 2 + Math.Abs(centalX - j) ^ 2)) * 5;
                        sec._energy_upgrade = 5;
                        sec._time_start = (int)Math.Round(Math.Sqrt(Math.Abs(centalZ - i) ^ 2 + Math.Abs(centalX - j) ^ 2)) * 5;
                        sec._time_upgrade = 5;

                        if (i + 1 < sizeZ)
                        {
                            listSector[i + 1, j] = Instantiate(blue[random.Next(0, blue.Count)], new Vector3(6.4f * j, 0, -6.4f * (i + 1)), new Quaternion(0, -90, 0, 0)) as GameObject;
                            var sec1 = listSector[i + 1, j].GetComponent<Sector>();
                            sec1._water = random.Next(3, 5);
                            sec1._food = random.Next(1, 3);
                            sec1._mushrooms_present = random.Next(1, 3);
                            sec1._mushrooms_max = 5;
                               
                            sec1._isColonised = false;
                            sec1.SetNear(false);
                               
                            sec1._time_of_colonisation = (int)Math.Round(Math.Sqrt(Math.Abs(centalZ - i) ^ 2 + Math.Abs(centalX - j) ^ 2)) * 10;
                            sec1._energy_for_colonise = (int)Math.Round(Math.Sqrt(Math.Abs(centalZ - i) ^ 2 + Math.Abs(centalX - j) ^ 2)) * 10;
                               
                            sec1._energy_start = (int)Math.Round(Math.Sqrt(Math.Abs(centalZ - i) ^ 2 + Math.Abs(centalX - j) ^ 2)) * 5;
                            sec1._energy_upgrade = 5;
                            sec1._time_start = (int)Math.Round(Math.Sqrt(Math.Abs(centalZ - i) ^ 2 + Math.Abs(centalX - j) ^ 2)) * 5;
                            sec1._time_upgrade = 5;
                        }
                        if (j + 1 < sizeX)
                        {
                            listSector[i, j + 1] = Instantiate(blue[random.Next(0, blue.Count)], new Vector3(6.4f * (j + 1), 0, -6.4f * i), new Quaternion(0, 90, 0, 0)) as GameObject;
                            var sec1 = listSector[i, j + 1].GetComponent<Sector>();
                            sec1._water = random.Next(3, 5);
                            sec1._food = random.Next(1, 3);
                            sec1._mushrooms_present = random.Next(1, 3);
                            sec1._mushrooms_max = 5;

                            sec1._isColonised = false;
                            sec1.SetNear(false);

                            sec1._time_of_colonisation = (int)Math.Round(Math.Sqrt(Math.Abs(centalZ - i) ^ 2 + Math.Abs(centalX - j) ^ 2)) * 10;
                            sec1._energy_for_colonise = (int)Math.Round(Math.Sqrt(Math.Abs(centalZ - i) ^ 2 + Math.Abs(centalX - j) ^ 2)) * 10;

                            sec1._energy_start = (int)Math.Round(Math.Sqrt(Math.Abs(centalZ - i) ^ 2 + Math.Abs(centalX - j) ^ 2)) * 5;
                            sec1._energy_upgrade = 5;
                            sec1._time_start = (int)Math.Round(Math.Sqrt(Math.Abs(centalZ - i) ^ 2 + Math.Abs(centalX - j) ^ 2)) * 5;
                            sec1._time_upgrade = 5;
                        }
                        if (j + 1 < sizeX && i + 1 < sizeZ)
                        {
                            listSector[i + 1, j + 1] = Instantiate(blue[random.Next(0, blue.Count)], new Vector3(6.4f * (j + 1), 0, -6.4f * (i + 1)), new Quaternion(0, 180, 0, 0)) as GameObject;
                            var sec1 = listSector[i + 1, j + 1].GetComponent<Sector>();
                            sec1._water = random.Next(3, 5);
                            sec1._food = random.Next(1, 3);
                            sec1._mushrooms_present = random.Next(1, 3);
                            sec1._mushrooms_max = 5;

                            sec1._isColonised = false;
                            sec1.SetNear(false);

                            sec1._time_of_colonisation = (int)Math.Round(Math.Sqrt(Math.Abs(centalZ - i) ^ 2 + Math.Abs(centalX - j) ^ 2)) * 10;
                            sec1._energy_for_colonise = (int)Math.Round(Math.Sqrt(Math.Abs(centalZ - i) ^ 2 + Math.Abs(centalX - j) ^ 2)) * 10;

                            sec1._energy_start = (int)Math.Round(Math.Sqrt(Math.Abs(centalZ - i) ^ 2 + Math.Abs(centalX - j) ^ 2)) * 5;
                            sec1._energy_upgrade = 5;
                            sec1._time_start = (int)Math.Round(Math.Sqrt(Math.Abs(centalZ - i) ^ 2 + Math.Abs(centalX - j) ^ 2)) * 5;
                            sec1._time_upgrade = 5;
                        }
                    }
                }

        listSector[centalZ, centalX].GetComponent<Sector>()._isColonised = true;
        listSector[centalZ, centalX].GetComponent<Sector>().SetNear(true);
        listSector[centalZ+1, centalX+1].GetComponent<Sector>().SetNear(true);
        listSector[centalZ+1, centalX-1].GetComponent<Sector>().SetNear(true);
        listSector[centalZ-1, centalX+1].GetComponent<Sector>().SetNear(true);
        listSector[centalZ-1, centalX-1].GetComponent<Sector>().SetNear(true);
        listSector[centalZ + 1, centalX].GetComponent<Sector>().SetNear(true);
        listSector[centalZ - 1, centalX].GetComponent<Sector>().SetNear(true);
        listSector[centalZ, centalX - 1].GetComponent<Sector>().SetNear(true);
        listSector[centalZ, centalX - 1].GetComponent<Sector>().SetNear(true);
    }
    private bool isNearWater(int i, int j)
    {
        var p = true;

        if (i > 0 && j > 0) if (listSector[i - 1, j - 1] != null) if (listSector[i - 1, j - 1].GetComponent<Sector>().GetWater() > 2) p = false;
        if (i > 0) if (listSector[i - 1, j] != null) if (listSector[i - 1, j].GetComponent<Sector>().GetWater() > 2) p = false;
        if (i > 0 && j < sizeX - 1) if (listSector[i - 1, j + 1] != null) if (listSector[i - 1, j + 1].GetComponent<Sector>().GetWater() > 2) p = false;
        if (j < sizeX - 1) if (listSector[i, j + 1] != null) if (listSector[i, j + 1].GetComponent<Sector>().GetWater() > 2) p = false;
        if (i < sizeZ - 1 && j < sizeX - 1) if (listSector[i + 1, j + 1] != null) if (listSector[i + 1, j + 1].GetComponent<Sector>().GetWater() > 2) p = false;
        if (i < sizeZ - 1) if (listSector[i + 1, j] != null) if (listSector[i + 1, j].GetComponent<Sector>().GetWater() > 2) p = false;
        if (i < sizeZ - 1 && j > 0) if (listSector[i + 1, j - 1] != null) if (listSector[i + 1, j - 1].GetComponent<Sector>().GetWater() > 2) p = false;
        if (j > 0) if (listSector[i, j - 1] != null) if (listSector[i, j - 1].GetComponent<Sector>().GetWater() > 2) p = false;

        return p;
    }
}