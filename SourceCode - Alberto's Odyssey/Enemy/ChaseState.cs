using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Enemy
{
    public class ChaseState : BaseState
    {
        public List<float> Distances = new List<float>();

        private Zombie _zombie;

        private GameObject[] _players;

        public ChaseState(Zombie zombie)
        {
            this._zombie = zombie;
        }

        public override void Enter()
        {
            _players = GameObject.FindGameObjectsWithTag("Player");
            _zombie.isChasing = true;

            GetTarget();
        }

        public override Type Tick()
        {
            Distances?.Clear();

            GetTarget();
            
            if (Distances.Any(distance => distance <= 1.5f))
            {
                return typeof(AttackState);
            }
            
            return typeof(ChaseState);
        }

        public override void Exit()
        {
            _zombie.isChasing = false;
            Debug.Log("ChaseExit");
        }

        private void GetTarget()
        {
            foreach (var player in _players)
            {
                Distances.Add(Vector3.Distance(player.transform.position, _zombie.transform.position));
            }

            _zombie.targetPlayer = _players[Distances.IndexOf(Distances.Min())];
        }
    }
}