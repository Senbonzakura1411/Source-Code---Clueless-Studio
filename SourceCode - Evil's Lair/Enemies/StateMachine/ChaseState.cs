using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ChaseState : BaseState
{
    private Enemy _enemy;
    private bool hasShouted;

    public ChaseState(Enemy enemy) : base(enemy.gameObject)
    {
        _enemy = enemy;
    }
    void Awake()
    {
#if UNITY_EDITOR
        Debug.unityLogger.logEnabled = false;
#else
     Debug.unityLogger.logEnabled = false;
#endif
    }
    public override Type Tick()
    {
        //Debug.Log("Chasing");
        if (_enemy.CheckHealth())
        {
            hasShouted = false;
            return typeof(DeathState);
        }


        if (_enemy.Target == null)
        {
            hasShouted = false;
            return typeof(WanderState);
        }

        if (!_enemy.animator.GetCurrentAnimatorStateInfo(0).IsName("Run") && !hasShouted)
        {

            _enemy.audioSource.PlayOneShot(_enemy.shoutSound);
            hasShouted = true;
            _enemy.animator.Play("Shout");
        }

        if (_enemy.animator.GetCurrentAnimatorStateInfo(0).IsName("Run"))
        {
            nav.speed = _enemy.runSpeed;
            nav.SetDestination(_enemy.Target.transform.position);
        }
        else if ((_enemy.animator.GetCurrentAnimatorStateInfo(0).IsName("Shout") || _enemy.animator.GetCurrentAnimatorStateInfo(0).IsName("Attack(1)")) && _enemy.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            nav.isStopped = false;
            _enemy.animator.SetInteger("State", 3);
        }

        var distance = Vector3.Distance(transform.position, _enemy.Target.transform.position);

        if (distance <= _enemy.attackRange)
        {
            return typeof(AttackState);
        }
        else if (distance >= _enemy.followRange)
        {
            hasShouted = false;
            return typeof(WanderState);
        }

        return null;
    }
}
