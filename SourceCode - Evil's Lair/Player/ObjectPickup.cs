using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.GetComponent<EnergyBox>() != null)
        {
            BatteryPower.power = 100;
            GUIUpdater.valueChange = true;
            col.gameObject.GetComponent<AudioSource>().Play();
            Destroy(col.gameObject);
        }
        else if (col.GetComponent<AmmoBox>() != null)
        {
            var manager = GetComponent<WeaponManager>();

            manager.EquippedWeapons[1].GetComponentInChildren<FPSGun>().RefillAmmo();

            if (manager.EquippedWeapons[2] != null)
            {
                manager.EquippedWeapons[2].GetComponent<FPSGun>().RefillAmmo();
            }
            GUIUpdater.valueChange = true;
            col.gameObject.GetComponent<AudioSource>().Play();
            Destroy(col.gameObject);
        }
        else if (col.GetComponent<MedKit>() != null)
        {
            PlayerHealth.health = 100;
            GUIUpdater.valueChange = true;
            col.GetComponent<AudioSource>().Play();
            Destroy(col.gameObject);
        }
    }
}
