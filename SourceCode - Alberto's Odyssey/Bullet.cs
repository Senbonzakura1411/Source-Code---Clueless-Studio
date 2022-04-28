using Photon.Pun;
using UnityEngine;
using Photon.Pun.UtilityScripts;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private GameObject hitVFX, missVFX;

    [HideInInspector] public Vector3 target;

    private Rigidbody _rigidbody;

    private Vector3 _direction;


    private void Awake() => _rigidbody = GetComponent<Rigidbody>();

    private void Start()
    {
        _direction = Vector3.Normalize(target - transform.position);
        transform.LookAt(target);
    }

    private void FixedUpdate()
    {
        _rigidbody.AddForce(_direction * bulletSpeed);
    }

    private void OnCollisionEnter(Collision other)
    {
        ContactPoint contact = other.GetContact(0);
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;
        if (other.gameObject.CompareTag("Zombie"))
        {
            if (GetComponent<PhotonView>().IsMine)
            {
                GameManager.Instance.GetComponent<PhotonView>().RPC(nameof(GameManager.GmPlayAtPoint), RpcTarget.All);
            }
            PhotonNetwork.Instantiate(hitVFX.name, pos, rot);
            if (GetComponent<PhotonView>().IsMine)
                GameManager.Instance.GetComponent<PhotonView>().RPC(nameof(GameManager.AddScore), RpcTarget.AllBuffered, 5, PhotonNetwork.LocalPlayer.GetPlayerNumber());
            PhotonNetwork.Destroy(gameObject);
        }
        else if (other.transform.gameObject.CompareTag("Environment"))
        {
            PhotonNetwork.Instantiate(missVFX.name, pos, rot);
            PhotonNetwork.Destroy(gameObject);
        }
        
    }
}