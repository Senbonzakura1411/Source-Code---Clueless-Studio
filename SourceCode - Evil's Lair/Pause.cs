using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.PostProcessing;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject pausePanel, settingsPanel, pauseFirstButton, settingsFirstButton;
    [SerializeField] PostProcessProfile standardProfile;
    ColorGrading colorGrading;
    public static bool isPaused;

    private void Awake()
    {
        LightSettingsPlayer.controls.Gameplay.Pause.performed += ctx => PauseAction();
        standardProfile.TryGetSettings(out colorGrading);
    }
    void PauseAction()
    {
        if (!isPaused)
        {
            Time.timeScale = 0f;
            pausePanel.SetActive(true);
            isPaused = true;
            AudioListener.pause = true;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        else
        {
            Time.timeScale = 1f;
            pausePanel.SetActive(false);
            isPaused = false;
            AudioListener.pause = false;
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(pauseFirstButton);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }


    public void Resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        AudioListener.pause = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Settings()
    {
        pausePanel.SetActive(false);
        settingsPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(settingsFirstButton);
    }

    public void BrightnessSlider(float newGradingValue)
    {
        colorGrading.postExposure.value = newGradingValue;

    }
    public void SettingsOK()
    {
        pausePanel.SetActive(true);
        settingsPanel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(pauseFirstButton);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
