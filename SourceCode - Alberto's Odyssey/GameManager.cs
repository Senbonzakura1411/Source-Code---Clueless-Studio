using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviourPunCallbacks
{
    private static GameManager _instance;

    #region Singleton

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameManager[] allSingletonsInScene = FindObjectsOfType<GameManager>();

                if (allSingletonsInScene != null && allSingletonsInScene.Length > 0)
                {
                    if (allSingletonsInScene.Length > 1)
                    {
                        Debug.LogWarning("There is more than one GameManager in the scene!");

                        for (var i = 1; i < allSingletonsInScene.Length; i++)
                        {
                            Destroy(allSingletonsInScene[i].gameObject);
                        }
                    }

                    _instance = allSingletonsInScene[0];
                }
                else
                {
                    Debug.LogWarning("There is no GameManager in the scene!");
                }
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null) return;
        Destroy(gameObject);
        Debug.LogWarning("Duplicated instance of GameManager has been destroyed");
    }

    #endregion

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject[] playerPrefabs;
    [SerializeField] private GameObject enemySpawner;
    [SerializeField] private Transform[] spawnPoints, generatorSpawnPoints;
    [SerializeField] private AudioClip zombieHitSound;
    [SerializeField] private GameObject[] playerUI;

    private int _totalScore, _currentSpawn;
    private int[] _playerScores = new int[4];


    private void Start()
    {
        photonView.RPC(nameof(ActivateUI), RpcTarget.All);
        if (PhotonNetwork.IsMasterClient)
        {
            foreach (var t in generatorSpawnPoints)
            {
                PhotonNetwork.Instantiate(enemySpawner.name, t.position, Quaternion.identity);
            }
        }

        _currentSpawn = PhotonNetwork.PlayerList.Length - 1;

        if (!PhotonNetwork.IsConnectedAndReady) return;
        if (playerPrefabs != null)
        {
            PhotonNetwork.Instantiate(playerPrefabs[_currentSpawn].name, spawnPoints[_currentSpawn].position,
                Quaternion.identity);
        }
        else
        {
            Debug.LogError("No player prefab found");
        }
    }

    private void Update()
    {
        scoreText.text = "Score: " + _totalScore;
    }

    [PunRPC]
    public void ActivateUI()
    {
        for (var i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            playerUI[i].SetActive(true);
            playerUI[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =
                PhotonNetwork.PlayerList[i].NickName;
        }
    }

    [PunRPC]
    public void AddScore(int points, int id)
    {
        _totalScore += points;
        _playerScores[id] += points;
        playerUI[id].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Score: " + _playerScores[id];
    }

    [PunRPC]
    public void GmPlayAtPoint()
    {
        AudioSource.PlayClipAtPoint(zombieHitSound, new Vector3(0, 15, 14), 1f);
    }
}