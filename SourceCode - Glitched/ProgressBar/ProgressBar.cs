using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    // 0 inicio, 600 fin 
    public GameObject player;

    public float fillAmount;

    public Image progressBar;

    public void Update()
    {
        SetProgressBar();
        progressBar.fillAmount = fillAmount;
    }

    public void SetProgressBar ()
    {
        fillAmount = player.transform.position.x / 600;
    }
}
