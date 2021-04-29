using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPanelRef : MonoBehaviour
{
    public static KillPanelRef PanelRef { get; private set; }

    [SerializeField] AudioSource source;
    [SerializeField] AudioClip clip;
    public void Awake()
    {
        PanelRef = this;  
        PanelRef.gameObject.SetActive(false);    
    }

    public IEnumerator KillPanelSpawn()
    {
        PanelRef.gameObject.SetActive(true);
        source.PlayOneShot(clip);
        yield return new WaitForSeconds(5f);
        PanelRef.gameObject.SetActive(false);
    }
}

