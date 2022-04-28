using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalCube : MonoBehaviour
{
    public Level3Cinematic cinematic;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            cinematic.SetOutro();
        }
    }
}
