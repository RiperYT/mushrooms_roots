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

    public GameObject ExtraInterface;

    int energyAmount;
    int foodAmount;
    int waterAmount;
    int mushroomsNumber;
    int speedMultiplier;


    private Main _main;

    // Start is called before the first frame update
    void Start()
    {
        speedMultiplier = 1;
        _main = Camera.main.GetComponent<Main>();

        energyField = GameObject.Find("EnergyField").GetComponent<Text>();
        foodField = GameObject.Find("FoodField").GetComponent<Text>();
        waterField = GameObject.Find("WaterField").GetComponent<Text>();
        mushroomsField = GameObject.Find("MushroomsField").GetComponent<Text>();
        speedField = GameObject.Find("SpeedField").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateResourcesFields();
    }

    public void Test()
    {
        UpdateSpeedInfo();
        UpdateExtractedResourcesInfo(Random.Range(0, 6), Random.Range(0, 6), Random.Range(0, 6));

        UpgradeTheZone(123, 345, 1);
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

    public void UpdateSpeedInfo()
    {
        if (speedMultiplier == 1) speedMultiplier = 2;
        else if (speedMultiplier == 2) speedMultiplier = 4;
        else if (speedMultiplier == 4) speedMultiplier = 0;
        else if (speedMultiplier == 0) speedMultiplier = 1;

        _main.SetSpeed(speedMultiplier);
        speedField.text = "x" + speedMultiplier;
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
        energyField.text = _main.GetEnergy().ToString() + "|" + _main.GetEnergyProduce().ToString() + "x" + _main.GetEnergyX().ToString() + "-" + _main.GetEnergyCosts().ToString();
        foodField.text = _main.GetFood().ToString() + "|" + _main.GetFoodProduce().ToString() + "x" + _main.GetFoodX().ToString() + "-" + _main.GetFoodCosts().ToString();
        waterField.text = _main.GetWater().ToString() + "|" + _main.GetWaterProduce().ToString() + "x" + _main.GetWaterX().ToString() + "-" + _main.GetWaterCosts().ToString();
        mushroomsField.text = _main.GetMushrooms().ToString() + "/" + _main.GetMushroomsMax().ToString();
    }
}
