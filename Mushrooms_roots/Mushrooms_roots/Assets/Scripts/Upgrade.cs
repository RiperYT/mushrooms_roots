using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    
    private bool _isActive;
    private Main _main;
    private Upgrade upgrade;

    public GameObject before;
    public int waterNeed, foodNeed, energyNeed;
    public float waterX, foodX, energyX;

    public float GetFoodX()
    {
        return foodX;
    }

    public float GetWaterX()
    {
        return waterX;
    }

    public float GetEnergyX()
    {
        return energyX;
    }

    public int GetFoodNeed()
    {
        return foodNeed;
    }

    public int GetWaterNeed()
    {
        return waterNeed;
    }

    public int GetEnergyNeed()
    {
        return energyNeed;
    }

    public void Activate()
    {
        if (_main.GetEnergy() >= energyNeed && _main.GetWater() >= waterNeed && _main.GetFood() >= foodNeed)
        {
            _isActive = true;
            _main.MultiplyEnergyX(energyX);
            _main.MultiplyFoodX(foodX);
            _main.MultiplyWaterX(waterX);
        }
    }

    public bool IsActive()
    {
        return _isActive;
    }

    public bool CanBeOpened()
    {
        if (before == null)
        {
            return true;
        }
        else
        {
            return upgrade.IsActive();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _main = Camera.main.GetComponent<Main>();

        if (before == null)
        {
            upgrade = before.GetComponent<Upgrade>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
