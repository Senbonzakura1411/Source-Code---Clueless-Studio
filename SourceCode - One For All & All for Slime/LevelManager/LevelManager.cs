using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LevelManager : MonoBehaviour
{
    public CinemachineVirtualCamera vCam;

    public Canvas sceneCanvas;

    public PlayerStats playerStats;
    public PlayerManager playerManager;

    public LevelZoneManager levelZoneManager;

    public GameObject playerInControl;
    public GameObject panelReposition;

    public GameObject tornadoInGame;

    public int currentSlime; // 0 fire, 1 electro, 2 earth, 3 air

    [SerializeField]
    [Header("CheckPoint")]
    public Vector3 lastCheckPointFullPlayer;
    public Vector3 lastCheckPointHeadPlayer;
    public Vector3 lastCheckPointBodyPlayer;

    public static LevelManager main;

    public static LevelManager GetInstance ()
    {
        return main;
    }

    public void Awake()
    {
        if (main != null && main !=this)
        {
            Destroy(this.gameObject);
        }
        main = this;
    }
    public void Start()
    {
        playerManager = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<PlayerManager>();
    }

    public void ActiveRepositionPanel ()
    {
        panelReposition.SetActive(true);
    }
}
