using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GUIUpdater : MonoBehaviour
{
    public static bool valueChange = false;

    [SerializeField]
    Image healthAmount;
    [SerializeField]
    Image BatteryUI;

    private void Start()
    {
        updateGUI();
    }
    void Update()
    {
        if (valueChange)
        {
            updateGUI();
            valueChange = false;
        }
    }

    private void updateGUI()
    {
        healthAmount.fillAmount = PlayerHealth.health * 0.01f;
        BatteryUI.fillAmount = BatteryPower.power / 100;
    }
}
