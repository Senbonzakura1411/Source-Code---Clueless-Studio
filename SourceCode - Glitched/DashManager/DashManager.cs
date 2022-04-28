using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashManager : MonoBehaviour
{
    public PlayerBehavior playerScript;

    public Image dashImage;
    public Image myImage;

    public Sprite full;
    public Sprite empty;


    public void Update()
    {
        dashImage.fillAmount = playerScript.coolDown / 3;
        if (dashImage.fillAmount == 1)
        {
            myImage.sprite = full;
        }
        else
        {
            myImage.sprite = empty;
        }
    }

}
