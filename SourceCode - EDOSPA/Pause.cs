using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.Audio;

public class Pause : MonoBehaviour
{
    [HideInInspector] public bool isPaused = false;
    [SerializeField] GameObject pausePanel, optionsPanels;
    [SerializeField] AudioMixer mixer;
    Gun gunScript;
    RigidbodyFirstPersonController controller;

void Start()
{
    gunScript = GetComponent<Gun>();
    controller = GetComponent<RigidbodyFirstPersonController>();
}
    void Update()
    {
        Debug.Log(isPaused);
        SetPause();
        if(isPaused)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    void SetPause()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            controller.mouseLook.lockCursor = false;
            isPaused = true;
            pausePanel.SetActive(true);
            gunScript.enabled = false;
            controller.enabled = false;
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && isPaused && pausePanel.activeSelf)
        {
            isPaused = false;
            pausePanel.SetActive(false);
            gunScript.enabled = true;
            controller.enabled = true;
            controller.mouseLook.lockCursor = true;
        }

    }

public void SetSensitivity(float value)
{
controller.mouseLook.XSensitivity = value;
controller.mouseLook.YSensitivity = value;
}

public void SetVolume(float value)
{
     mixer.SetFloat("masterVol", Mathf.Log10(value) * 20);
}
    public void OnOptionsClicked()
    {
        pausePanel.SetActive(false);
        optionsPanels.SetActive(true);
    }
    public void OnBackClicked()
    {
        pausePanel.SetActive(true);
        optionsPanels.SetActive(false);
    }

    public void OnQuitClicked()
    {
        Application.Quit();
    }
}
