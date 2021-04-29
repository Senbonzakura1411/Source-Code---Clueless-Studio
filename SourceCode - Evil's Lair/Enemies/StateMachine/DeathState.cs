using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : BaseState
{
    private Enemy _enemy;
    public DeathState(Enemy enemy) : base(enemy.gameObject)
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
        //Debug.Log("Dead");
        if (_enemy.EnemyHP <= 0)
        {
            nav.isStopped = true;
            _enemy.animator.Play("Death", 0);
            _enemy.Death();
        }

        return null;
    }
}