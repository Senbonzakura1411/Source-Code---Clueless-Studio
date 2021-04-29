using System.Collections.Generic;
using UnityEngine;
public class BirdEnemy : MonoBehaviour
{
    [SerializeField] int hp;
    [SerializeField] float _vel;

    Enemy enemy;
    bool isRight, isDead;
    Coroutine coroutine;

    private void Start()
    {
        enemy = new Enemy(hp, _vel, gameObject.GetComponent<Animator>());
        if (Random.value > 0.5f)
        {
            isRight = true;
        }
        coroutine = StartCoroutine(enemy.Move(isRight, this.gameObject));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("BowlingBall"))
        {
            Destroy(collision.gameObject);
            enemy.TakeDamage();
        }
    }

    private void OnBecameInvisible()
    {
        if (this.gameObject.activeSelf)
        {
            StopCoroutine(coroutine);
            Vector2 thisPosition = transform.position;
            if (Camera.main.WorldToViewportPoint(transform.position).x > 1 || Camera.main.WorldToViewportPoint(transform.position).x < 0)
            {
                thisPosition.x *= -1;
            }
            transform.position = thisPosition;
            coroutine = StartCoroutine(enemy.Move(isRight, this.gameObject));
        }
    }

    private void Update()
    {
        if (!isDead)
        {
            if (enemy._hp <= 0)
            {
                StopCoroutine(coroutine);
                isDead = true;
            }
            enemy.Die(this.gameObject);
        }

    }
}
