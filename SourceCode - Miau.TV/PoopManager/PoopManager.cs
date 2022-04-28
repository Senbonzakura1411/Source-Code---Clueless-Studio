using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopManager : MonoBehaviour
{
    public float aliveTime;
    public float badPoopTime;
    public bool imBadPoop;
    public CatManager myCat;

    public void Update()
    {
        aliveTime += Time.deltaTime;
        if (aliveTime >= badPoopTime)
        {
            imBadPoop = true;
        }

        if (imBadPoop)
        {
            myCat.catPoints.SetPoopPoints();
        }
    }
}
