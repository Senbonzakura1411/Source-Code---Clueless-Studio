using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerOneManager playerOne;
    private PlayerTwoManager playerTwo;

    private void Awake()
    {
        playerOne = FindObjectOfType<PlayerOneManager>();
        playerTwo = FindObjectOfType<PlayerTwoManager>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag(gameObject.GetComponent<TeamSwap>().enemyTag))
        {
            Hurt(collision.gameObject.GetComponent<Enemy>());
        }
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    switch (collision.gameObject.tag)
    //    {
    //        case "Enemy":
    //            StartCoroutine(HurtEnemy(collision.gameObject.GetComponent<Enemy>()));
    //            break;
    //        default:
    //            break;
    //    }
    //}

    //Enemy gets jump on by player so it gets destroyed


    //IEnumerator HurtEnemy(Enemy enemy)
    //{
    //    Destroy(enemy.gameObject);
    //    yield return new WaitForEndOfFrame();
    //

    //Player collides with enemy and it gets hurt
    void Hurt(Enemy enemy)
    {
        if (enemy.isActiveAndEnabled)
        {
            if (this.gameObject.tag == "Player") playerOne.LoseLife();
            else if (this.gameObject.tag == "Player2") playerTwo.LoseLife();
            Destroy(this.gameObject);
        }

    }


}
