using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject uiPanel;
    public GameObject pausePanel;

    public GameControl gC;

    public void Start()
    {
        gC = GameControl.GetInstance();
    }

    public void Update()
    {
        ManagePause();
        if (Input.GetKeyDown(KeyCode.P))
        {
            SetPause();
        }
    }

    public void ManagePause()
    {
        if (gC.isPaused)
        {
            uiPanel.SetActive(false);
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            uiPanel.SetActive(true);
            pausePanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void SetPause ()
    {
        if (gC.isPaused)
        {
            gC.isPaused = false;
        }
        else
        {
            gC.isPaused = true;
        }
    }
}
