using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerTwoManager : MonoBehaviour
{
    public int lives;
    [SerializeField]
    private Player player2;
    [SerializeField]
    private Transform playerSpawn;
    [SerializeField]
    private int level;
    [SerializeField]
    Text livesTxt;


    private void Start()
    { 
        UpdateGUI();
    }
    public void LoseLife()
    {
        if ( lives > 0)
        {
            StartCoroutine(Respawn());
        }
        else
        {
            CompleteLevel();
        }
    }

    void EndGame()
    {           
        StartNewGame();
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(2f);
        lives--;
        Instantiate(player2.gameObject, playerSpawn.position, Quaternion.identity);
        UpdateGUI();
    }
        private void CompleteLevel()
    {
        level++;
        if (level <= SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(level);
        }
        else
        {
            EndGame();
        }
    }
    void StartNewGame()
    {
        level = 0;
        SceneManager.LoadScene(level);
    }

    private void UpdateGUI()
    {
        livesTxt.text = "P2 Lives left: " + lives;
    }
}
