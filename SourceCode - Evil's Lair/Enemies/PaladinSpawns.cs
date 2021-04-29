using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;

public class PaladinSpawns : MonoBehaviour
{
    [SerializeField] GameObject paladinObject;
    public static int paladinSpawned;
    int spawnPosition, totalSpawns;
    [SerializeField] List<GameObject> spawnPoints;
    [SerializeField] List<GameObject> firstSpawnPoint;

    void Start()
    {
        spawnPosition = Random.Range(0, firstSpawnPoint.Count);
        Instantiate(paladinObject, firstSpawnPoint[spawnPosition].transform.position, Quaternion.identity);
        totalSpawns++;

        for (paladinSpawned = 1; paladinSpawned < 2; paladinSpawned++)
        {
            spawnPosition = Random.Range(0, spawnPoints.Count);
            Instantiate(paladinObject, spawnPoints[spawnPosition].transform.position, Quaternion.identity);
            spawnPoints.RemoveAt(spawnPosition);
            totalSpawns++;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (totalSpawns < 4)
        {
            for (; paladinSpawned < 2; paladinSpawned++)
            {
                spawnPosition = Random.Range(0, spawnPoints.Count);
                Instantiate(paladinObject, spawnPoints[spawnPosition].transform.position, Quaternion.identity);
                spawnPoints.RemoveAt(spawnPosition);
                totalSpawns++;
            }
        }
    }
}
