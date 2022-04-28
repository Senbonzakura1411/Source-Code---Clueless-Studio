using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerScripts
{
    public class InventorySlot : MonoBehaviour
    {
        public bool imFull;

        public Image myImage;

        public Sprite[] items;
        public string[] itemPrefabs;

        public GameObject player;

        public PlayerInventory inventory;

        public string itemName;

        public int itemId; // 0 es vacio y los demas son items

        public int itemValue;

        public float itemWeight;

        public void Update()
        {
            myImage.sprite = items[itemId];
        }

        public void GetItem(int iD, string name, int val, float weight)
        {
            itemId = iD;
            itemName = name;
            itemValue = val;
            itemWeight = weight;
        }

        public void DropItems()
        {
            var pos = new Vector3(player.transform.position.x, player.transform.position.y + 1, player.transform.position.z);
            PhotonNetwork.Instantiate(itemPrefabs[itemId], pos, Quaternion.identity);
            inventory.CheckWeight(-itemWeight);
            inventory.CheckValue(-itemValue);
            player = null;
            imFull = false;
            itemId = 0;
            itemName = "";
            itemValue = 0;
            itemWeight = 0;
        }


    }

}
