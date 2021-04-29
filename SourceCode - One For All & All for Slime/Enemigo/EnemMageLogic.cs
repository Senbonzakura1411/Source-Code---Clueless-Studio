using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemMageLogic : MonoBehaviour
{
    public float speed;
    private float slowspeed;
    public float lineOfSite;
    public float attackRange;
    public float fireRate = 1f;
    private float nextFireTime;
    //public GameObject Attack;
    //public GameObject attackParent;
    private Transform player;
    public Transform PuntoAtaque;
    public LayerMask PlayerLayer;
    public int danoAtaque = 50;

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
            
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
            if (nextFireTime <Time.time){
            attack();
            nextFireTime = Time.time + fireRate;}
            
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (PuntoAtaque == null)
            return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
        Gizmos.DrawWireSphere(transform.position, attackRange);
        
    }
    private void attack()
    {
        //Animación
        //animador.SetTrigger("Attack");
        //Detectar jugador
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(PuntoAtaque.position, attackRange, PlayerLayer);
        //Dañar jugador
        foreach(Collider2D player in hitPlayer)
        {
            player.GetComponent<PlayerTest>().OnHit(danoAtaque);
        }

    }
}
