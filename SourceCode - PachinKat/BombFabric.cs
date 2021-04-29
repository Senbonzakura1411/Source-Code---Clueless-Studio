using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombFabric : MonoBehaviour
{
    [SerializeField] GameObject bombPrefab;
    float elapsed = 1f;
    void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed >= 1f)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(bombPrefab, transform.position, Quaternion.identity);
                AudioManager.instance.Play("BombDrop");
                elapsed %= 1f;
            }
        }
       
    }
}
