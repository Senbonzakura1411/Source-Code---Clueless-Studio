using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameControl gC;
    public PlayerBehavior player;

    public QuadManager backGround;

    public GameObject panelInicial;

    public GameObject tryAgainPanel;

    public void Start()
    {
        gC = GameControl.GetInstance();
        if (gC.imNewGame)
        {
            panelInicial.SetActive(true);
            player._speed = 0;
            player._speed = 0;
            player._canFlash = false;
            player._canJump = false;
        }
        else
        {
            panelInicial.SetActive(false);
            player._speed = player.baseSpeed;
            player._speed = gC.playerSpeed;
            player._canFlash = true;
            player._canJump = true;

            if (gC.intentos >= 10)
            {
                StartCoroutine(TryAgainPanel());
            }
        }
    }

    public void PressContinueGame()
    {
        panelInicial.SetActive(false);
        player._speed = player.baseSpeed;
        player._speed = gC.playerSpeed;
        player._canFlash = true;
        player._canJump = true;
    }

    public IEnumerator TryAgainPanel ()
    {
        tryAgainPanel.SetActive(true);
        gC.intentos = 1;
        yield return new WaitForSeconds(3f);
        tryAgainPanel.SetActive(false);
    }
}
