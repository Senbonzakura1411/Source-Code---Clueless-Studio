using System;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static bool isGameOver;

    [SerializeField] TextMeshProUGUI scoreText, highScoreText;
    [SerializeField] GameObject gameOverPanel;


    private void Start()
    {
        SetHighScoreText();
    }
    private void Update()
    {
        if (!isGameOver)
        {
            Time.timeScale = 1f;
            scoreText.text = "Score : " + Enemy.points;
        }
        else
        {
            if (Enemy.points > Convert.ToUInt64(PlayerPrefs.GetString("HighScore", "0")))
            {
                SetHighScoreValue(Enemy.points);
            }
            Time.timeScale = 0f;
        }
    }


    private void SetHighScoreText()
    {
        highScoreText.text = "High Score: " + PlayerPrefs.GetString("HighScore", "0");
    }

    private void SetHighScoreValue(ulong score)
    {
        PlayerPrefs.SetString("HighScore", score.ToString());
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void ResetButton()
    {
        Enemy.points = 0;
        BirdEnemy[] enemies = FindObjectsOfType<BirdEnemy>();
        foreach (BirdEnemy enemy in enemies)
        {
            Destroy(enemy.gameObject);
        }
        BowlingBallBehaviour[] bowlingBalls = FindObjectsOfType<BowlingBallBehaviour>();
        foreach (BowlingBallBehaviour ball in bowlingBalls)
        {
            Destroy(ball.gameObject);
        }
        SetHighScoreText();
        isGameOver = false;
        gameOverPanel.SetActive(false); 
    }
}
