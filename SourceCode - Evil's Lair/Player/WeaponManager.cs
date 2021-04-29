using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponManager : MonoBehaviour
{
    PlayerControls controls;
    [SerializeField] GameObject[] weapons;
    public List<GameObject> EquippedWeapons;
    int currentWeapon;

    void Awake()
    {
        controls = LightSettingsPlayer.controls;

        controls.Gameplay.WeaponSwap.performed += ctx => WeaponSwap();
    }

    private void Start()
    {
        currentWeapon = 1;
        EquippedWeapons[currentWeapon].SetActive(true);
    }

    private void Update()
    {
        if (PlayersLevelManager.Instance.levelUp)
        {
            if (!EquippedWeapons.Contains(weapons[PlayersLevelManager.Instance.playersLevel + 1]))
            {
                AddWeapon();
            }
        }
        switch (currentWeapon)
        {
            default:
                break;
        }
    }

    private void AddWeapon()
    {
        EquippedWeapons[currentWeapon].SetActive(false);
        EquippedWeapons.RemoveAt(2);
        EquippedWeapons.Add(weapons[PlayersLevelManager.Instance.playersLevel + 1]);
        currentWeapon = 2;
        EquippedWeapons[currentWeapon].SetActive(true);
        PlayersLevelManager.Instance.levelUp = false;
    }

    private void WeaponSwap()
    {
        if (!Pause.isPaused)
        {
            {
                if (EquippedWeapons[2] == null)
                {
                    if (EquippedWeapons[1].GetComponentInChildren<FPSGun>().reloading == false)
                    {
                        if (currentWeapon == 1)
                        {
                            EquippedWeapons[currentWeapon].SetActive(false);
                            currentWeapon = 0;
                            EquippedWeapons[currentWeapon].SetActive(true);
                        }
                        else
                        {
                            EquippedWeapons[currentWeapon].SetActive(false);
                            currentWeapon = 1;
                            EquippedWeapons[currentWeapon].SetActive(true);
                        }
                    }
                }
                else if (EquippedWeapons[1].GetComponentInChildren<FPSGun>().reloading == false && EquippedWeapons[2]?.GetComponent<FPSGun>().reloading == false)
                {
                    if (currentWeapon < EquippedWeapons.Count - 1)
                    {
                        EquippedWeapons[currentWeapon].SetActive(false);
                        currentWeapon += 1;
                        EquippedWeapons[currentWeapon].SetActive(true);
                    }
                    else
                    {
                        EquippedWeapons[currentWeapon].SetActive(false);
                        currentWeapon = 0;
                        EquippedWeapons[currentWeapon].SetActive(true);
                    }
                }
            }
        }
    }
}


