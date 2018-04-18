using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryManager : MonoBehaviour
{
    public Image batteryPercentImg;
    public Color32 fullColor;
    public Color32 halfColor;
    public Color32 emptyColor;

    public int batteryMax = 24;

    private int batteryCurrent;

    void Start()
    {
        batteryCurrent = batteryMax;
        batteryPercentImg.fillAmount = 1.0f;
    }

    public void ReduceBattery()
    {
        batteryCurrent--;

        DisplayBatteryPercentage();

        if (batteryCurrent <= 0)
        {
            GameMaster.instance.EndGame("Buddy's Out Of Power!");
        }
    }

    void DisplayBatteryPercentage()
    {
        float percentage = ((float)batteryCurrent / (float)batteryMax);

        batteryPercentImg.fillAmount = percentage;

        if (batteryPercentImg.fillAmount <= 0.33f)
        {
            batteryPercentImg.color = emptyColor;
        }
        else if (batteryPercentImg.fillAmount >= 0.66f)
        {
            batteryPercentImg.color = fullColor;
        }
        else
        {
            batteryPercentImg.color = halfColor;
        }
    }
}
