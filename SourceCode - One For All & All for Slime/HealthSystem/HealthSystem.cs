using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public LevelManager lM;

    public int health;
    public int numberOfHearths;

    public IndividualHearth[] hearths;

    public void Start()
    {
        lM = LevelManager.GetInstance();
    }

    private void Update()
    {
        health = lM.playerStats.health;
        numberOfHearths = lM.playerStats.maxHealth;
        HearthManage();
    }

    public void HearthManage ()
    {
        for (int i = 0; i < hearths.Length; i++)
        {
            if (i < health)
            {
                hearths[i].SetFull();
            }
            else
            {
                hearths[i].SetEmpty();
            }

            if (i < numberOfHearths)
            {
                hearths[i].gameObject.SetActive(true);
            }
            else
            {
                hearths[i].gameObject.SetActive(false);
            }
        }
    }

}
