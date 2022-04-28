using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CatNavSystem : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;


    public Vector3 catTarget;
    public bool canWalk;
    Animator anim;

    //Variables para rotacion smooth
    Quaternion oldRot, newRot;

    private void Start()
    {
        anim = GetComponent<Animator>();
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (canWalk)
        {
            navMeshAgent.destination = catTarget;

            //Implementacion de rotacion meh
            oldRot = transform.rotation;
            transform.LookAt(catTarget);
            newRot = transform.rotation;
            transform.rotation = oldRot;
            transform.rotation = Quaternion.Lerp(transform.rotation, newRot, 1f * Time.deltaTime);
        }
        if (!navMeshAgent.isStopped)
            anim.SetInteger("State", 1);
        else if (navMeshAgent.isStopped)
        {
            anim.SetInteger("State", 0);
        }
    }
}
