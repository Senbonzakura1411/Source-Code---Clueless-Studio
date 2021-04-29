using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ZoneChanger : MonoBehaviour
{
    public ZoneManager myZone;

    public int CurrentZone;
    public int NextZone;
    public Vector3 posToGo;
    public LevelManager lM;

    [SerializeField]
    [Header("Looking Right")]
    public bool lookingRight;

    public void Start()
    {
        lM = LevelManager.GetInstance();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ChangeZone();
        }
    }

    public void ChangeZone()
    {
        lM.levelZoneManager.ChangeZone(posToGo, NextZone, CurrentZone, lookingRight);
    }
}
