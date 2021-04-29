using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightLeftPlatform : MonoBehaviour
{
    [SerializeField] Transform pointA, pointB;
    public Transform minRange, maxRange;
    float direction;
    Vector2 movement, playerMovement, verticalMovement;
    [SerializeField] float speed = 1;

    private void Start()
    {
        direction = 1;
    }
    private void Update()
    {
        Move();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            MovePlayer(collision.gameObject);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {

    }

    private void Move()
    {
        if (transform.position.x > pointB.position.x)
        {
            direction = -1;
        }
        else if (transform.position.x < pointA.position.x)
        {
            direction = 1;
        }
        movement = Vector2.right * direction * speed * Time.deltaTime;
        transform.Translate(movement);

    }
    private void MovePlayer(GameObject obj)
    {
        playerMovement = Vector2.right * direction * speed * Time.deltaTime;
        obj.transform.Translate(playerMovement);
    }

    public void MoveUp()
    {
        if (transform.position.y < maxRange.position.y)
        {
            verticalMovement = Vector2.up * speed * Time.deltaTime;
            transform.Translate(verticalMovement);
        }
        
    }
    public void MoveDown()
    {
        if (transform.position.y > minRange.position.y)
        {
            verticalMovement = Vector2.down * speed * Time.deltaTime;
            transform.Translate(verticalMovement);
        }

    }
}
