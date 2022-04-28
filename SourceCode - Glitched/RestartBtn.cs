using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartBtn : MonoBehaviour
{
    public GameControl gC;

    public void Start()
    {
        gC = GameControl.GetInstance();
    }

    public void PressButton ()
    {
        gC.RestartValues();
        SceneManager.LoadScene("Game");
    }
}
