using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirSlashManager : MonoBehaviour
{
    public float Speed;
    public Rigidbody2D rB2;
    public float damage;
    public GameObject particleToCreate;

    public void Start()
    {
        Invoke("DestroyObj", 0.7f);
    }

    public void FixedUpdate()
    {
        rB2.velocity = Vector2.right * Speed;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemigo"))
        {
            other.gameObject.GetComponent<EnemyStats>().OnHit(damage);
            DestroyObj();
        }
    }

    public void DestroyObj ()
    {
        Instantiate(particleToCreate, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
