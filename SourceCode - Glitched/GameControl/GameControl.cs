using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    private static GameControl _instance;

    public Vector3 startPos;
    public float playerSpeed;

    public bool isMuted;

    public float[] audioVolumes;

    public bool isPaused;

    public bool imNewGame;

    public int intentos;


    public static GameControl GetInstance ()
    {
        return _instance;
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    public void RestartValues ()
    {
        imNewGame = true;
        isPaused = false;
        for (int i = 0; i < audioVolumes.Length; i++)
        {
            audioVolumes[i] = 0;
        }
        playerSpeed = 8;
        startPos = new Vector3(0, -3.35f, 0);
        intentos = 0;
    }

    public void ChangeMasterVolume (float value)
    {
        AudioListener.volume = value;
    }
  
}
