using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    public float maxLife;
    public float life;
    public GameObject Muerteplayer;
    public bool imDamaged;
    public int damage;
    // Start is called before the first frame update
    public void Start()
    {
        life = maxLife;
    }
    public void OnHit (float damage)
    {
        imDamaged = true;
        life -= damage;
        if(life <= 0)
        {
            Morir();
        }
    }
    // Update is called once per frame
    void Morir ()
    {
        //Instantiate(Muerteplayer, transform.position, Quaternion.identity);
        Debug.Log("Jugador Destruido");
        FindObjectOfType<AudioManager>().Play("PADeath");
    }
}
