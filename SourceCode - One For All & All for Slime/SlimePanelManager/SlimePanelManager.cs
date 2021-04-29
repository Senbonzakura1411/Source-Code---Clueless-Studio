using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlimePanelManager : MonoBehaviour
{
    public LevelManager lM;

    public Image [] slimeSlot;

    public Color[] slimeColor;

    public void Start()
    {
        lM = LevelManager.GetInstance();
    }

    public void Update()
    {
        if (lM.playerManager.playerIsSplit == false)
        {
            SlimeRoulete();
            ChangeRight();
            ChangeLeft();
        }
    }

    public void ChangeRight ()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (lM.currentSlime < 3)
            {
                lM.currentSlime++;
            }
            else
            {
                lM.currentSlime = 0;
            }
        }
    }

    public void ChangeLeft()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (lM.currentSlime > 0)
            {
                lM.currentSlime--;
            }
            else
            {
                lM.currentSlime = 3;
            }
        }
    }
    public void SlimeRoulete ()
    {
        switch(lM.currentSlime)
        {
            case 0:
                slimeSlot[0].color = slimeColor[0];
                slimeSlot[1].color = slimeColor[1];
                slimeSlot[2].color = slimeColor[2];
                slimeSlot[3].color = slimeColor[3];

                slimeSlot[0].fillAmount = lM.playerStats.currentFireCooldown / lM.playerStats.fireCooldown;
                slimeSlot[1].fillAmount = lM.playerStats.currentElectricCooldown / lM.playerStats.electricCooldown;
                slimeSlot[2].fillAmount = lM.playerStats.currentEarthCooldown / lM.playerStats.earthCooldown;
                slimeSlot[3].fillAmount = lM.playerStats.currentAirCooldown / lM.playerStats.airCooldown;
                break;
            case 1:
                slimeSlot[0].color = slimeColor[1];
                slimeSlot[1].color = slimeColor[2];
                slimeSlot[2].color = slimeColor[3];
                slimeSlot[3].color = slimeColor[0];

                slimeSlot[3].fillAmount = lM.playerStats.currentFireCooldown / lM.playerStats.fireCooldown;
                slimeSlot[0].fillAmount = lM.playerStats.currentElectricCooldown / lM.playerStats.electricCooldown;
                slimeSlot[1].fillAmount = lM.playerStats.currentEarthCooldown / lM.playerStats.earthCooldown;
                slimeSlot[2].fillAmount = lM.playerStats.currentAirCooldown / lM.playerStats.airCooldown;
                break;
            case 2:
                slimeSlot[0].color = slimeColor[2];
                slimeSlot[1].color = slimeColor[3];
                slimeSlot[2].color = slimeColor[0];
                slimeSlot[3].color = slimeColor[1];

                slimeSlot[2].fillAmount = lM.playerStats.currentFireCooldown / lM.playerStats.fireCooldown;
                slimeSlot[3].fillAmount = lM.playerStats.currentElectricCooldown / lM.playerStats.electricCooldown;
                slimeSlot[0].fillAmount = lM.playerStats.currentEarthCooldown / lM.playerStats.earthCooldown;
                slimeSlot[1].fillAmount = lM.playerStats.currentAirCooldown / lM.playerStats.airCooldown;
                break;
            case 3:
                slimeSlot[0].color = slimeColor[3];
                slimeSlot[1].color = slimeColor[0];
                slimeSlot[2].color = slimeColor[1];
                slimeSlot[3].color = slimeColor[2];

                slimeSlot[1].fillAmount = lM.playerStats.currentFireCooldown / lM.playerStats.fireCooldown;
                slimeSlot[2].fillAmount = lM.playerStats.currentElectricCooldown / lM.playerStats.electricCooldown;
                slimeSlot[3].fillAmount = lM.playerStats.currentEarthCooldown / lM.playerStats.earthCooldown;
                slimeSlot[0].fillAmount = lM.playerStats.currentAirCooldown / lM.playerStats.airCooldown;
                break;
        }
    }
}
