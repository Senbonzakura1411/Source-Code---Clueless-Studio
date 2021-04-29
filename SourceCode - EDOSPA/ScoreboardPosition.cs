using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreboardPosition : MonoBehaviour
{
    Vector3 pos;
    private void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            pos.x = Screen.width/2;
            pos.y = Screen.height/2;
           
        }
        else
        {
            pos.x = 5000;
            pos.y = 0;
        };


        transform.position = pos;
    }
}
