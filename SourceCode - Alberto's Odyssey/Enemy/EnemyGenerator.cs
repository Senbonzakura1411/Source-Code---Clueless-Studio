using System.Collections;
using Photon.Pun;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class EnemyGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject maleZombie, femaleZombie;
        [SerializeField] private Transform[] spawnPositions;
        [SerializeField] private float timer;
        
        private void Start()
        {
            StartCoroutine(SpawnZombies());
        }

        private void Update()
        {
            if (timer > 3f)
            {
                timer -= 0.25f * Time.unscaledDeltaTime;
            }
        }

        private IEnumerator SpawnZombies()
        {
            while (true)
            {
                if (Random.value > 0.5)
                {
                    PhotonNetwork.Instantiate(maleZombie.name, spawnPositions[Random.Range(0, spawnPositions.Length)].position, quaternion.identity);
                }
                else
                {
                    PhotonNetwork.Instantiate(femaleZombie.name, spawnPositions[Random.Range(0, spawnPositions.Length)].position, quaternion.identity);
                }

                yield return new WaitForSeconds(Random.Range(timer, 5f));
            }
        }
    }
}