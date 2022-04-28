using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class SaveHandler : MonoBehaviour
{
    readonly static string path = "Cerebriums";
    [SerializeField]
    private Animator transition;
    public  void Save()
    {
        SerializationManager.SaveData(path, Shop.Instance.ShopItemsList, Game.Instance.Coins);
        Debug.Log("Saved");
    }

    public  void Load()
    {
        Debug.Log("Loaded");
        PlayerData data = SerializationManager.Load(Application.persistentDataPath + "/saves/" + path + ".save");
        for (int i = 0; i < data.purchased.Length; i++)
        {
            Shop.Instance.ShopItemsList[i].IsPurchased = data.purchased[i];
        }
        Game.Instance.Coins = data.currency;
    }

    public void ConsoleButton()
    {
        StartCoroutine(SkipVideo(1f,5));
    }
    IEnumerator SkipVideo(float time, int level)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(level);
    }
}
