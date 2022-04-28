using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DayCounter : MonoBehaviour
{
    public TMP_Text textDay;
    private int day = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        textDay.text = string.Format("{0}", day);
    }

    public void Onpress()
    {
        day = day+1;
        
    }
}
