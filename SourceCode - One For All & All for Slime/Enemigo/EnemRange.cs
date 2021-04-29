using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemRange : MonoBehaviour
{ 
    public float speed;
    private float slowspeed;
    public float lineOfSite;
    public float attackRange;
    public float fireRate = 1f;
    private float nextFireTime;
    public GameObject bullet;
    public GameObject bulletParent;
    private Transform player;
    public LayerMask PlayerLayer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    //public Animador animador;
    // Update is called once per frame
    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer < lineOfSite && distanceFromPlayer>attackRange)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
        }
        else if (distanceFromPlayer < lineOfSite && distanceFromPlayer <= attackRange)
        {
            
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, (0 *  speed * Time.deltaTime));
            if (nextFireTime <Time.time){
            Shoot();
            nextFireTime = Time.time + fireRate;}
            
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
        Gizmos.DrawWireSphere(transform.position, attackRange);
        
    }
    void Shoot ()
    {
        Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
        
    }
}
