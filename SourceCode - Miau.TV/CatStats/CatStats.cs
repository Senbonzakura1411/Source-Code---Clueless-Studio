using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatStats : MonoBehaviour
{
    public float maxEnergy;
    public float energy;
    public bool imSleep;
    public bool needSleep;

    public float maxHunger;
    public float hunger;
    public bool imEating;
    public bool needEat;

    public float maxPoop;
    public float poop;
    public bool needPoop;

    public void Update()
    {
        EnergyManager();
        HungerManager();
        PoopManager();
    }

    public void HungerManager ()
    {
        if (!imEating)
        {
            if (hunger >= 0)
            {
                hunger -= Time.deltaTime;
            }
            if (hunger <= maxHunger/3)
            {
                needEat = true;
            }
            else
            {
                needEat = false;
            }
        }
        else
        {
            if (hunger < maxHunger)
            {
                hunger += Time.deltaTime * 6;
            }
            else
            {
                needEat = false;
            }
        }
    }

    public void EnergyManager ()
    {
        if (!imSleep)
        {
            if (energy >= 0)
            {
                energy -= Time.deltaTime;
            }
            if (energy <= maxEnergy / 3)
            {
                needSleep = true;
            }
            else
            {
                needSleep = false;
            }
        }
        else
        {
            if (energy < maxEnergy)
            {
                energy += Time.deltaTime * 4;
            }
            else
            {
                needSleep = false;
            }
        }

    }

    public void PoopManager ()
    {
        if (imEating)
        {
            poop += Time.deltaTime;
        }

        if (poop >= maxPoop)
        {
            needPoop = true;
        }
    }
}
