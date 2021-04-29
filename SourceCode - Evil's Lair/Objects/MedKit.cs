using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedKit : MonoBehaviour
{
    private void Start()
    {
        Destroy(this.gameObject, 10);
    }
    //private void OnCollisionEnter(Collision col)
    //{
    //    if (col.gameObject.CompareTag("Player"))
    //    {
    //        PlayerHealth.health = 100;
    //        GUIUpdater.valueChange = true;
    //        gameObject.GetComponent<AudioSource>().Play();
    //        Destroy(this.gameObject);
    //    }
    //}
}
