using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatPointsManager : MonoBehaviour
{
    public CatManager myCat;

    public float eatPointsTime;
    public float maxEatPointsTime;
    public int eatPoints;

    public float sleepPointsTime;
    public float maxSleepPointsTime;
    public int sleepPoints;

    public float playPointsTime;
    public float maxPlayPointsTime;
    public int playPoints;

    public float poopPointsTime;
    public float maxPoopPointTime;
    public int poopPoints;

    public void Start()
    {
        eatPointsTime = maxEatPointsTime;
        sleepPointsTime = maxSleepPointsTime;
        playPointsTime = maxPlayPointsTime;
    }

    public void Update()
    {
        SetSleepPoints();
        SetEatPoints();
        SetPlayPoints();
    }

    public void SetSleepPoints()
    {
        if (myCat.myCat.isSleeping)
        {
            if (sleepPointsTime > 0)
            {
                sleepPointsTime -= Time.deltaTime;
            }
            else
            {
                PointSystem.Instance.AddPoints(sleepPoints);
                sleepPointsTime = maxSleepPointsTime;
            }
        }
        else
        {
            sleepPointsTime = maxSleepPointsTime;
        }
    }

    public void SetEatPoints()
    {
        if (myCat.myCat.isEating)
        {
            if (eatPointsTime > 0)
            {
                eatPointsTime -= Time.deltaTime;
            }
            else
            {
                PointSystem.Instance.AddPoints(eatPoints);
                eatPointsTime = maxEatPointsTime;
            }
        }
        else
        {
            eatPointsTime = maxEatPointsTime;
        }
    }

    public void SetPlayPoints()
    {
        if (myCat.myCat.isPlaying)
        {
            if (playPointsTime > 0)
            {
                playPointsTime -= Time.deltaTime;
            }
            else
            {
                PointSystem.Instance.AddPoints(playPoints);
                playPointsTime = maxPlayPointsTime;
            }
        }
        else
        {
            playPointsTime = maxPlayPointsTime;
        }
    }

    public void SetPoopPoints()
    {
        if (poopPointsTime > 0)
        {
            poopPointsTime -= Time.deltaTime;
        }
        else
        {
            PointSystem.Instance.AddPoints(playPoints);
            poopPointsTime = maxPoopPointTime;
        }
    }
}

