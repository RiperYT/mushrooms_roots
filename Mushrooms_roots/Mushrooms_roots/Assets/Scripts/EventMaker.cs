using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventMaker : MonoBehaviour
{
    public int minSnow, maxSnow, minSummer, maxSummer;
    public GameObject summer, winter;

    private int number;
    private Main _main;
    private List<Event> events = new List<Event>();

    public List<Event> GetListEvents()
    {
        return events;
    }

    // Start is called before the first frame update
    void Start()
    {
        _main = Camera.main.GetComponent<Main>();
        number = 0;
        for(int i = 1; i <= 100; i++)
        {
            var k = new System.Random().Next(minSummer, maxSummer + 1);
            for (int j = 0; j < k; j++)
                events.Add(summer.GetComponent<Event>());
            k = new System.Random().Next(minSnow, maxSnow + 1);
            for (int j = 0; j < k; j++)
                events.Add(winter.GetComponent<Event>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_main.TimeNow() / (_main.GetTimeS() * 10000000) > number)
        {
            //_main.AddEnergyCosts(-events[number].GetEnergyCost());
            //_main.AddWaterCosts(-events[number].GetWaterCost());
            //_main.AddFoodCosts(-events[number].GetFoodCost());

            _main.energyCost = events[number].GetEnergyCost();
            _main.waterCost = events[number].GetWaterCost();
            _main.foodCost = events[number].GetFoodCost();

            number++;

            //_main.AddEnergyCosts(events[number].GetEnergyCost());
            //_main.AddWaterCosts(events[number].GetWaterCost());
            //_main.AddFoodCosts(events[number].GetFoodCost());

            if (events[number].Name().Equals("Summer")) _main.isSummer = true;
            if (events[number].Name().Equals("Winter")) _main.isSummer = false;
        }
    }
}
