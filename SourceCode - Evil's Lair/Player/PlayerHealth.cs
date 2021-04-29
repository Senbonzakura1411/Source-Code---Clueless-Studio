using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerHealth : MonoBehaviour
{

    public static float health = 100;
    [SerializeField] GameObject defeatPanel;
    private void Update()
    {
        if (health <= 0)
        {
            Time.timeScale = 0f;
            defeatPanel.SetActive(true);
        }
    }
}
