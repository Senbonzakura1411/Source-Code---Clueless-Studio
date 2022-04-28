using System;
using System.Collections.Generic;
using System.Diagnostics;
using Photon.Pun;
using Utils;
using Debug = UnityEngine.Debug;

public class InfoCollector : PunSingleton<InfoCollector>
{
    public List<string> playerNames;

    public List<float> weights;

    public List<float> values;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    
    [PunRPC]
    public void CollectData()
    {
        playerNames.Clear();
        weights.Clear();
        values.Clear();
        
        foreach (var player in PhotonNetwork.PlayerList)
        {
            if (player.CustomProperties.ContainsKey("Value") && player.CustomProperties.ContainsKey("Weight"))
            {   
                
                playerNames.Add(player.NickName);
                weights.Add((float) player.CustomProperties["Weight"]);
                values.Add((float) player.CustomProperties["Value"]);
            }
        }
    }
}