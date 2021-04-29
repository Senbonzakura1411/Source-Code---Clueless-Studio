using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class PlayersLevelManager : MonoBehaviour
{
    private static PlayersLevelManager _instance;
    public static PlayersLevelManager Instance { get { return _instance; } }

    [HideInInspector]
    public int playersLevel = 0;
    [HideInInspector]
    public bool levelUp;
    [SerializeField] Image paladinsCount;
    [SerializeField] GameObject paladinIcon;
    [SerializeField] Sprite[] paladinsLeftImages;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }


    void Update()
    {
        if (playersLevel >= 4)
        {
            paladinIcon.SetActive(true);
        }

        paladinsCount.sprite = paladinsLeftImages[playersLevel];
    }
    public void LevelUp()
    {
        if (!levelUp && playersLevel < 4)
        {
            playersLevel++;
            levelUp = true;
        }
    }
}
