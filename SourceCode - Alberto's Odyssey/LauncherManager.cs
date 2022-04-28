using UnityEngine;
using Photon.Pun;
using Photon.Pun.Demo.Cockpit;
using  Photon.Realtime;
using TMPro;

public class LauncherManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject enterGamePanel, connectionStatusPanel, lobbyPanel, nameField;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    private void Start()
    {
        enterGamePanel.SetActive(true);
        connectionStatusPanel.SetActive(false);
        lobbyPanel.SetActive(false);
    }
    
    public void ConnectToPhotonServer()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.LocalPlayer.NickName = nameField.GetComponent<TMP_InputField>().text;
            PhotonNetwork.ConnectUsingSettings();
            enterGamePanel.SetActive(false);
            connectionStatusPanel.SetActive(true);
        }
    }
    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    #region Photon Callbacks
    public override void OnConnectedToMaster()
    {
        Debug.Log(PhotonNetwork.NickName + " Connected to Photon Server");
        lobbyPanel.SetActive(true);
        connectionStatusPanel.SetActive(false);
    }

    public override void OnConnected()
    {
        Debug.Log("Connected to Internet");
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        Debug.Log(message);
        CreateAndJoinRoom();
    }
    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.NickName + " joined to " + PhotonNetwork.CurrentRoom.Name);
        PhotonNetwork.LoadLevel(1);
    }
    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + " joined to " + PhotonNetwork.CurrentRoom.Name + " " + PhotonNetwork.CurrentRoom.PlayerCount);
    }

    #endregion
    private void CreateAndJoinRoom()
    {
        string randomRoomName = "Room " + Random.Range(0, 10000);
        RoomOptions roomOptions = new RoomOptions {IsOpen = true, IsVisible = true, MaxPlayers = 4};
        PhotonNetwork.CreateRoom(randomRoomName,roomOptions);
    }
}
