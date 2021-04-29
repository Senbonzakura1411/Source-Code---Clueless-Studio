using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class BaseState 
{
    public BaseState(GameObject gameObject)
    {
        this.gameObject = gameObject;
        this.transform = gameObject.transform;
        this.nav = gameObject.GetComponent<NavMeshAgent>();                                                                                                                 
    }
    protected GameObject gameObject;
    protected Transform transform;
    protected NavMeshAgent nav;
  
    public abstract Type Tick();
}
