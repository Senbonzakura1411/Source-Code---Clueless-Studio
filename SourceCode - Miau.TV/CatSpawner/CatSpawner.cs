using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatSpawner : MonoBehaviour
{
    public GameObject cat;

    public LevelManager lM;

    public float maxSpawnTime;
    public float spawnTime;

    public void Start()
    {
        lM = LevelManager.GetInstance();
        spawnTime = maxSpawnTime;
    }

    public void Update()
    {
        SpawnManager();
    }

    public void SpawnManager ()
    {
        if (lM != null)
        {
            if (lM.catsInGame < lM.maxCats)
            {
                if (spawnTime > 0)
                {
                    spawnTime -= Time.deltaTime;
                }
                else
                {
                    GameObject mycat = Instantiate(cat, transform.position, Quaternion.identity);
                    mycat.gameObject.GetComponent<CatManager>().initialPosition = transform.position;
                    lM.catsInGame++;
                    spawnTime = maxSpawnTime;
                }
            }
        }
        
    }


}
