using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class TitanHPGUI : MonoBehaviour
{
    [SerializeField] Enemy titan;
    [SerializeField] Image titanHP;
    void Update()
    {
        titanHP.fillAmount = titan.EnemyHP * 0.0001f; 
    }
  
}
