using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugAll : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 1f;
    }
    void Update()
    {
        Debug.Log("Working");
        Debug.Log("Tiempo" + Time.timeScale);
    }
}
