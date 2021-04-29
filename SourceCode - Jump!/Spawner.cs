using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    Enemy[] _enemies;
    [SerializeField]
    float _spawnDelay, _startSpawnDelay;

    public bool _isCompleted;
    void Start()
    {
        StartCoroutine(Spawn());
    }

    void Update()
    {

    }

    //Spawn each enemy on the collection once
    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(_startSpawnDelay);
        for (int i = 0; i < _enemies.Length; i++)
        {
            Enemy enemyInstance = Instantiate(_enemies[i], transform.position, Quaternion.identity);
            enemyInstance.Move(transform.right);
            _isCompleted = i >= _enemies.Length - 1;
            yield return new WaitForSeconds(_spawnDelay);
        }
    }
}
