using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameHandler5 : MonoBehaviour
{
    public static int score;
    public int scoreLimit;
    private int _nextLevel = 2;
    public GameObject startMessagePanel, finalMessagePanel;
    public TextMeshProUGUI textScore;
    public Button pauseButton;

   
    private float _timer;

    private void Start()
    {
        pauseButton.interactable = false;
        Time.timeScale = 0;
        startMessagePanel.SetActive(true);      
    }
    private void Update()
    {
        _timer = 60f - Time.timeSinceLevelLoad;
        Debug.Log(_timer);
        if (score == scoreLimit || Mathf.Round(_timer) == 0)
        {
            Time.timeScale = 0f;
            pauseButton.interactable = false;

            if (score == scoreLimit)
            {
                finalMessagePanel.SetActive(true);
                textScore.text = "Lo hiciste perfecto.\n\n Felicidades.\n\n Obtuviste " + (score * 10) + " Verbis.";
            }
            else if (score > scoreLimit / 2)
            {
                finalMessagePanel.SetActive(true);
                textScore.text = "Buen Trabajo.\n\n Lo estas logrando.\n\n Obtuviste " + (score * 10) + " Verbis.";       
            }
            else
            {
                finalMessagePanel.SetActive(true);
                textScore.text = "Lo intentaste.\n\n Obtuviste " + (score * 10) + " Verbis.\n\n Sigue practicando para desbloquear otros niveles.";
            }
           
        }

        
    }
    public void OnStartClick()
    {
        pauseButton.interactable = true;
        startMessagePanel.SetActive(false);
        Time.timeScale = 1f;
    }
    public void OnOkClick()
    {
        if (score > scoreLimit / 2 && LevelManager.reachedLevel < _nextLevel)
        {
            LevelManager.reachedLevel = _nextLevel;
        }
        Game.Instance.Coins += (score * 10);
        score = 0;
        Time.timeScale = 1f;
        SceneManager.LoadScene(5);
    }
}
