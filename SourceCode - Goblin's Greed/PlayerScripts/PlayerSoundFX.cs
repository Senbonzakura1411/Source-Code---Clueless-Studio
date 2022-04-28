using Photon.Pun;
using UnityEngine;

namespace PlayerScripts
{
    public class PlayerSoundFX : MonoBehaviourPunCallbacks
    {
        [SerializeField] private AudioClip[] audioClips;

        private AudioSource _audioSource;

        private void Awake() => _audioSource = GetComponent<AudioSource>();


        public void RPCSoundCall(string methodName)
        {
            if (photonView.IsMine)
            {
                GetComponent<PhotonView>().RPC(methodName, RpcTarget.All); 
            }
        }

        [PunRPC]
        public void WalkClipCall()
        {
            _audioSource.PlayOneShot(audioClips[0]);
        }

        [PunRPC]
        public void ThrowClipCall()
        {
            _audioSource.PlayOneShot(audioClips[1]);
        }

        [PunRPC]
        public void CopyCatClipCall()
        {
            _audioSource.PlayOneShot(audioClips[2], 0.5f);
        }

        [PunRPC]
        public void HasteClipCall()
        {
            _audioSource.PlayOneShot(audioClips[3], 0.75f);
        }
        
        [PunRPC]
        public void JumpClipCall()
        {
            _audioSource.PlayOneShot(audioClips[4], 2f);
        }
        
        [PunRPC]
        public void HitClipCall()
        {
            _audioSource.PlayOneShot(audioClips[5], 1.5f);
        }
        
        [PunRPC]
        public void MoneyClipCall()
        {
            _audioSource.PlayOneShot(audioClips[6], 0.75f);
        }
    }
}