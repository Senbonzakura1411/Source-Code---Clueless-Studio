using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteManager : MonoBehaviour
{
    public AudioSource myAudio;
    public GameControl gC;

    public void Start()
    {
        gC = GameControl.GetInstance();
        myAudio = gameObject.GetComponent<AudioSource>();
    }

    public void Update()
    {
        if (gC.isMuted)
        {
            myAudio.mute = true;
        }
        else
        {
            myAudio.mute = false;
        }
    }
}
