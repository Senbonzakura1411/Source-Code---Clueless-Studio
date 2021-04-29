using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBox : MonoBehaviour

{
    private void Start()
    {
        Destroy(this.gameObject, 10);
    }
    //private void OnTriggerEnter(Collider col)
    //{
    //    if (col.gameObject.CompareTag("Player"))
    //    {
    //        BatteryPower.power = 100;
    //        GUIUpdater.valueChange = true;
    //        gameObject.GetComponent<AudioSource>().Play();
    //        Destroy(this.gameObject);
    //    }
    //}
}
