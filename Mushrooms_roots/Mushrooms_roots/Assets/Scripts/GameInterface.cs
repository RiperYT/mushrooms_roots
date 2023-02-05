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

    // Start is called before the first frame update
    void Start()
    {
        energyField = GameObject.Find("EnergyField").GetComponent<Text>();
        foodField = GameObject.Find("FoodField").GetComponent<Text>();
        waterField = GameObject.Find("WaterField").GetComponent<Text>();
        mushroomsField = GameObject.Find("MushroomsField").GetComponent<Text>();
        speedField = GameObject.Find("SpeedField").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Test()
    {
        UpdateResourcesInfo(1, 2, 3, 4);
        UpdateSpeedInfo(4);
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

    public void UpdateSpeedInfo(int speed)
    {
        speedMultiplier = speed;

        Debug.Log(speedMultiplier + "");

        UpdateSpeedField();
    }

    private void UpdateResourcesFields()
    { 
        energyField.text = energyAmount + "x";
        foodField.text = foodAmount + "x";
        waterField.text = waterAmount + "x";
        mushroomsField.text = mushroomsNumber + "x";
    }

    private void UpdateSpeedField()
    { 
        speedField.text = speedMultiplier + "x";
    }
}
