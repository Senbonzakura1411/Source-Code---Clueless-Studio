using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeRemaining;
    [HideInInspector] public bool timerIsRunning = false;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] GameObject defeatPanel;

    private void Start()
    {
        timerIsRunning = true;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Game Over");
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
        else
        {
            Time.timeScale = 0f;
            defeatPanel.SetActive(true);
        }

    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
