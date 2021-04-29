using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    [SerializeField] GameObject playerPrefab;

    public Transform[] spawnPoints;

    public int currentSpawn;
    public int prevSpawn;


    void Start()
    {
        currentSpawn = Random.Range(0, spawnPoints.Length - 1);
        if (currentSpawn == prevSpawn)
            currentSpawn++;

        prevSpawn = currentSpawn;

        if (PhotonNetwork.IsConnectedAndReady)
        {
            if (playerPrefab != null)
            {
                PhotonNetwork.Instantiate(playerPrefab.name, spawnPoints[currentSpawn].position, Quaternion.identity);
                currentSpawn = Random.Range(0, spawnPoints.Length - 1);
                if (currentSpawn == prevSpawn)
                    currentSpawn++;

                prevSpawn = currentSpawn;
            }
            else
            {
                Debug.Log("No player prefab found");
            }
        }
    }
    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.NickName + " joined to " + PhotonNetwork.CurrentRoom.Name);
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + " joined to " + PhotonNetwork.CurrentRoom.Name + " " + PhotonNetwork.CurrentRoom.PlayerCount);
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
}
