using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    //#region Singleton:LevelManager

    //public static LevelManager Instance;

    //void Awake()
    //{
    //    if (Instance == null)
    //        Instance = this;
    //    else
    //        Destroy(gameObject);
    //}

    //#endregion

    public Button[] levelButtons;
    public static int reachedLevel = 3;

    private void Start()
    {
        if (reachedLevel <= 1)
        {
            reachedLevel = 1;
        }
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i + 1 > reachedLevel)
            {
                levelButtons[i].interactable = false;
            }     
        }
    }

    public void LoadLevel(int i)
    {
        SceneManager.LoadScene(i);
    }
    
    public void PowerButton ()
    {
        SceneManager.LoadScene(1);
    }
    
    
}
