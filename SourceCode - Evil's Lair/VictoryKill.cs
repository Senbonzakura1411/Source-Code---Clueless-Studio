using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryKill : MonoBehaviour
{
    bool isWin;
    int flag;
    int bossHP;
    [SerializeField] GameObject victoryPanel;
    [SerializeField] Enemy bossEnemy;
    void Start()
    {
        isWin = false;    
    }

    void Update()
    {
        bossHP = bossEnemy.EnemyHP;
        if (bossHP <= 0)
        {
            isWin = true;
        }
        if (isWin && flag == 0)
        {
            flag++;
            Victory();
        }
        
    }

    private void Victory()
    {
        Time.timeScale = 0f;
        victoryPanel.SetActive(true);
    }

    public void OKButton()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }
}
