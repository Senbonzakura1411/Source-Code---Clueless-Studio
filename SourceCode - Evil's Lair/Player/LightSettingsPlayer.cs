using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.PostProcessing;

public class LightSettingsPlayer : MonoBehaviour
{
    public static PlayerControls controls;
    [SerializeField]
    PostProcessVolume volume;
    [SerializeField]
    PostProcessProfile standard;
    [SerializeField]
    PostProcessProfile nightVision;

    public static bool nightVisionActive = false;
    [SerializeField]
    GameObject nightVisionOverlay;
    [SerializeField]
    GameObject flashlight;
    void Awake()
    {
        controls = new PlayerControls();

        controls.Gameplay.Action2.performed += ctx => CameraSwap();
        controls.Gameplay.Flashlight.performed += ctx => Flashlight();
    }

    void Flashlight()
    {
        if (!Pause.isPaused)
        {
            if (!flashlight.activeSelf) flashlight.SetActive(true);
            else flashlight.SetActive(false);
        }
    }

    public void CameraSwap()
    {
        if (!Pause.isPaused)
        {
            if (!nightVisionActive && BatteryPower.power > 0)
            {
                nightVisionOverlay.SetActive(true);
                volume.profile = nightVision;
                nightVisionActive = true;
            }
            else
            {
                nightVisionOverlay.SetActive(false);
                volume.profile = standard;
                nightVisionActive = false;
            }
        }
    }
    void OnEnable()
        {
            controls.Gameplay.Enable();
        }

        void OnDisable()
        {
            controls.Gameplay.Disable();
        }
}
