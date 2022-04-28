using System;
using UnityEngine;

namespace Enemy
{
    public class AttackState : BaseState
    {

        private Zombie _zombie;
        public AttackState(Zombie zombie) 
        {
            this._zombie = zombie;
        }

        public override void Enter()
        {
            _zombie.animator.SetBool("IsAttacking", true);
        }

        public override Type Tick()
        {
            if (Vector3.Distance(_zombie.transform.position, _zombie.targetPlayer.transform.position) > 1.5f || !_zombie.targetPlayer.CompareTag("Player"))
            {
                _zombie.animator.SetBool("IsAttacking", false);
                return typeof(ChaseState);
            }

            return typeof(AttackState);
        }

        public override void Exit()
        {
            Debug.Log("AttackExit");
        }
    }
}
