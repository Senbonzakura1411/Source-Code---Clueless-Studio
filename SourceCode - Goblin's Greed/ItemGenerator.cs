using Photon.Pun;
using UnityEngine;
using Utils;

public class ItemGenerator : Singleton<ItemGenerator>
{
    [SerializeField] private string[] items;
    [SerializeField] private string[] rareItems;

    private GameObject[] _createdItems;
    public GameObject[] CreateItems(int quantity, Vector3 position)
    {
        _createdItems = new GameObject[quantity];

        for (var i = 0; i < quantity; i++)
        {
            if (Random.value > 0.95)
            {
                CreateRareItem(i, position);
            }
            else
            {
                CreateCommonItem(i, position);
            }
        }
        
        return _createdItems;
    }

    private void CreateCommonItem(int i, Vector3 pos)
    {
        _createdItems[i] = PhotonNetwork.Instantiate(items[Random.Range(0, items.Length)], pos, Quaternion.identity);
        var item = _createdItems[i].GetComponent<Item>();
        item.isRare = false;
    }

    private void CreateRareItem(int i, Vector3 pos)
    {
        _createdItems[i] = PhotonNetwork.Instantiate(rareItems[Random.Range(0, rareItems.Length)], pos, Quaternion.identity);
        var item = _createdItems[i].GetComponent<Item>();
        item.isRare = true;
    }
    
    public void CreateFakeItem(Vector3 pos)
    {
        var createdItem= PhotonNetwork.Instantiate(rareItems[Random.Range(0, rareItems.Length)], pos, Quaternion.identity);
        var item = createdItem.GetComponent<Item>();
        item.isFake = true;
        LootExplosion.InstantiateExplosion(item.gameObject);
    }
}