using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingBallBehaviour : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(DestroySelf());
    }

    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(8f);
        Destroy(this.gameObject);
    }
}
