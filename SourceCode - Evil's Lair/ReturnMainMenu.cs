using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ReturnMainMenu : MonoBehaviour
{
    void Update()
    {
        if (Gamepad.current.leftTrigger.wasPressedThisFrame && Gamepad.current.rightTrigger.wasPressedThisFrame)
        {
            Application.Quit();
        }
    }
}
