using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemFollow : MonoBehaviour
{
    public Animator anim;
    public Rigidbody2D rB2;
    public LevelManager lM;
    public float speed;
    private float slowspeed;
    public float lineOfSite;
    public float attackRange;
    public float fireRate = 1f;
    private float nextFireTime;
    //public GameObject Attack;
    //public GameObject attackParent;
    public Transform player;
    public Transform PuntoAtaque;
    public LayerMask PlayerLayer;
    public int danoAtaque = 50;

    public bool lookingRight;

    private bool isIdle;
    private bool isWalk;
    private bool isAtack;
    private bool isHit;
    private bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        lM = LevelManager.GetInstance();
        SetIdle();

    }
    //public Animador animador;
    // Update is called once per frame
    void Update()
    {
        SetAnimations();
        if (player ==null)
        {
            if (lM.playerInControl != null)
            player = lM.playerInControl.transform;
        }
        else
        {
            DirectionLooking();
            float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
            if (distanceFromPlayer < lineOfSite && distanceFromPlayer > attackRange)
            {
                
                //transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
                SetWalk();
                if (player.position.x > transform.position.x)
                {
                    rB2.velocity = Vector3.right * speed;
                    lookingRight = true;
                }
                else
                {
                    rB2.velocity = Vector3.left * speed;
                    lookingRight = false;
                }
            }
            else if (distanceFromPlayer < lineOfSite && distanceFromPlayer <= attackRange)
            {

                //transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
                if (nextFireTime < Time.time)
                {
                    SetAtack();
                    nextFireTime = Time.time + fireRate;
                }

            }
            else
            {
                SetIdle();
            }
        }
        
    }

    public void DirectionLooking ()
    {
        if (lookingRight)
        {
            gameObject.transform.localScale = new Vector3(1,1,1);
        }
        else
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (PuntoAtaque == null)
            return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
        Gizmos.DrawWireSphere(PuntoAtaque.position, attackRange);
        
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
            if (lM.playerInControl == lM.playerManager.fullPlayer.gameObject)
            {
                player.GetComponent<FullPlayerManager>().OnHit(danoAtaque);
            }
            if (lM.playerInControl == lM.playerManager.bodyPlayer.gameObject)
            {
                player.GetComponent<BodyManager>().OnHit(danoAtaque);
            }
            if (lM.playerInControl == lM.playerManager.headPlayer[lM.currentSlime].gameObject)
            {
                player.GetComponent<HeadManager>().OnHit(danoAtaque);
            }
            FindObjectOfType<AudioManager>().Play("EmelA");
        }

    }

    public void SetIdle ()
    {
        isIdle = true;
        isWalk = false;
        isAtack = false;
        isHit = false;
        isDead = false;
    }
    public void SetWalk()
    {
        isIdle = false;
        isWalk = true;
        isAtack = false;
        isHit = false;
        isDead = false;
    }

    public void SetAtack()
    {
        isIdle = false;
        isWalk = false;
        isAtack = true;
        isHit = false;
        isDead = false;
    }

    public void SetAnimations ()
    {
        anim.SetBool("Idle", isIdle);
        anim.SetBool("Walk", isWalk);
        anim.SetBool("Atack", isAtack);
        anim.SetBool("Hit", isHit);
        anim.SetBool("Dead", isDead);
    }
}
