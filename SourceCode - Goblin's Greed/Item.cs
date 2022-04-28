using Photon.Pun;
using UnityEngine;
using Random = UnityEngine.Random;

public class Item : MonoBehaviourPunCallbacks
{
    [SerializeField] public int value;
    [SerializeField] public float weight;
    
    public bool isRare;
    public bool isFake;
    public string itemName;
    public int itemId;


    private void Start()
    {
        if (isRare)
        {
            value = Random.Range(500, 1000);
            weight = Random.Range(10f, 20f);
        }
        else if (isFake)
        {
            value = 0;
            weight = Random.Range(10f, 20f);
        }
        else
        {
            value = Random.Range(10, 250);
            weight = Random.Range(1f, 10f);
        }
    }
    [PunRPC]
    public void DestroyItem()
    {
        PhotonNetwork.Destroy(gameObject);
    }
}