using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour
{
    public GameControl gC;

    public Image myImage;

    public Sprite muted;
    public Sprite unMuted;

    public void Start()
    {
        gC = GameControl.GetInstance();
        myImage = gameObject.GetComponent<Image>();
    }

    public void Update()
    {
        if (gC.isMuted)
        {
            myImage.sprite = muted;
        }
        else
        {
            myImage.sprite = unMuted;
        }
    }

    public void PressBtn()
    {
        if (gC.isMuted)
        {
            gC.isMuted = false;
        }
        else
        {
            gC.isMuted = true;
        }
    }
}
