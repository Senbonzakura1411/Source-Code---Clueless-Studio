using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPointManager : MonoBehaviour
{
    public AudioSource myAudio;
    public Animator anim;

    public void Start()
    {
        myAudio = gameObject.GetComponent<AudioSource>();
        anim = gameObject.GetComponent<Animator>();
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetAxisRaw("Jump") > 0)
            {
                myAudio.Play();
                anim.Play("Active");
            }
        }
    }
}
