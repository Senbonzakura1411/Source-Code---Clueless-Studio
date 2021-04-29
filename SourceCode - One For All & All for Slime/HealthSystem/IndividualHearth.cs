using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndividualHearth : MonoBehaviour
{
    public Animator anim;

    public bool imFull;
    public bool imEmpty;

    public void Update()
    {
        SetAnimations();
    }
    public void SetFull ()
    {
        imFull = true;
        imEmpty = false;
    }

    public void SetEmpty ()
    {
        imFull = false;
        imEmpty = true;
    }
    public void SetAnimations ()
    {
        anim.SetBool("Full", imFull);
        anim.SetBool("Empty",imEmpty);
    }
}
