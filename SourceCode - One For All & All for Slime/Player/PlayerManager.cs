using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public LevelManager lM;
    public PlayerStats playerStats;
    public FullPlayerManager fullPlayer;
    public HeadManager[] headPlayer;
    public BodyManager bodyPlayer;

    public bool playerIsSplit;
    public bool controlHead;

    public bool canMove;

    public bool lookingRight;

    bool hasPressCtrl;

    public void Start()
    {
        canMove = true;
        lM = LevelManager.GetInstance();
        lM.playerManager = this;
    }

    public void Update()
    {
        SeparateBody();
        SwitchControl();
        FollowCamera();
        UniteBody();
    }
    public void FollowCamera()
    {
        if (playerIsSplit)
        {
            if (controlHead)
            {
                lM.vCam.Follow = headPlayer[lM.currentSlime].gameObject.transform;
                lM.playerInControl = headPlayer[lM.currentSlime].gameObject;
            }
            else
            {
                lM.vCam.Follow = bodyPlayer.gameObject.transform;
                lM.playerInControl = bodyPlayer.gameObject;
            }
        }
        else
        {
            lM.vCam.Follow = fullPlayer.gameObject.transform;
            lM.playerInControl = fullPlayer.gameObject;
        }
    }
    public void SeparateBody()
    {
        if (!playerIsSplit)
        {
            if (!hasPressCtrl)
            {
                if (Input.GetKeyDown(KeyCode.LeftControl))
                {
                    hasPressCtrl = true;
                    headPlayer[lM.currentSlime].transform.position = fullPlayer.headPos.position;
                    bodyPlayer.transform.position = fullPlayer.bodyPos.position;
                    headPlayer[lM.currentSlime].gameObject.SetActive(true);
                    headPlayer[lM.currentSlime].StartSeparation();
                    bodyPlayer.gameObject.SetActive(true);
                    fullPlayer.gameObject.SetActive(false);
                    controlHead = true;
                    playerIsSplit = true;
                    playerStats.currentHealth = playerStats.health;
                    playerStats.maxHealth = 1;
                    playerStats.health = 1;
                    AudioManager.instance.Play("PAAttach");
                }
            }
            if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                hasPressCtrl = false;
            }

        }
    }
    public void SwitchControl()
    {
        if (playerIsSplit)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if (controlHead)
                {
                    playerStats.maxHealth = 4;
                    switch (playerStats.currentHealth)
                    {
                        case 4:
                            playerStats.health = 3;
                            break;
                        case 3:
                            playerStats.health = 2;
                            break;
                        case 2:
                            playerStats.health = 1;
                            break;
                        case 1:
                            playerStats.health = 1;
                            break;
                    }
                    controlHead = false;
                }
                else
                {
                    playerStats.maxHealth = 1;
                    playerStats.health = 1;
                    controlHead = true;
                }
            }
        }
    }

    public void UniteBody()
    {
        if (playerIsSplit)
        {
            float distance = Vector3.Distance(headPlayer[lM.currentSlime].gameObject.transform.position, bodyPlayer.gameObject.transform.position);

            if (distance <= 2)
            {
                if (!hasPressCtrl)
                {
                    if (Input.GetKeyDown(KeyCode.LeftControl))
                    {
                        hasPressCtrl = true;
                        fullPlayer.gameObject.transform.position = bodyPlayer.gameObject.transform.position;
                        fullPlayer.gameObject.SetActive(true);
                        bodyPlayer.gameObject.SetActive(false);
                        headPlayer[lM.currentSlime].gameObject.SetActive(false);
                        controlHead = false;
                        playerIsSplit = false;
                        playerStats.maxHealth = 4;
                        playerStats.health = playerStats.currentHealth;
                        AudioManager.instance.Play("PADetach");
                    }
                }

            }

            if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                hasPressCtrl = false;
            }
        }
    }
}
