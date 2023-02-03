using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    private int _water;
    private int _water_produce;
    private int _water_costs;
    private int _food;
    private int _food_produce;
    private int _food_costs;
    private int _energy;
    private int _energy_produce;
    private int _energy_costs;
    private int _mushrooms;
    private int _mushrooms_max;

    private long _ticks_from_start;
    private long _last_ticks;
    private bool _pause;

    public void StartPause()
    {
        _pause = true;
    }

    public void EndPause()
    {
        _pause = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        _pause = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_pause) UpdateTicks();
    }

    private void UpdateTicks()
    {
        var now = DateTime.Now.Ticks;
        _last_ticks = now;
    }
}
