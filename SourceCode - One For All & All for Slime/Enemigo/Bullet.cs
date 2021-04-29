using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    GameObject target;
    public float speed = 20f;
    public Rigidbody2D bulletRB;
    public int damage = 30;
    //public Transform PuntoAtaque;
    //public LayerMask PlayerLayer;
    //public GameObject "impactEffect";
    // Start is called before the first frame update
    void Start()
    {
        bulletRB = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        Vector2 moveDir = (target.transform.position - transform.position).normalized * speed;
        bulletRB.velocity = new Vector2(moveDir.x,moveDir.y);
    }
    //void OnTrigger2D(Collider2D hitInfo) {
        //if (PuntoAtaque == null)
            //return;
        //Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(PuntoAtaque.position, attackRange, PlayerLayer);
        //foreach(Collider2D player in hitPlayer)
        //{
            //player.GetComponent<PlayerTest>().OnHit(damage);
        //}
    //}

    void OnTrigger2D(Collider2D hitInfo) {
        PlayerTest player = hitInfo.GetComponent<PlayerTest>();
        if (player != null)
        {
            player.GetComponent<PlayerTest>().OnHit(damage);
        }

        //Instantiate(impactEffect, transform.position, transform.rotation);

        Destroy(gameObject);
    }
    

    
}
