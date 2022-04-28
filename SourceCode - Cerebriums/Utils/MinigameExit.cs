using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameExit : MonoBehaviour
{
    public GameObject panel;

    
    public void OnClick()
    {
        panel.SetActive(true);
        Time.timeScale = 0f;
    }

   public  void OnResumeClick()
    {
        panel.SetActive(false);
        Time.timeScale = 1.0f;
    }
   public void OnExitClick()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(5);
    }
}
