using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneTransitionPanel : MonoBehaviour
{
    public LevelManager lM;

    public void Start()
    {
        lM = LevelManager.GetInstance();
    }
    public void SetOff()
    {

        gameObject.SetActive(false);
    }
    public void PlayerCanMove()
    {
        lM.playerManager.canMove = true;
    }
}
