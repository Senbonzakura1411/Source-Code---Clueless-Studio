using System.Collections;
using Photon.Pun;
using UnityEngine;

namespace PlayerScripts
{
    public class PlayerInteraction : MonoBehaviour
    {
        private PlayerUI playerUI;

        private PlayerInventory _inventory;

        private Chest _chest;

        private PlayerInput _input;

        private PlayerController _controller;

        private Rigidbody _rigidbody;

        private Animator _animator;

        private static readonly int IsOpening = Animator.StringToHash("IsOpening");

        private void Awake()
        {
            _input = GetComponent<PlayerInput>();
            _controller = GetComponent<PlayerController>();
            _animator = GetComponent<Animator>();
            _rigidbody = GetComponent<Rigidbody>();
            _inventory = GetComponent<PlayerInventory>();
        }

        private void Start()
        {
            playerUI = _inventory.playerUI;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Chest"))
                _chest = other.GetComponent<Chest>();
            if (other.CompareTag("Brazier"))
                Debug.Log("player in portal zone");
            if (other.CompareTag("Portal"))
                if (GetComponent<PhotonView>().IsMine)
                {
                    GetComponent<PlayerSetup>().SetData();
                }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Brazier"))
            {
                playerUI.infoTxt.text = "Press E to Open Portal";
                if (_input.ActionInput && !other.GetComponent<Brazier>().isPortalCreated)
                {
                    Debug.Log("A player opened the portal");
                    GameManager.Instance.GetComponent<PhotonView>()
                        .RPC(nameof(GameManager.SetPortalTimer), RpcTarget.All);
                    StartCoroutine(CreatePortalCoroutine(other.transform.position));
                }
            }

            if (other.CompareTag("Chest"))
            {
                playerUI.infoTxt.text = "Press E to open";
                if (_input.ActionInput && !_chest.isOpen)
                {
                    Debug.Log("ActivatedChest");
                    StartCoroutine(OpenChestCoroutine(_chest.gameObject));
                    _chest.photonView.RPC(nameof(_chest.OpenChest), RpcTarget.All);
                }
            }

            if (other.CompareTag("Portal"))
            {
                playerUI.infoTxt.text = "Press E to Enter";
                if (_input.ActionInput)
                {       
                    InfoCollector.Instance.GetComponent<PhotonView>().RPC(nameof(InfoCollector.CollectData), RpcTarget.All);
                    var manager = GameManager.Instance;
                    manager.GetComponent<PhotonView>().RPC(nameof(GameManager.SetEscaped), RpcTarget.All);
                    manager.GetCamera();
                    PhotonNetwork.Destroy(transform.parent.gameObject);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Chest"))
            {
                _chest = null;
                playerUI.infoTxt.text = "Information";
            }

            if (other.CompareTag("Brazier"))
            {
                playerUI.infoTxt.text = "Information";
            }

            if (other.CompareTag("Portal"))
            {
                playerUI.infoTxt.text = "Information";
            }
        }

        private IEnumerator OpenChestCoroutine(GameObject chest)
        {
            _rigidbody.isKinematic = true;
            _controller.enabled = false;
            _animator.SetBool(IsOpening, true);
            yield return new WaitForSeconds(3f);
            _chest.SpawnLoot();
            _animator.SetBool(IsOpening, false);
            _rigidbody.isKinematic = false;
            _controller.enabled = true;
        }

        private IEnumerator CreatePortalCoroutine(Vector3 pos)
        {
            var explosion = PhotonNetwork.Instantiate("PortalExplosion", pos, Quaternion.identity);
            yield return new WaitForSeconds(7f);
            PhotonNetwork.Destroy(explosion);
            PhotonNetwork.Instantiate("Portal", pos, Quaternion.identity);
        }
    }
}