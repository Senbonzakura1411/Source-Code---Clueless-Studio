using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heavyweight : MonoBehaviour
{
    [SerializeField] GameObject particleFX;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EarthSlime"))
        {
            AudioManager.instance.Play("PSGrndS");
            Instantiate(particleFX, transform.position, Quaternion.identity);
            Destroy(this.gameObject, 0.25f);
        }
    }
}
