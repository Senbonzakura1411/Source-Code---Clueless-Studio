using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class WanderState : BaseState
{
    private Vector3? _destination;
    private Enemy _enemy;
    private float timer;

    void Awake()
    {
#if UNITY_EDITOR
        Debug.unityLogger.logEnabled = false;
#else
     Debug.unityLogger.logEnabled = false;
#endif
    }
    public WanderState(Enemy enemy) : base(enemy.gameObject)
    {
        _enemy = enemy;     
    }

    public override Type Tick()
    {
        //Debug.Log("Wandering");

       

        if (_enemy.CheckHealth())
            return typeof(DeathState);

        if (_enemy.Target != null)
        {
            nav.isStopped = true;
            return typeof(ChaseState);
        }

        
        var chaseTarget = CheckForAggro();
        if (chaseTarget != null)
        {
            _enemy.SetTarget(chaseTarget);
            return typeof(ChaseState);
        }
        timer += Time.deltaTime;

        if (timer >= _enemy.wanderTime)
        {
            nav.isStopped = false;
            _enemy.animator.SetInteger("State", 1);
            _destination = RandomNavSphere(transform.position, _enemy.wanderRadius, -1);
            nav.SetDestination(_destination.Value);
            timer = 0;
        }
        else if (timer >= (_enemy.wanderTime/2))
        {
            nav.isStopped = true;
            _enemy.animator.SetInteger("State", 0);
        }
        
        return null;
    }

    private Vector3 RandomNavSphere(Vector3 origin, float radius, int layermask)
    {
        Vector3 randDirection = UnityEngine.Random.insideUnitSphere;

        randDirection.x += UnityEngine.Random.Range(-radius, radius);
        randDirection.z += UnityEngine.Random.Range(-radius, radius);
        randDirection += origin;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, radius, layermask);
        return navHit.position;
    }

    private Transform CheckForAggro()
    {
        GameObject target = _enemy.sensor.GetNearest();
        if (target != null)
        {
            return target.transform;
        }
        else
        {
            return null;
        }

    }
}

