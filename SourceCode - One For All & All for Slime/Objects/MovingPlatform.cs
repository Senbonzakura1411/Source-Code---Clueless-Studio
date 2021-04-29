using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    Vector2 startPos;
    GameObject obj;
    [SerializeField] Transform endPos;

    private void Start()
    {
        startPos = transform.position;
    }
    private void Update()
    {
        if (transform.position.x < endPos.position.x && obj != null)
        {
            Move();
            DistanceCheck();
            
        }
        else
        {
            transform.position = startPos;
        }
        

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8 && !obj)
        {
            obj = collision.gameObject;
        }
    }
    private void Move()
    {
        Vector2 position = transform.position;
        Vector2 colPos = obj.transform.position;
        position = position + new Vector2(1, 0) * Time.deltaTime;
        transform.position = position;
        colPos = colPos + new Vector2(1, 0) * Time.deltaTime;
        obj.transform.position = colPos;
    }

    void DistanceCheck()
    {
        float dis = Vector2.Distance(obj.transform.position, transform.position);
        if (dis > 5)
        {
            transform.position = startPos;
            obj = null;
        }
    }

}
