using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnersSpawn : MonoBehaviour
{
    [SerializeField] GameObject runnerObject;
    public static int runnersSpawned;
    int spawnPosition;
    [SerializeField] List<GameObject> spawnPoints;
    void Start()
    { 

        for (runnersSpawned = 0; runnersSpawned < 10; runnersSpawned++)
        {
            spawnPosition = Random.Range(0, spawnPoints.Count);
            Instantiate(runnerObject, spawnPoints[spawnPosition].transform.position, Quaternion.identity);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (runnersSpawned < 20)
        {
           StartCoroutine(SpawnRunners());
        }
    }

    IEnumerator SpawnRunners()
    {
        spawnPosition = Random.Range(0, spawnPoints.Count);
        Instantiate(runnerObject, spawnPoints[spawnPosition].transform.position, Quaternion.identity);
        runnersSpawned++;
        yield return new WaitForSeconds(25f);
       
    }
}
