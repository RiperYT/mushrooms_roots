using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameInterface : MonoBehaviour
{
    Text energyField;
    Text foodField;
    Text waterField;
    Text mushroomsField;
    Text speedField;
    Text requiredResourcesField;

    public GameObject[] foodIcons;
    public GameObject[] noFoodIcons;
    public GameObject[] waterIcons;
    public GameObject[] noWaterIcons;
    public GameObject[] mushroomsIcons;
    public GameObject[] noMushroomsIcons;

    public GameObject ExtraInterface;

    int energyAmount;
    int foodAmount;
    int waterAmount;
    int mushroomsNumber;
    int speedMultiplier;
    int requiredEnergyAmount;
    int requiredMushroomsNumber;
    int extractedFoodAmount;
    int extractedWaterAmount;
    int extractedMushroomsNumber;

    private Main _main;

    // Start is called before the first frame update
    void Start()
    {
        speedMultiplier = 1;
        energyField = GameObject.Find("EnergyField").GetComponent<Text>();
        foodField = GameObject.Find("FoodField").GetComponent<Text>();
        waterField = GameObject.Find("WaterField").GetComponent<Text>();
        mushroomsField = GameObject.Find("MushroomsField").GetComponent<Text>();
        speedField = GameObject.Find("SpeedField").GetComponent<Text>();
        _main = Camera.main.GetComponent<Main>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateResourcesFields();
    }

    public void Test()
    {
        UpdateResourcesInfo(1, 2, 3, 4);
        UpdateSpeedInfo();
    }

    public void Open(Sector sector)
    {
        ExtraInterface.active = true;
        UpdateExtractedResourcesInfo(sector.GetFood(), sector.GetWater(), sector.GetMushroomsPresent());
    }
    public void Close()
    {
        ExtraInterface.active = false;
    }

    public void UpdateExtractedResourcesInfo(int food, int water, int mushrooms)
    {
        for (int i = 0; i < 5; i++)
        {
            foodIcons[i].SetActive(false);
            waterIcons[i].SetActive(false);
            mushroomsIcons[i].SetActive(false);

            noFoodIcons[i].SetActive(true);
            noWaterIcons[i].SetActive(true);
            noMushroomsIcons[i].SetActive(true);
        }

        for (int i = 0; i < food; i++)
        {
            foodIcons[i].SetActive(true);
            noFoodIcons[i].SetActive(false);
        }

        for (int i = 0; i < water; i++)
        {
            waterIcons[i].SetActive(true);
            noWaterIcons[i].SetActive(false);
        }

        for (int i = 0; i < mushrooms; i++)
        {
            mushroomsIcons[i].SetActive(true);
            noMushroomsIcons[i].SetActive(false);
        }
    }

    public void UpdateResourcesInfo(int energy, int food, int water, int mushrooms)
    {
        energyAmount = energy;
        foodAmount = food;
        waterAmount = water;
        mushroomsNumber = mushrooms;

        Debug.Log(energyAmount + " " + foodAmount + " " + waterAmount + " " + mushroomsNumber);

        UpdateResourcesFields();
    }

    public void UpdateSpeedInfo()
    {
        if (speedMultiplier == 1) speedMultiplier = 2;
        else if (speedMultiplier == 2) speedMultiplier = 4;
        else if (speedMultiplier == 4) speedMultiplier = 0;
        else if (speedMultiplier == 0) speedMultiplier = 1;

        Debug.Log(speedMultiplier + "");

        UpdateSpeedField();
    }

    private void UpdateResourcesFields()
    { 
        energyField.text = _main.GetEnergy().ToString() + "|" + _main.GetEnergyProduce().ToString() + "x" + _main.GetEnergyX().ToString() + "-" + _main.GetEnergyCosts().ToString();
        foodField.text = _main.GetFood().ToString() + "|" + _main.GetFoodProduce().ToString() + "x" + _main.GetFoodX().ToString() + "-" + _main.GetFoodCosts().ToString();
        waterField.text = _main.GetWater().ToString() + "|" + _main.GetWaterProduce().ToString() + "x" + _main.GetWaterX().ToString() + "-" + _main.GetWaterCosts().ToString();
        mushroomsField.text = _main.GetMushrooms().ToString() + "/" + _main.GetMushroomsMax().ToString();
    }

    private void UpdateSpeedField()
    {
        _main.SetSpeed(speedMultiplier);
        speedField.text = "x" + speedMultiplier;
    }
}
