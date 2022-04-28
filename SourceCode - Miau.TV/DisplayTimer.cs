using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayTimer : MonoBehaviour
{
    public TMP_Text textTimer;
    public GameObject Message;
    private float timer = 480.0f;
    private bool isTimer = true;
    // Start is called before the first frame update
    void Update()
    {
        if (isTimer)
        {
            timer += Time.deltaTime*3;
            DisplayTime();
        }
        if (timer >= 1200f)
            {
            Message.SetActive(true);
            isTimer=false;
            }
        else
        {   
            Message.SetActive(false);
        }
    }

    void DisplayTime()
    {
        
        int hours2 = Mathf.FloorToInt(6);
        int hours = Mathf.FloorToInt(timer / 60.0f);
        int minutes = Mathf.FloorToInt(timer - hours * 60);
        textTimer.text = string.Format("{0:00}:{1:00}", hours, minutes);
    }

    public void StopTimer()
    {
        //isTimer=false;
        timer = 360.0f;
    }
}
