using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;



public static class SerializationManager
{
    public static bool SaveData(string saveName, List<Shop.ShopItem> shopItems, int coins)
    {
        BinaryFormatter formatter = GetBinaryFormatter();

        if (!Directory.Exists(Application.persistentDataPath + "/saves"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/saves");
        }
        string path = Application.persistentDataPath + "/saves/" + saveName + ".save";
        FileStream stream = File.Create(path);

        PlayerData data = new PlayerData(shopItems, coins);

        formatter.Serialize(stream, data);
        stream.Close();
        return true;
    }

    public static PlayerData Load(string path)
    {
        if (!File.Exists(path))
        {
            return null;
        }
        else
        {
            BinaryFormatter formatter = GetBinaryFormatter();
            FileStream stream = File.Open(path, FileMode.Open);

            try
            {
                PlayerData save = formatter.Deserialize(stream) as PlayerData;
                stream.Close();
                return save;
            }
            catch
            {
                Debug.LogErrorFormat("Failed to load file at {0}", path);
                stream.Close();
                return null;
            }
        }
    }

    public static BinaryFormatter GetBinaryFormatter()
    {
        BinaryFormatter formatter = new BinaryFormatter();

        return formatter;
    }

}
