using System.Collections;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using Quaternion = UnityEngine.Quaternion;

namespace PlayerScripts
{
    public class PlayerAbilities : MonoBehaviourPunCallbacks
    {
        [SerializeField] private float hasteCooldown = 30f, hasteDuration = 5f;

        [SerializeField] private float copyCatCooldown = 60f, copyCatDuration = 3f;

        [SerializeField] private float throwCooldown = 30f, throwDuration = 1.22f;

        [SerializeField] private Transform throwPosition;

        [SerializeField] private PlayerUI playerUI;

        private PlayerController _controller;

        private PlayerInput _input;

        private bool _hasteInCooldown, _copyCatInCooldown, _throwInCooldown, _isCasting;

        private TrailRenderer _trail;

        private Animator _animator;

        private Rigidbody _rigidbody;

        private float hasteTimer, copyTimer, throwTimer;

        private static readonly int IsCopyCat = Animator.StringToHash("IsCopycat");
        private static readonly int IsThrowing = Animator.StringToHash("IsThrowing");


        private void Awake()
        {
            _input = GetComponent<PlayerInput>();
            _controller = GetComponent<PlayerController>();
            _trail = GetComponent<TrailRenderer>();
            _animator = GetComponent<Animator>();
            _rigidbody = GetComponent<Rigidbody>();

            hasteTimer = hasteCooldown;
            copyTimer = copyCatCooldown;
            throwTimer = throwCooldown;
        }

        private void Update()
        {
            if (_input.HasteInput && !_hasteInCooldown)
            {
                _hasteInCooldown = true;
                StartCoroutine(HastePlayer(hasteDuration, hasteCooldown));
            }

            if (!_isCasting && _input.CopycatInput && !_copyCatInCooldown && _controller.OnGround)
            {
                _copyCatInCooldown = true;
                _isCasting = true;
                StartCoroutine(CopyCatPlayer(copyCatDuration, copyCatCooldown));
            }

            if (!_isCasting && _input.MouseLeftClick && !_throwInCooldown && _controller.OnGround)
            {
                _throwInCooldown = true;
                _isCasting = true;
                StartCoroutine(ThrowPlayer(throwDuration, throwCooldown, _input.MouseWorldTerrainPositionInputVector));
            }

            ManageHasteTime();
            ManageCopyTime();
            ManageThrowTime();
        }

        private IEnumerator HastePlayer(float duration, float cooldown)
        {
            _controller.IsHasting = true;
            photonView.RPC(nameof(PlayerSoundFX.HasteClipCall), RpcTarget.All);
            photonView.RPC(nameof(InvertTrailState), RpcTarget.All);
            yield return new WaitForSeconds(duration);
            _controller.IsHasting = false;
            photonView.RPC(nameof(InvertTrailState), RpcTarget.All);
            hasteTimer = 0;
            yield return new WaitForSeconds(cooldown);
            _hasteInCooldown = false;
        }

        private IEnumerator CopyCatPlayer(float duration, float cooldown)
        {
            _animator.SetBool(IsCopyCat, true);
            _controller.enabled = false;
            _rigidbody.isKinematic = true;
            yield return new WaitForSeconds(duration);
            _isCasting = false;
            _animator.SetBool(IsCopyCat, false);
            _controller.enabled = true;
            _rigidbody.isKinematic = false;
            ItemGenerator.Instance.CreateFakeItem(transform.position);
            copyTimer = 0;
            yield return new WaitForSeconds(cooldown);
            _copyCatInCooldown = false;
        }

        private IEnumerator ThrowPlayer(float duration, float cooldown, Vector3 pos)
        {
            _animator.SetBool(IsThrowing, true);
            _controller.enabled = false;
            pos.y = transform.position.y + 1.25f;
            transform.LookAt(pos);
            _rigidbody.isKinematic = true;
            yield return new WaitForSeconds(duration);
            _isCasting = false;
            var rock = PhotonNetwork.Instantiate("Rock", throwPosition.position, Quaternion.identity).GetComponent<Rock>();
            rock.target = pos;
            _animator.SetBool(IsThrowing, false);
            yield return new WaitForSeconds(duration);
            _rigidbody.isKinematic = false;
            _controller.enabled = true;
            throwTimer = 0;
            yield return new WaitForSeconds(cooldown);
            _throwInCooldown = false;
        }

        public void ManageHasteTime ()
        {
            if (hasteTimer < hasteCooldown)
            {
                hasteTimer += Time.deltaTime;
            }
            else
            {
                hasteTimer = hasteCooldown;
            }

            playerUI.hasteIcon.fillAmount = hasteTimer / hasteCooldown;
        } 
        public void ManageCopyTime ()
        {
            if (copyTimer < copyCatCooldown)
            {
                copyTimer += Time.deltaTime;
            }
            else
            {
                copyTimer = copyCatCooldown;
            }

            playerUI.copyIcon.fillAmount = copyTimer / copyCatCooldown;
        }

        public void ManageThrowTime()
        {
            if (throwTimer < throwCooldown)
            {
                throwTimer += Time.deltaTime;
            }
            else
            {
                throwTimer = throwCooldown;
            }

            playerUI.throwIcon.fillAmount = throwTimer / throwCooldown;
        }

        [PunRPC]
        private void InvertTrailState()
        {
            _trail.enabled ^= true;
        }
        
        
    }
}