using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BatteryPower : MonoBehaviour
{
    [SerializeField] float DrainTime = 5.0f;
    [SerializeField] float RecoverTime = 5.0f;
    [SerializeField] LightSettingsPlayer cam;
    public static float power = 100;
    private bool isLeft = true;

    private void Update()
    {
       
        if (LightSettingsPlayer.nightVisionActive)
        {
            power -= 1.0f / DrainTime * Time.deltaTime;
            GUIUpdater.valueChange = true;
        }
        else
        {
            power += 1.0f / RecoverTime * Time.deltaTime;
            GUIUpdater.valueChange = true;
            isLeft = true;
        }
        if (power <= 0 && isLeft)
        {
            turnOff();
            isLeft = !isLeft;
        }
    }

    private void turnOff()
    {
        cam.CameraSwap();
    }
}

