using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrapBehavior : MonoBehaviour
{
    private bool active;
    private bool unActive;

    public Animator anim;

    public void Update()
    {
        SetAnimations();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("FireSlime"))
        {
            SetUnActive();
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("FireSlime"))
        {
            SetActive();
        }
    }

    public void SetActive ()
    {
        active = true;
        unActive = false;
    }

    public void SetUnActive ()
    {
        active = false;
        unActive = true;
    }

    public void SetAnimations()
    {
        anim.SetBool("Active", active);
        anim.SetBool("UnActive", unActive);
    }
    void PlaySlimeFireTrapSound()
    {
        AudioManager.instance.Play("PSFlameE");
    }
}
