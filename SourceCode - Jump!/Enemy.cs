using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Enemy : MonoBehaviour
{
    
    Rigidbody2D _enemyRigidBody;
    [SerializeField]
    float _enemySpeed;
    Vector2 _movementDirection;

    void Awake()
    {
        _enemyRigidBody = GetComponent<Rigidbody2D>();
        _enemyRigidBody.gravityScale = 0.50f;
    }
    void Update()
    {
        Move(_movementDirection);
    }

    //Move the enemy in a direction
    public void Move(Vector2 direction)
    {
        _movementDirection = direction;
        _enemyRigidBody.velocity = new Vector2(_movementDirection.x * _enemySpeed, _enemyRigidBody.velocity.y);
    }

    //Change enemy direction when colliding with another enemy
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Player1Team") || collision.gameObject.CompareTag("Player2Team"))
        {
            _movementDirection *= -1f;
        }
    }
}
