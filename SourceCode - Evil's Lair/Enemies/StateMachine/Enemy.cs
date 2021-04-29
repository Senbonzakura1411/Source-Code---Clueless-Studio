using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SensorToolkit;


public class Enemy : MonoBehaviour
{
    public Transform Target { get; private set; }

    public TriggerSensor sensor;
    public Animator animator;
    public int EnemyHP;
    public float attackRange, followRange, attackCD, wanderTime, wanderRadius, runSpeed;
    public AudioSource audioSource;
    public AudioClip shoutSound, deathSound, attackSound;
    
    public StateMachine StateMachine => GetComponent<StateMachine>();

    private bool hasDied;
    private void Awake()
    {
#if UNITY_EDITOR
        Debug.unityLogger.logEnabled = true;
#else
     Debug.unityLogger.logEnabled = false;
#endif
        InitializeStateMachine();
    }

    private void InitializeStateMachine()
    {
        var states = new Dictionary<Type, BaseState>()
        {
            { typeof(WanderState), new WanderState(this) },
            { typeof (ChaseState), new ChaseState(this) },
            { typeof (AttackState), new AttackState(this) },
             { typeof (DeathState), new DeathState(this) }
        };

        GetComponent<StateMachine>().SetStates(states);
    }
    private void Start()
    {
        if (!this.gameObject.CompareTag("Titan"))
        {
            EnemyHP += EnemyHP * PlayersLevelManager.Instance.playersLevel;
        }
    }

    public void SetTarget(Transform target)
    {
        Target = target;
    }

    public void TakeDamage(int damage, Transform target)
    {
        SetTarget(target);
        EnemyHP -= damage;
    }
    public void Attack()
    {
        animator.Play("Attack");
        audioSource.PlayOneShot(attackSound);
        gameObject.transform.LookAt(Target.position);
       // Debug.Log("Attack");
    }

    public bool CheckHealth()
    {
        if (EnemyHP <= 0)
        {
            return true;
        }
        return false;
    }
    public void Death()
    {
        if (this.gameObject.CompareTag("Runner") && !hasDied)
        {
            RunnersSpawn.runnersSpawned--;
        }
        if (this.gameObject.CompareTag("Paladin") && !hasDied)
        {
            PaladinSpawns.paladinSpawned--;
            PlayersLevelManager.Instance.LevelUp();
            CoroutineManager.Instance.StartCoroutine(KillPanelRef.PanelRef.KillPanelSpawn());
        }
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && !hasDied)
        {
            audioSource.PlayOneShot(deathSound);
        }
        Destroy(this.gameObject, 2f);
        hasDied = true;
    }
}
