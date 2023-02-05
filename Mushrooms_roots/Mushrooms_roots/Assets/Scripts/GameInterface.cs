using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class GameInterface : MonoBehaviour
{
    public GameObject[] foodIcons;
    public GameObject[] noFoodIcons;
    public GameObject[] waterIcons;
    public GameObject[] noWaterIcons;
    public GameObject[] mushroomsIcons;
    public GameObject[] noMushroomsIcons;

    public GameObject requiredEnergyIcon;
    public GameObject extraMushroomsIcon;

    public Text interactionField1;
    public Text interactionField2;
    public Text interactionField3;
    public Text interactionField4;

    Text energyField;
    Text foodField;
    Text waterField;
    Text mushroomsField;
    Text speedField;
    Text requiredEnergyField;
    Text requiredMushroomsField;

    int energyAmount;
    int foodAmount;
    int waterAmount;
    int mushroomsNumber;
    int speedMultiplier;
    int requiredEnergyAmount;
    int requiredMushroomsNumber;

    // Start is called before the first frame update
    void Start()
    {
        energyField = GameObject.Find("EnergyField").GetComponent<Text>();
        foodField = GameObject.Find("FoodField").GetComponent<Text>();
        waterField = GameObject.Find("WaterField").GetComponent<Text>();
        mushroomsField = GameObject.Find("MushroomsField").GetComponent<Text>();
        speedField = GameObject.Find("SpeedField").GetComponent<Text>();
        requiredEnergyField = GameObject.Find("RequiredEnergyField").GetComponent<Text>();
        requiredMushroomsField = GameObject.Find("RequiredMushroomsField").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Test()
    {
        UpdateExtractedResourcesInfo(Random.Range(0,6), Random.Range(0, 6), Random.Range(0, 6));

        UpgradeTheZone(123, 345, 1);
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

    public void UpdateRequirementInfo(int energy, int mushrooms)
    {
        requiredEnergyAmount = energy;
        requiredMushroomsNumber = mushrooms;

        UpdateRequirementField();
    }

    public void UpdateSpeedInfo(int speed)
    {
        speedMultiplier = speed;

        Debug.Log(speedMultiplier + "");

        UpdateSpeedField();
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

    public void NoContactZone()
    {
        extraMushroomsIcon.SetActive(false);
        requiredEnergyIcon.SetActive(false);

        interactionField1.text = "You need";
        interactionField2.text = "to open";
        interactionField3.text = "nearby";
        interactionField4.text = "zone";
    }

    public void GetTheZone(int energy, int seconds)
    {
        extraMushroomsIcon.SetActive(false);
        requiredEnergyIcon.SetActive(true);

        interactionField1.text = energy + "";
        interactionField2.text = seconds + "sec";
        interactionField3.text = "to open";
        interactionField4.text = "the zone";
    }

    public void UpgradeTheZone(int energy, int seconds, int extraMushrooms)
    {
        extraMushroomsIcon.SetActive(true);
        requiredEnergyIcon.SetActive(true);

        interactionField1.text = energy + "";
        interactionField2.text = seconds + "sec";
        interactionField3.text = "to grow";
        interactionField4.text = extraMushrooms + "";
    }

    public void MaxLimitOfTheZone()
    {
        extraMushroomsIcon.SetActive(false);
        requiredEnergyIcon.SetActive(false);

        Debug.Log(interactionField1 + " " + interactionField2 + " " + interactionField3 + " " + interactionField4);

        interactionField1.text = "You have";
        interactionField2.text = "reached";
        interactionField3.text = "the max";
        interactionField4.text = "level";
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

    private void UpdateRequirementField()
    { 
        requiredEnergyField.text = requiredEnergyAmount + "x";
        requiredMushroomsField.text= requiredMushroomsNumber + "x";
    }
}
