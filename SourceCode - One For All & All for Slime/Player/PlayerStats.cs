using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    [SerializeField]
    [Header("Level Manager")]
    public LevelManager lM;


    [SerializeField]
    [Header("Speed")]
    public float fullBaseSpeed = 10;
    public float fullSpeed;

    public float slimeBaseSpeed = 5;
    public float slimeSpeed;

    public float bodyBaseSpeed = 7;
    public float bodySpeed;

    [SerializeField]
    [Header("JumpForce")]
    public float fullBaseJumpForce;
    public float fullJumpForce;
    public float slimeBaseJumpForce;
    public float slimeJumpForce;
    public float bodyBaseJumpForce;
    public float bodyJumpForce;

    [SerializeField]
    [Header("Health")]
    public int currentHealth;
    public int health;
    public int maxHealth;

    [SerializeField]
    [Header("Special Electric")]
    public float initialElectricCounterUse;
    public float electricCounterUse;
    public float electricCooldown;
    public float currentElectricCooldown;
    public bool canUseElectric;
    public bool usingElectric;

    [SerializeField]
    [Header("Special Fire")]
    public float initialFireCounterUse;
    public float fireCounterUse;
    public float fireCooldown;
    public float currentFireCooldown;
    public bool canUseFire;
    public bool usingFire;

    [SerializeField]
    [Header("Special Earth")]
    public float initialEarthCounterUse;
    public float earthCounterUse;
    public float earthCooldown;
    public float currentEarthCooldown;
    public bool canUseEarth;
    public bool usingEarth;

    [SerializeField]
    [Header("Special Air")]
    public float airCooldown;
    public float currentAirCooldown;
    public bool canUseAir;

    public void Start()
    {
        lM = LevelManager.GetInstance();
        lM.playerStats = this;

        fullSpeed = fullBaseSpeed;
        slimeSpeed = slimeBaseSpeed;
        bodySpeed = bodyBaseSpeed;

        fullJumpForce = fullBaseJumpForce;
        slimeJumpForce = slimeBaseJumpForce;
        bodyJumpForce = bodyBaseJumpForce;
        health = maxHealth;

        currentElectricCooldown = electricCooldown;
        currentFireCooldown = fireCooldown;
        currentEarthCooldown = earthCooldown;
        currentAirCooldown = airCooldown;
    }

    public void Update()
    {
        ManageElectricCooldown();
        ManageElectricUseTimer();
        CancelElectric();
        ManageFireUseTimer();
        ManageFireCooldown();
        CancelFire();
        ManageEarthUseTimer();
        ManageEarthCooldown();
        CancelEarth();
        ManagerAirCooldown();
    }

    public void ManageElectricUseTimer()
    {
        if (electricCounterUse > 0)
        {
            usingElectric = true;
            canUseElectric = false;
            currentElectricCooldown = 0;
            electricCounterUse -= Time.deltaTime;
        }
        else
        {
            if (usingElectric)
            {
                AudioManager.instance.Pause("PAElecF"); 
                usingElectric = false;

            }
        }
    }

    public void ManageElectricCooldown()
    {
        if (!usingElectric)
        {
            if (currentElectricCooldown < electricCooldown)
            {
                currentElectricCooldown += Time.deltaTime;
            }
            else
            {
                canUseElectric = true;
            }
        }

    }

    public void CancelElectric()
    {
        if (lM.currentSlime != 1 || lM.playerManager.playerIsSplit)
        {
            usingElectric = false;
            electricCounterUse = 0;
            AudioManager.instance.Pause("PAElecF");
        }
    }

    public void ManageFireUseTimer()
    {
        if (fireCounterUse > 0)
        {
            usingFire = true;
            canUseFire = false;
            currentFireCooldown = 0;
            fireCounterUse -= Time.deltaTime;
        }
        else
        {
            if (usingFire)
            {
                AudioManager.instance.Pause("PAFlameT");
                usingFire = false;

            }
        }
    }

    public void ManageFireCooldown()
    {
        if (!usingFire)
        {
            if (currentFireCooldown < fireCooldown)
            {
                currentFireCooldown += Time.deltaTime;
            }
            else
            {
                canUseFire = true;
            }
        }

    }

    public void CancelFire()
    {
        if (lM.currentSlime != 0 || lM.playerManager.playerIsSplit)
        {

            usingFire = false;
            fireCounterUse = 0;
            AudioManager.instance.Pause("PAFlameT");
        }
    }

    public void ManageEarthUseTimer()
    {
        if (earthCounterUse > 0)
        {
            usingEarth = true;
            canUseEarth = false;
            currentEarthCooldown = 0;
            earthCounterUse -= Time.deltaTime;
        }
        else
        {
            if (usingEarth)
            {
                usingEarth = false;

            }
        }
    }

    public void ManageEarthCooldown()
    {
        if (!usingEarth)
        {
            if (currentEarthCooldown < earthCooldown)
            {
                currentEarthCooldown += Time.deltaTime;
            }
            else
            {
                canUseEarth = true;
            }
        }

    }

    public void CancelEarth()
    {
        if (lM.currentSlime != 0 || lM.playerManager.playerIsSplit)
        {
            usingFire = false;
            fireCounterUse = 0;
        }
    }

    public void UseAirSpecial ()
    {
        currentAirCooldown = 0;
    }

    public void ManagerAirCooldown ()
    {
        if (currentAirCooldown < airCooldown)
        {
            canUseAir = false;
            currentAirCooldown += Time.deltaTime;
        }
        else
        {
            canUseAir = true;
        }
    }
}
