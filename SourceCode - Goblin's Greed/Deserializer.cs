using System;
using System.Linq;
using TMPro;
using UnityEngine;

public class Deserializer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] names;

    [SerializeField] private TextMeshProUGUI[] valuesText;

    [SerializeField] private TextMeshProUGUI[] weightsText;

    [SerializeField] private TextMeshProUGUI winnerText;
    

    private InfoCollector _collector;

    private void Awake()
    {
        _collector = FindObjectOfType<InfoCollector>();

        for (var i = 0; i < _collector.playerNames.Count; i++)
        {
            names[i].text = _collector.playerNames[i];
        }

        for (var i = 0; i < _collector.values.Count; i++)
        {
            {
                valuesText[i].text = Mathf.Round(_collector.values[i]).ToString();
            }
        }

        for (var i = 0; i < _collector.weights.Count; i++)
        {
            {
                weightsText[i].text = Mathf.Round(_collector.weights[i]).ToString();
            }
        }

        winnerText.text = GetWinner();
    }

    private string GetWinner()
    {
        for (var i = 0; i < _collector.values.Count; i++)
        {
            if (Math.Abs(_collector.values[i] - _collector.values.Max()) < 0.0001f)
            {
                return _collector.playerNames[i];
            }
        }

        return null;
    }
}