using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    private float _water;
    private int _water_produce;
    private float _water_produce_x;
    private int _water_costs;

    private float _food;
    private int _food_produce;
    private float _food_produce_x;
    private int _food_costs;

    private float _energy;
    private int _energy_produce;
    private float _energy_produce_x;
    private int _energy_costs;

    private int _mushrooms;
    private int _mushrooms_max;

    private long _ticks_from_start;
    private long _last_ticks;
    private bool _pause;
    private int _speed;

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
        return _water_costs;
    }

    public int GetWaterProduce()
    {
        return _water_produce;
    }

    public void MultiplyWaterX(float k)
    {
        _water_produce_x *= k;
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
        return _food_costs;
    }

    public int GetFoodProduce()
    {
        return _food_produce;
    }

    public void MultiplyFoodX(float k)
    {
        _food_produce_x *= k;
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
        return _energy_costs;
    }

    public int GetEnergyProduce()
    {
        return _energy_produce;
    }

    public void MultiplyEnergyX(float k)
    {
        _energy_produce_x *= k;
    }

    public void AddEnergyCosts(int k)
    {
        _energy_costs += k;
    }

    public void AddEnergyProduce(int k)
    {
        _energy_produce += k;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _pause = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_pause)
        {
            UpdateTicks();
        }
    }

    private void UpdateTicks()
    {
        var now = DateTime.Now.Ticks;
        _last_ticks = now;
        _ticks_from_start = _ticks_from_start + (now - _last_ticks) * _speed;
    }
}