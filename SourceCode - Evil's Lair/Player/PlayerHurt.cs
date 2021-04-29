using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurt : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField]  AudioClip gotHitSound;
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Titan"))
        {
            Debug.Log(col.gameObject.name);
            PlayerHealth.health -= Random.Range(35, 40);
            GUIUpdater.valueChange = true;
           audioSource.PlayOneShot(gotHitSound);
        }
        if (col.gameObject.CompareTag("Paladin"))
        {
            Debug.Log(col.gameObject.name);
            PlayerHealth.health -= Random.Range(25, 35);
            GUIUpdater.valueChange = true;  
            audioSource.PlayOneShot(gotHitSound);
        }
        if (col.gameObject.CompareTag("Runner"))
        {
            Debug.Log(col.gameObject.name);
            PlayerHealth.health -= Random.Range(10,20);
            GUIUpdater.valueChange = true;
            audioSource.PlayOneShot(gotHitSound);
        }
    }
}
