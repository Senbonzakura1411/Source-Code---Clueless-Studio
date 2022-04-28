using System;
using System.Collections.Generic;
using Photon.Pun;
using Player;
using UnityEngine;

namespace Enemy
{
    public class Zombie : MonoBehaviour
    {
        [SerializeField] private float zombieSpeed = 0.5f;

        public ZombieStateManager StateMachine => GetComponent<ZombieStateManager>();

        public GameObject targetPlayer;

        public bool isChasing;

        public Animator animator;

        private void Awake()
        {
            InitializeStateMachine();
            animator = GetComponent<Animator>();
        }

        private void InitializeStateMachine()
        {
            var states = new Dictionary<Type, BaseState>()
            {
                {typeof(ChaseState), new ChaseState(this)},
                {typeof(AttackState), new AttackState(this)}
            };

            GetComponent<ZombieStateManager>().SetStates(states);
        }

        private void Update()
        {
            if (isChasing)
            {
                transform.LookAt(targetPlayer.transform.position);
                transform.position =
                    Vector3.MoveTowards(transform.position, targetPlayer.transform.position, zombieSpeed * Time.deltaTime);
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.CompareTag("Bullet"))
            {
                GetComponent<PhotonView>().RPC(nameof(ZombieDie), RpcTarget.AllBuffered);
            }
        }

        [PunRPC]
        private void ZombieDie()
        {
            Destroy(gameObject);
        }
        
        private void DealDamageToPlayer()
        {
            targetPlayer.GetComponent<TakeDamage>().DamageTakenOnAttack();
        }
    }
}