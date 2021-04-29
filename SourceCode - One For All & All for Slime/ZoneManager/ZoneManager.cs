using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ZoneManager : MonoBehaviour
{
    [SerializeField]
    [Header("Level Manager")]
    public LevelManager lM;

    [SerializeField]
    [Header("Zone Cam")]
    public CinemachineVirtualCamera vCam;

    [SerializeField]
    [Header("Zone Cam")]
    public Camera zoneCamera;

    public void Start()
    {
        lM = LevelManager.GetInstance();
    }
}
