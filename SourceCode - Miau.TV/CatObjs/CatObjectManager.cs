using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatObjectManager : MonoBehaviour
{
    public CatManager currentCat;
    public string myName;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cat"))
        {
            if (currentCat == null)
            {
                currentCat = other.gameObject.GetComponent<CatManager>();
                currentCat.SetCatActivity(this.gameObject, myName);
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Cat"))
        {
            if (other.gameObject.GetComponent<CatManager>() == currentCat)
            {
                currentCat = null;
            }
        }
    }
}
