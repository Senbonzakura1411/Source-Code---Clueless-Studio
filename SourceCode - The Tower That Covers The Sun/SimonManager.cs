using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimonManager : MonoBehaviour
{
    private List<int> playerTaskList = new List<int>();
    private List<int> playerSequenceList = new List<int>();

    public List<List<Color32>> buttonColors = new List<List<Color32>>();
    public List<Button> clickableButtons;
    public CanvasGroup buttons;
    public GameObject startButton;

    public void Awake()
    {
        buttonColors.Add(new List<Color32> { new Color32(255, 100, 100, 255), new Color32 (255, 0, 0, 255)}); //add red
        buttonColors.Add(new List<Color32> { new Color32(255, 187, 109, 255), new Color32 (255, 136, 0, 255)}); //add Orange
        buttonColors.Add(new List<Color32> { new Color32(162, 255, 124, 255), new Color32 (72, 248, 0, 255)}); //add green
        buttonColors.Add(new List<Color32> { new Color32(57, 111, 255, 255), new Color32 (0, 70, 255, 255)}); //add blue
        for (int i=0;i<4;i++)
        {
            clickableButtons[i].GetComponent<Image>().color = buttonColors[i][0];
        }
    }

    public void AddToPlayerSequenceList(int buttonId)
    {
        playerSequenceList.Add(buttonId);
        StartCoroutine(HighlightButton(buttonId));
        for (int i=0;i<playerSequenceList.Count;i++)
        {
            if(playerTaskList[i] == playerSequenceList[i])
            {
                continue;
            }
            else
            {
                StartCoroutine(PlayerLost());
                return;
            }
        }
        if(playerSequenceList.Count == playerTaskList.Count)
        {
            StartCoroutine(StartNextRound());
        }
    }

    public void StartGame()
    {
        StartCoroutine(StartNextRound());
        startButton.SetActive(false);
    }

    public IEnumerator HighlightButton(int buttonId)
    {
        clickableButtons[buttonId].GetComponent<Image>().color = buttonColors[buttonId][1];
        //agregar sonido
        yield return new WaitForSeconds(0.5f);
        clickableButtons[buttonId].GetComponent<Image>().color = buttonColors[buttonId][0];
    }

    public IEnumerator PlayerLost()
    {
        //agregar sonido
        playerSequenceList.Clear();
        playerTaskList.Clear();
        yield return new WaitForSeconds(2f);
        startButton.SetActive(true);

    }

    public IEnumerator StartNextRound()
    {
        playerSequenceList.Clear();
        buttons.interactable=false;
        yield return new WaitForSeconds(1f);
        //agregar sonido
        playerTaskList.Add(Random.Range(0,4));
        foreach (int index in playerTaskList)
        {
            yield return StartCoroutine(HighlightButton(index));
        }
        buttons.interactable=true;
        yield return null;
    }
}
