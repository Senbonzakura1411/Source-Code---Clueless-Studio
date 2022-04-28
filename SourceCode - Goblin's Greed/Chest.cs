using System;
using Photon.Pun;
using UnityEngine;

public class Chest : MonoBehaviourPunCallbacks
{
    public bool isOpen;

    [HideInInspector] public Animator animator;
    private static readonly int IsOpen = Animator.StringToHash("IsOpen");

    private Vector3 _spawnPoint;
    private void Awake() => animator = GetComponent<Animator>();

    private void Start()
    {
        _spawnPoint = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
        if (isOpen && photonView.IsMine)
        {
            SpawnLoot();
            photonView.RPC(nameof(OpenChest), RpcTarget.All);
        }
    }

    [PunRPC]
    public void OpenChest()
    {
        isOpen = true;
        animator.SetBool(IsOpen, true);
    }

    public void SpawnLoot()
    {
        var items = ItemGenerator.Instance.CreateItems(5, _spawnPoint);

        LootExplosion.InstantiateExplosion(items);
    }
    
}