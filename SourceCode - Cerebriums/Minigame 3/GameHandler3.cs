using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class GameHandler3 : MonoBehaviour
{
    public static bool isFound;
    public static int treasureCount;
    public static string direction;
    public GameObject player, treasure, startMessagePanel, finalMessage;
    public GameObject[] items;
    public Vector3[] itemsPositions;
    public AudioSource audioSource;
    public AudioClip[] directionSounds;
    public TextMeshProUGUI treasureText;
    public Button pauseButton;

    private float _playerX;
    private float _playerY;
    private Vector3 _playerPos;
    private int _nextLevel = 4;
    private int[] _validSpawns = { 1, 2, 3, 4, 5, 6, 7, 9, 10, 11, 12, 13, 14, 15 };
    void Start()
    {
        isFound = false;
        treasureCount = 0;
        direction = null;
        Grid grid = new Grid(5, 3, 3f, new Vector3(-8, -4));
        items = new GameObject[3];
        itemsPositions = new Vector3[3];
        startMessagePanel.SetActive(true);
        pauseButton.interactable = false;

        player.transform.position = Grid.gridCenters[8];
        _playerPos = player.transform.position;
        _playerX = player.transform.position.x;
        _playerY = player.transform.position.y;

        SpawnItems();
        while (!areDistinct(itemsPositions))
        {
            foreach (var obj in items)
            {
                Destroy(obj);
            }
            SpawnItems();
            Debug.Log("Spawned");
        }
        Time.timeScale = 0f;

    }


    private void Update()
    {
        treasureText.text = "Treasures left = " + (3 - treasureCount);
        _playerPos = player.transform.position;
        _playerX = player.transform.position.x;
        _playerY = player.transform.position.y;
        if (treasureCount == 3)
        {
            Time.timeScale = 0f;
            finalMessage.SetActive(true);
            pauseButton.interactable = false;
        }
    }


    private bool areDistinct(Vector3[] arr)
    {
        HashSet<Vector3> s = new HashSet<Vector3>(arr);

        return (s.Count == arr.Length);
    }

    private void SpawnItems()
    {
        for (int i = 0; i < items.Length; i++)
        {
            items[i] = Instantiate(treasure, Grid.gridCenters[_validSpawns[UnityEngine.Random.Range(0, _validSpawns.Length)]], Quaternion.identity);
            itemsPositions[i] = items[i].transform.position;
        }
    }

    public void OnStartClick()
    {
        startMessagePanel.SetActive(false);
        pauseButton.interactable = true;
        Time.timeScale = 1f;
        Invoke("CheckCloseDistance", 2);
    }
    public void OnOkClick()
    {
        if (LevelManager.reachedLevel < _nextLevel)
        {
            LevelManager.reachedLevel = _nextLevel;
        }
        Game.Instance.Coins += 100;
        isFound = false;
        treasureCount = 0;
        direction = null;
        Time.timeScale = 1f;
        SceneManager.LoadScene(5);
    }

    //This is the algorithm that finds the location of items
    #region SearchAlgorithm
    public void CheckCloseDistance()
    {

        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
            {
                itemsPositions[i] = new Vector3(100, 100);
            }
            else
            {
                itemsPositions[i] = items[i].transform.position;
            }
        }
        if (!isFound)
        {
            foreach (var item in itemsPositions)
            {
                if (_playerY == item.y && _playerX < item.x && Vector3.Distance(item, _playerPos) == 3)
                {
                    isFound = true;
                    direction = "Right";
                    audioSource.PlayOneShot(directionSounds[2], 3f);
                    Debug.Log("Move Right");
                    break;
                }
                else if (_playerY == item.y && _playerX > item.x && Vector3.Distance(item, _playerPos) == 3)
                {
                    isFound = true;
                    direction = "Left";
                    audioSource.PlayOneShot(directionSounds[1], 3f);
                    Debug.Log("Move Left");
                    break;
                }
                else if (_playerY < item.y && Vector3.Distance(item, _playerPos) <= 6)
                {
                    isFound = true;
                    direction = "Up";
                    Debug.Log("Move Up");
                    audioSource.PlayOneShot(directionSounds[3], 3f);
                    break;
                }
                else if (_playerY > item.y && Vector3.Distance(item, _playerPos) <= 6)
                {
                    isFound = true;
                    direction = "Down";
                    audioSource.PlayOneShot(directionSounds[0], 3f);
                    Debug.Log("Move Down");
                    break;
                }
            }
        }
        if (!isFound) CheckMediumDistance();
    }
    private void CheckMediumDistance()
    {

        foreach (var item in itemsPositions)
        {
            if (Vector3.Distance(item, _playerPos) >= 6 && Vector3.Distance(item, _playerPos) < 7 && (_playerX > item.x))
            {
                isFound = true;
                direction = "Left";
                audioSource.PlayOneShot(directionSounds[1], 3f);
                Debug.Log("Move Left");
                break;
            }
            else if (Vector3.Distance(item, _playerPos) >= 6 && Vector3.Distance(item, _playerPos) < 7 && _playerX < item.x)
            {
                isFound = true;
                direction = "Right";
                audioSource.PlayOneShot(directionSounds[2], 3f);
                Debug.Log("Move Right");
                break;
            }
        }
        if (!isFound) CheckLongDistance();
    }
    private void CheckLongDistance()
    {

        foreach (var item in itemsPositions)
        {
            if (Vector3.Distance(item, _playerPos) > 7 && Vector3.Distance(item, _playerPos) < 20 && _playerX > item.x)
            {
                isFound = true;
                direction = "Left";
                audioSource.PlayOneShot(directionSounds[1], 3f);
                Debug.Log("Move Left");
                break;
            }
            else if (Vector3.Distance(item, _playerPos) > 7 && Vector3.Distance(item, _playerPos) < 20 && _playerX < item.x)
            {
                isFound = true;
                direction = "Right";
                audioSource.PlayOneShot(directionSounds[2], 3f);
                Debug.Log("Move Right");
                break;
            }
            else
            {
                Debug.Log("Blank");
            }
        }

    }
    #endregion  


}



