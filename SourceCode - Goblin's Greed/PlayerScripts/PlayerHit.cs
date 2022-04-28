using System.Collections;
using Photon.Pun;
using UnityEngine;

namespace PlayerScripts
{
    public class PlayerHit : MonoBehaviourPunCallbacks
    {
        private Animator _animator;

        private PlayerController _controller;
    
        private static readonly int IsStunned = Animator.StringToHash("IsStunned");


        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _controller = GetComponent<PlayerController>();
        }

        [PunRPC]
        public void OnPlayerHit(float duration)
        {
            StartCoroutine(OnPlayerHitCoroutine(duration));
        }
        
        private IEnumerator OnPlayerHitCoroutine(float duration)
        {
            if (photonView.IsMine)
            {
                _animator.SetBool(IsStunned, true);
                GetComponent<PlayerInventory>().DropItemsStunned();
                _controller.enabled = false;
                yield return new WaitForSeconds(duration);
                _animator.SetBool(IsStunned, false);
                _controller.enabled = true;
                
            }
        }
    }
}
