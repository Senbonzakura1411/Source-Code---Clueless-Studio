using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int currency;
    public bool[] purchased;
    //public int level;
    //public int currentAvatar;


    public PlayerData(List<Shop.ShopItem> shopItems, int coins)
    {
        purchased = new bool[shopItems.Count];
        for (int i = 0; i < shopItems.Count; i++)
        {
            purchased[i] = shopItems[i].IsPurchased;
        }
        currency = coins;
    }

    
}
