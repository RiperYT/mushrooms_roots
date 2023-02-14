using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event : MonoBehaviour
{
    public string name;
    public int waterCost, foodCost, EnergyCost;
    public string Name()
    {
        return name;
    }
    public int GetWaterCost()
    {
        return waterCost;
    }

    public int GetFoodCost()
    {
        //ToDo
        return foodCost;
    }

    public int GetEnergyCost()
    {
        //ToDo
        return EnergyCost;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
