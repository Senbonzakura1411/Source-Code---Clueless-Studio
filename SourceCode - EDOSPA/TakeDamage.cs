using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityStandardAssets.Characters.FirstPerson;
using System;
using TMPro;
using Utilities;
public class TakeDamage : MonoBehaviourPunCallbacks
{
    public float hp;
    Animator anim;
    Rigidbody rb;
    AudioSource audioSource;
    [SerializeField] AudioClip headshotFX;
    bool isDead = false;

    [Header("Death UI Panel")]
    [SerializeField] GameObject deathUIpanel; 
    [SerializeField] TextMeshProUGUI deathText;
    private void Start()
    {
        hp = 100;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    [PunRPC]
    public void OnHitDamage(float damage, Player player)
    {
        if (damage > 75)
        {
            audioSource.PlayOneShot(headshotFX, 10f);
        }
        hp -= damage;
        Debug.Log(hp);

        if (hp <= 0f && !isDead)
        {
            isDead = true;
            player.AddScore(1);
            Die();
        }
    }

    private void Die()
    {
        if (photonView.IsMine)
        {
            anim.SetBool("isDead", true);
            StartCoroutine(Respawn());
            GameManager.instance.currentSpawn = UnityEngine.Random.Range(0, GameManager.instance.spawnPoints.Length - 1);
            if (GameManager.instance.currentSpawn == GameManager.instance.prevSpawn)
                GameManager.instance.currentSpawn++;

            GameManager.instance.prevSpawn = GameManager.instance.currentSpawn;
        }
    }

    IEnumerator Respawn()
    {
        deathUIpanel.SetActive(true);
        float respawnTime = 5.0f;
        while (respawnTime>0.0f)
        {
            yield return new WaitForSeconds(1.0f);
            respawnTime -= 1.0f;

            GetComponent<RigidbodyFirstPersonController>().enabled = false;
            GetComponent<Gun>().enabled = false;
            photonView.RPC("DeathSetup", RpcTarget.AllBuffered);
            deathText.text = "Respawning in " + respawnTime.ToString(".00");
        }

        anim.SetBool("isDead", false);
        deathUIpanel.SetActive(false);

        transform.position = GameManager.instance.spawnPoints[GameManager.instance.currentSpawn].position;

        GetComponent<RigidbodyFirstPersonController>().enabled = true;
        GetComponent<Gun>().enabled = true;
        photonView.RPC("RespawnSetup", RpcTarget.AllBuffered);
        photonView.RPC("RegainHP", RpcTarget.AllBuffered);
        isDead = false;
    }

    [PunRPC]
    public void RegainHP()
    {
        hp = 100;
    }
    [PunRPC]
    public void DeathSetup()
    {
        GetComponent<CapsuleCollider>().enabled = false;
        rb.useGravity = false;
    }
    
    [PunRPC]
    public void RespawnSetup()
    { 
        GetComponent<CapsuleCollider>().enabled = true;
        rb.useGravity = true;
        GetComponent<Gun>().RefillAmmo();
    }

}
