using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LevelZoneManager : MonoBehaviour
{
    [SerializeField]
    [Header("Level Manager")]
    public LevelManager lM;

    [SerializeField]
    [Header("Player Script")]
    public PlayerManager playerScript;

    [SerializeField]
    [Header("Transition Panel")]
    public GameObject transitionPanel;

    [SerializeField]
    [Header("Zones")]
    public int CurrentZone;
    public GameObject[] Zones;


    public void Start()
    {
        lM = LevelManager.GetInstance();
        lM.levelZoneManager = this;  
    }


    public void ChangeZone(Vector2 posToGo, int nextZone, int previosZone, bool lookingRight)
    {
        StartCoroutine(ActiveNewZone(posToGo, nextZone, previosZone, lookingRight));
    }

    public IEnumerator ActiveNewZone(Vector2 posToGo, int nextZone, int previosZone, bool lookingRight)
    {
        playerScript = lM.playerManager;
        transitionPanel.SetActive(true);
        playerScript.canMove = false;
        yield return new WaitForSeconds(1f);
        Zones[previosZone].SetActive(false);
        lM.playerInControl.gameObject.transform.position = new Vector2(posToGo.x, posToGo.y);
        if (lookingRight)
        {
            playerScript.lookingRight = true;
        }
        else
        {
            playerScript.lookingRight = false;
        }
        Zones[nextZone].SetActive(true);
        //lM.sceneCanvas.worldCamera = Zones[nextZone].gameObject.GetComponent<ZoneManager>().zoneCamera;
        lM.vCam = Zones[nextZone].gameObject.GetComponent<ZoneManager>().vCam;
    }
}
