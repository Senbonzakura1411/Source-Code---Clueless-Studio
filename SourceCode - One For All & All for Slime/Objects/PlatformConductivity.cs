using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformConductivity : MonoBehaviour
{
    [SerializeField] RightLeftPlatform platform;

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("ElectricSlime"))
        {
            platform.MoveUp();
            AudioManager.instance.Play("PSCond");
        }
    }
}
