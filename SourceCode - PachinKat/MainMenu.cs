using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void OnQuitButtonClicked()
    {
        Application.Quit();
    }

    public void OnStartButtonClicked()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }    
}
