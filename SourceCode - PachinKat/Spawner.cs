using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    float timer = 12f;

    [SerializeField] GameObject[] birdEnemies;
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }
    private void Update()
    {
        if (timer > 5f)
        {
            timer -= 0.01f * Time.deltaTime;
        }
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            if (Random.value < 0.10)
            {
                Instantiate(birdEnemies[2], transform.position, Quaternion.identity);
            }
            else if (Random.value < 0.35)
            {
                Instantiate(birdEnemies[1], transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(birdEnemies[0], transform.position, Quaternion.identity);
            }
            yield return new WaitForSeconds(timer);
        }
    }
}
