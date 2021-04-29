using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverBarrier : MonoBehaviour
{
    [SerializeField] GameObject gameOverPanel;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            gameOverPanel.SetActive(true);
            GameManager.isGameOver = true;
        }
    }
}
