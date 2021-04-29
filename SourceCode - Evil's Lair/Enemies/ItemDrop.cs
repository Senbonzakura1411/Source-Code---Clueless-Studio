using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField]
    GameObject[] objects;

    float chance, x, y, z;
  
    private void OnDestroy()
    {
        x = transform.position.x;
        y = transform.position.y + 1.5f;
        z = transform.position.z;
         chance = Random.Range(0, 100);
        if (chance <= 1)
            Instantiate(objects[0], new Vector3(x,y,z), Quaternion.identity);
        else if (chance <= 50)
            Instantiate(objects[1], new Vector3(x, y, z), Quaternion.identity);
        else if (chance <= 60)
            Instantiate(objects[2], new Vector3(x, y, z), Quaternion.identity);
    }
}
