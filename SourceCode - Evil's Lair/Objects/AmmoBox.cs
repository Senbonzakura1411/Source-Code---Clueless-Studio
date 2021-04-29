using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    private void Start()
    {
        Destroy(this.gameObject, 10);
    }
    //private void OnTriggerEnter(Collider col)
    //{
    //    if (col.gameObject.CompareTag("Player"))
    //    {
    //        WeaponManager manager = col.gameObject.GetComponent<WeaponManager>();
    //        manager.EquippedWeapons[2].GetComponent<FPSGun>().bulletStock = manager.EquippedWeapons[2].GetComponent<FPSGun>().stockMax;
    //        if (manager.EquippedWeapons[3] != null) manager.EquippedWeapons[3].GetComponent<FPSGun>().bulletStock = manager.EquippedWeapons[2].GetComponent<FPSGun>().stockMax;
    //        GUIUpdater.valueChange = true;
    //        gameObject.GetComponent<AudioSource>().Play();
    //        Destroy(this.gameObject);
    //    }
    //}
}
