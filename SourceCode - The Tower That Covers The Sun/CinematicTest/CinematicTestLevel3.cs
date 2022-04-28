using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicTestLevel3 : MonoBehaviour
{
    public PlayerController player;
    public Walkable targetCinematic;
    public LevelManager lM;

    public void Start()
    {
        lM = LevelManager.GetInstance();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            SetCinematic();
        }
    }

    public void SetCinematic ()
    {
        lM.cinematicMode = true;
        player.CinematicPathFind(targetCinematic.transform);
    }
}
