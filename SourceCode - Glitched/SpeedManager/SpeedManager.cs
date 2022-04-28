using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedManager : MonoBehaviour
{
    public PlayerBehavior playerScript;

    public Text myText;

    public void Start()
    {
        myText = gameObject.GetComponent<Text>();
    }

    public void Update()
    {

        switch(playerScript._speed)
        {
            case 8:
                myText.color = Color.green;
                myText.text = "1";
                break;
            case 9:
                myText.color = new Color(200f/255f, 255f/255f, 0);
                myText.text = "2";
                break;
            case 10:
                myText.color = Color.yellow;
                myText.text = "3";
                break;
            case 11:
                myText.color = new Color(255f/255f, 145f/255f, 0);
                myText.text = "4";
                break;
            case 12:
                myText.color = Color.red;
                myText.text = "5";
                break;
            case 13:
                myText.color = Color.red;
                myText.text = "6";
                break;
            case 14:
                myText.color = Color.red;
                myText.text = "7";
                break;
            case 15:
                myText.color = Color.red;
                myText.text = "8";
                break;
            case 16:
                myText.color = Color.red;
                myText.text = "9";
                break;
        }
    }
}
