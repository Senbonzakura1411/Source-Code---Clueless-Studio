using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateMusic : MonoBehaviour
{
    public AudioSource myAudio;
    public int myOrder;

    public float active;

    public GameControl gC;

    public void Start()
    {
        gC = GameControl.GetInstance();
        active = gC.audioVolumes[myOrder];
    }

    public void Update()
    {
        ManageVolume();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            active = 1;
            gC.audioVolumes[myOrder] = 1;
        }
    }

    public void ManageVolume ()
    {
        if (active == 1)
        {
            if (myAudio.volume < 1)
            {
                myAudio.volume += 0.2f * Time.deltaTime;
            }
        }
        else if (active == 0)
        {
            myAudio.volume = 0;
        }
    }
}
