using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float maxLife;
    public float life;
    public float armadura;
    public GameObject MuerteEnemigo;
    
    public int damage;

    public bool imDamaged;
    public bool imStunned;

    // Start is called before the first frame update
    public void Start()
    {
        life = maxLife;
    }
    public void Update()
    {
        
    }
    public void OnHit (float damage)
    {
        imDamaged = true;
        life -= damage;
        if(life <= 0)
        {
            //Morir();
        }
    }
    // Update is called once per frame
    void Morir ()
    {
        Instantiate(MuerteEnemigo, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void CheckFPS ()
    {

    }

    public void OnHitFire (float damage)
    {
        damage = ((damage / (1 / Time.deltaTime)) * (1 + (armadura / 10)));
        //damage = damage / (1 / Time.deltaTime);
        imDamaged = true;
        life -= damage;
        if (life <= 0)
        {
            //Morir();
        }
    }

    public void OnHitElectric (float damage)
    {
        damage = damage / (1 / Time.deltaTime);
        imDamaged = true;
        life -= damage;
        if (life <= 0)
        {
            //Morir();
        }
    }

    public void OnHitEarth (float damage)
    {
        damage = damage / (1 / Time.deltaTime);
        imDamaged = true;
        imStunned = true;
        life -= damage;
        if (life <= 0)
        {
            //Morir();
        }
    }

}