using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    private Enemy _enemy;
    private float _attackReadyTimer;
    private bool hasAttacked = false;
    public AttackState(Enemy enemy) : base(enemy.gameObject)
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
        //Debug.Log("Attacking");
        if (!hasAttacked)
        {
            _enemy.Attack();
            hasAttacked = true;

        }
      
        if (_enemy.CheckHealth())
            return typeof(DeathState);

        if (_enemy.Target == null)
        {
            hasAttacked = false;
            return typeof(WanderState);
        }
        _attackReadyTimer -= Time.deltaTime;

        if (_attackReadyTimer <= 0f && Vector3.Distance(transform.position, _enemy.Target.transform.position) <= _enemy.attackRange)
        {
            _enemy.Attack();
            _attackReadyTimer = _enemy.attackCD;
        }
        else if (Vector3.Distance(transform.position, _enemy.Target.transform.position) >= _enemy.attackRange)
        {
            hasAttacked = false;
            return typeof(ChaseState);
        }
        return null;
    }
}