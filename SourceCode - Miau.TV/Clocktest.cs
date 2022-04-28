using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clocktest : MonoBehaviour
{
    private const float REAL_SECONDS_PER_INGAME_DAY = 60f;

    private Text timeText;
    private float day;
    // Start is called before the first frame update
    private void Awake()
    {
        //timeText = transform.Find("timeText").GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        day += Time.deltaTime / REAL_SECONDS_PER_INGAME_DAY;

        float dayNormalized = day % 1f;

        float hoursPerDay = 24f;
        string hoursString = Mathf.Floor(dayNormalized * hoursPerDay).ToString("00");

        float minutesPerHour = 60f;
        string minutesString = Mathf.Floor(((dayNormalized * hoursPerDay) % 1f) * minutesPerHour).ToString("00");
        
        timeText.text = hoursString + ":" + minutesString;

    }
}
