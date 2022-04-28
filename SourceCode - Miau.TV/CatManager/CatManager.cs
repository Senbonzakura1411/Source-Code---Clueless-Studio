using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatManager : MonoBehaviour
{
    public LevelManager lM;
    public CatBehavior myCat;

    public CatPointsManager catPoints;

    public CatStats catStats;

    public GameObject poop;

    public Vector3 initialPosition;

    public float aliveTime;
    public float maxAliveTime;

    public int currentActivities;
    public int maxActivities;

    public void Start()
    {
        lM = LevelManager.GetInstance();
        catStats = gameObject.GetComponent<CatStats>();
        initialPosition = transform.position;
    }

    public void Update()
    {
        AliveManager();
        ActivitiesManager();
    }

    public void SetCatActivity(GameObject target, string objectType)
    {
        myCat.objectTarget = target;
        myCat.SetFollow(objectType);
    }

    public void AliveManager ()
    {
        if (aliveTime < maxAliveTime)
        {
            aliveTime += Time.deltaTime;
        }
        else
        {
            myCat.SetExit();
        }
    }

    public void ActivitiesManager ()
    {
        if (currentActivities >= maxActivities)
        {
            myCat.SetExit();
        }
    }

}
