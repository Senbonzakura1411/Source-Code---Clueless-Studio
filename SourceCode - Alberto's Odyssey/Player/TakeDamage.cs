using Photon.Pun;
using Photon.Pun.UtilityScripts;
using UnityEngine;

namespace Player
{
    public class TakeDamage : MonoBehaviour
    {
        [SerializeField] private AudioClip onDamagedClip;
        
        private int _hp = 100;
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (_hp <= 0)
            {
                tag = "Untagged";
                GetComponent<Animator>().SetBool("IsDead", true);
                GetComponent<Collider>().enabled = false;
                GetComponent<Rigidbody>().isKinematic = true;
               var scripts = GetComponents(typeof(MonoBehaviour));

               foreach (var s in scripts)
               {
                   Destroy(s);
               }
            }
        }

        public void DamageTakenOnAttack()
         {
             if (GetComponent<PhotonView>().IsMine)
             {
                 _audioSource.PlayOneShot(onDamagedClip, 5f);
             }
             _hp -= 20;
         }
    }
}
