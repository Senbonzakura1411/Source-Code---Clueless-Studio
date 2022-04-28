using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Utils;

public class GameManager : PunSingleton<GameManager>
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform[] spawnPoints;
    
    public List<Camera> cameras;
    public string InGameTimer { get; private set; }

    private int PlayersEscaped { get;  set; }

    private float Timer { get; set; }
    
    private float _startTimer, _timeDifference;

    private int _playersConnected;

   
        
    private bool _isTimerClipPlaying;


    private void Start()
    {
        if (!PhotonNetwork.IsConnectedAndReady) return;
        photonView.RPC(nameof(InGameCheck), RpcTarget.AllBuffered);
        photonView.RPC(nameof(SetTimer), RpcTarget.AllBuffered);


        AudioManager.Instance.Pause("MenuMusic");
        AudioManager.Instance.Play("BgMusic");
    }

    private void Update()
    {
        Debug.Log(_timeDifference);
        if (_timeDifference > 0f && _timeDifference <= 60f && !_isTimerClipPlaying)
        {
            _isTimerClipPlaying = true;
            AudioManager.Instance.Play("Timer");
        }
        GetTimer();
        OnGameEnd();
    }
    


    #region PhotonCallbacks

    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.NickName + " joined to " + PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + " joined to " + PhotonNetwork.CurrentRoom.Name + " " +
                  PhotonNetwork.CurrentRoom.PlayerCount);
    }

    public override void OnLeftRoom()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    #endregion

    #region PUNRPC

    [PunRPC]
    private void InGameCheck()
    {
        _playersConnected++;

        if (_playersConnected == PhotonNetwork.PlayerList.Length)
        {
            SpawnPlayer();
        }
    }
    
    [PunRPC]
    public void SetPortalTimer()
    {
        FindObjectOfType<Brazier>().isPortalCreated = true;
        _startTimer = Time.time;
        Timer = 120f;
    }
    
    [PunRPC]
    private void SetTimer()
    {
        _startTimer = Time.time;
        Timer = 600f;
        
    }
    
    [PunRPC]
    public void SetEscaped()
    {
        PlayersEscaped++;
        Debug.Log(PlayersEscaped);
    }

    #endregion

    #region Private Methods

    private void SpawnPlayer()
    {
        if (playerPrefab != null)
        {
            PhotonNetwork.Instantiate(playerPrefab.name, spawnPoints[PhotonNetwork.LocalPlayer.ActorNumber - 1].position, Quaternion.identity);
        }
        else
            Debug.Log("No player prefab found");
    }
    
    
    private void GetTimer()
    {
         _timeDifference = Timer - (Time.time - _startTimer);
        var minutes = Mathf.FloorToInt(_timeDifference / 60);
        var seconds = Mathf.FloorToInt(_timeDifference - minutes * 60);
        InGameTimer = $"{minutes:00}:{seconds:00}";
    }
    
    private void OnGameEnd()
    {
        if (_timeDifference <= 0f || PlayersEscaped == _playersConnected)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(2);
        }
    }

    #endregion

    public void GetCamera()
    {
        foreach (var cam in cameras)
        {
            if (cam != null)
            {
                cam.gameObject.SetActive(true);
            }
        }
    }
    
}