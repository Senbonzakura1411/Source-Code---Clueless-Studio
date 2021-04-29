using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class TeamSwap : MonoBehaviour
{
    public string enemyTag;
    


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Player1Team") || collision.gameObject.CompareTag("Player2Team"))
        {
                StartCoroutine(Swap(collision.gameObject.GetComponent<Enemy>()));
        }
    }

    IEnumerator Swap(Enemy enemy)
    {
        //detectEnemies();
        //foreach (var enemy in enemies)
        Debug.Log(enemy);

        if (enemy.gameObject.tag == "Enemy" || enemy.gameObject.tag == enemyTag)
        {
            //enemy.enabled = !enemy.enabled;
            if (gameObject.tag == "Player")
            {
                Debug.Log("P1");
                enemy.gameObject.GetComponent<SpriteRenderer>().color = new Color (0, 107, 255);
                enemy.gameObject.tag = "Player1Team";
                //enemy.GetComponent<Player2Team>().enabled = false;
                //enemy.GetComponent<Player1Team>().enabled = true;
                //yield return new WaitForSeconds(teamCooldown);
                //yield return new WaitForEndOfFrame();
                //enemy.GetComponent<Player1Team>().enabled = false; 
                //enemy.enabled = true;
                //enemy.gameObject.tag = "Enemy";        
            }
            else if (gameObject.tag == "Player2")
            {
                Debug.Log("P2");
                enemy.gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
                enemy.gameObject.tag = "Player2Team";
                //    Player2Team team2Script = gameObject.AddComponent(typeof(Player2Team)) as Player2Team;
                //    enemy.tag = "Player2Team";
                //    yield return new WaitForSeconds(teamCooldown);
                //    yield return new WaitForEndOfFrame();
                //    Destroy(enemy.GetComponent<Player2Team>());
                //    enemy.GetComponent<Enemy>().enabled = true;
                //    enemy.gameObject.tag = "Enemy";

            }
        }
        yield return new WaitForEndOfFrame();
 
    }


    //    Collider2D[] detectEnemies()
    //    {
    //        enemies = Physics2D.OverlapCircleAll(gameObject.transform.position, teamAOE);
    //        return enemies;
    //    }

}

