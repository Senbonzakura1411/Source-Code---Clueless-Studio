using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conductivity : MonoBehaviour
{
    [SerializeField] GameObject controllingLever, item;

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("ElectricSlime"))
        {
            item.SetActive(true);
            AudioManager.instance.Play("PSCond");
        }
    }
    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("ElectricSlime") && controllingLever.GetComponent<Lever>().isActivated == false)
        {
            item.SetActive(false);
        }
    }
}
