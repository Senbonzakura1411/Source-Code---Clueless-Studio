using System.Collections;
using Photon.Pun;
using Player;
using TMPro;
using UnityEngine;

public class Gun : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform gunPosition;
    [SerializeField] private int magazineSize = 6;

    [SerializeField] private float reloadTime = 2f;

    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private GameObject shootVFX;
    [SerializeField] private AudioClip shootSound, noAmmoClip, reloadClip;

    private Animator _animator;

    private bool _targetRegistered, _isShooting, _isReloading;

    private int _bulletsLeft;

    private Vector3 _shootTarget;

    private PlayerInput _input;

    private AudioSource _audioSource;

    private PlayerController _playerController;

    private PlayerRotation _playerRotation;

    private static readonly int IsFiring = Animator.StringToHash("IsFiring");
    private static readonly int IsReloading = Animator.StringToHash("IsReloading");


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _input = GetComponent<PlayerInput>();
        _audioSource = GetComponent<AudioSource>();
        _playerController = GetComponent<PlayerController>();
        _playerRotation = GetComponent<PlayerRotation>();
    }

    private void Start()
    {
        _bulletsLeft = magazineSize;
    }

    private void Update()
    {
        if (_input.MouseLeftClick && _bulletsLeft > 0 && !_isReloading)
        {
            _playerController.enabled = false;
            _playerRotation.enabled = false;
            _animator.SetBool(IsFiring, true);
            _isShooting = true;
            if (!_targetRegistered)
            {
                _targetRegistered = true;
                _shootTarget = _input.WorldFixedYPositionVector;
            }
        }
        else if (_input.MouseLeftClick && !_isReloading)
        {
            if (photonView.IsMine)
            {
                photonView.RPC(nameof(PlayNoAmmoSound), RpcTarget.All);
            }
        }

        if (_input.ReloadInput && _bulletsLeft < magazineSize && !_isShooting && !_isReloading)
        {
            _isReloading = true;
            StartCoroutine(Reload(reloadTime));
        }

        ammoText.text = _bulletsLeft + "/" + magazineSize;
        Debug.DrawLine(gunPosition.position, _input.WorldFixedYPositionVector, Color.green);
    }

    [PunRPC]
    private void PlayNoAmmoSound()
    {
        _audioSource.PlayOneShot(noAmmoClip, 5f);
    }

    [PunRPC]
    private void PlayShootSound()
    {
        _audioSource.PlayOneShot(shootSound);
    }

    [PunRPC]
    private void PlayReloadSound()
    {
        _audioSource.PlayOneShot(reloadClip, 10f);
    }

    private void Shoot()
    {
        if (photonView.IsMine)
        {
            photonView.RPC(nameof(PlayShootSound), RpcTarget.All);
            PhotonNetwork.Instantiate(shootVFX.name, gunPosition.position, transform.rotation);
            var bullet = PhotonNetwork.Instantiate(bulletPrefab.name, gunPosition.position, transform.rotation)
                .GetComponent<Bullet>();
            bullet.target = _shootTarget;
            _bulletsLeft--;
            _animator.SetBool(IsFiring, false);
            _targetRegistered = false;
            _isShooting = false;
            _playerController.enabled = true;
            _playerRotation.enabled = true;
        }
    }


    private IEnumerator Reload(float timer)
    {
        photonView.RPC(nameof(PlayReloadSound), RpcTarget.All);
        _animator.SetBool(IsReloading, true);
        yield return new WaitForSeconds(timer);
        _animator.SetBool(IsReloading, false);
        _bulletsLeft = magazineSize;
        _isReloading = false;
    }
}