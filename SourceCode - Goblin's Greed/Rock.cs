using Photon.Pun;
using PlayerScripts;
using UnityEngine;

public class Rock : MonoBehaviourPunCallbacks
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float stunDuration = 2.5f;

    [HideInInspector] public Vector3 target;

    private float _aliveTimer;



    private void Start()
    {
        transform.LookAt(target);
        _aliveTimer = Time.time;
    }

    private void Update()
    {
        var step = speed * Time.deltaTime;
        transform.position += transform.forward * step;
        
        if (Time.time - _aliveTimer >= 1.5f)
        {
            if (photonView.IsMine)
            {
                PhotonNetwork.Destroy(gameObject);    
            }
        }

    }

    private void OnCollisionEnter(Collision other)
    {
        if (photonView.IsMine)
        {
            PhotonNetwork.Destroy(gameObject);    
        }
        
        if (other.gameObject.CompareTag("EnemyPlayer") && photonView.IsMine)
        {
            other.gameObject.GetComponent<PhotonView>().RPC(nameof(PlayerHit.OnPlayerHit), RpcTarget.All, stunDuration);
        }

       
    }
}