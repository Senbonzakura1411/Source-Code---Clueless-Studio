using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SIMONTEST : MonoBehaviour

{
    [SerializeField] private List<int> playerTaskList = new List<int>();
    [SerializeField] List<int> playerSequenceList = new List<int>();
    [SerializeField] private List<Color32> buttonColors;
    [SerializeField] private Transform[] towersPositions;
    [SerializeField] private GameObject[] waterfalls;
    public List<GameObject> clickableButtons;
    public GameObject fakeLight1;
    public GameObject fakeLight2;
    public GameObject startButton;
    public GameObject mainLight;
    public bool buttonsClickable;
    private float _towerSpeed = 20f;
    private Vector3[] _startPos;

    public Level2Cinematic cinematic;


    private void Start()
    {
        AudioManager.Instance.Pause("nightTheme1");
        AudioManager.Instance.Play("dayTheme2");
        buttonsClickable = false;
        _startPos = new[] {towersPositions[0].position, towersPositions[1].position, towersPositions[2].position};
    }

    public void AddToPlayerSequenceList(int buttonId)
    {
        playerSequenceList.Add(buttonId);
        StartCoroutine(HighlightButton(buttonId));

        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("cycle daytime");
            GameManager.Instance.CycleTimeOfDay();
        }

        if (playerTaskList[playerSequenceList.Count - 1] != playerSequenceList[playerSequenceList.Count - 1])
        {
            StartCoroutine(PlayerLost());
            return;
        }

        if (playerSequenceList.Count == playerTaskList.Count)
        {
            if (!CheckGameStatus())
            {
                StartCoroutine(StartNextRound());
            }
        }
    }

    private bool CheckGameStatus()
    {
        if (playerSequenceList.Count >= 1)
        {
            waterfalls[0].SetActive(true);
            _towerSpeed *= 1.4f;
        }

        if (playerSequenceList.Count >= 3)
        {
            waterfalls[1].SetActive(true);
            _towerSpeed *= 1.4f;
        }

        if (playerSequenceList.Count == 5)
        {
            waterfalls[2].SetActive(true);
            AudioManager.Instance.Play("simonComplete");
            StartCoroutine(StopGame());
            return true;
        }

        return false;
    }

    public void StartGame()
    {
        AudioManager.Instance.Play("simonStart");
        mainLight.GetComponent<MeshRenderer>().material.color = Color.white;
        StartCoroutine(StartNextRound());
        startButton.SetActive(false);
    }

    public IEnumerator HighlightButton(int buttonId)
    {
        clickableButtons[buttonId].SetActive(false);
        AudioManager.Instance.Play("buttonSeq1");
        yield return new WaitForSeconds(0.5f);
        clickableButtons[buttonId].SetActive(true);
    }

    public IEnumerator HighlightLight(int buttonId, GameObject Light)
    {
        Light.GetComponent<MeshRenderer>().material.color = buttonColors[buttonId];
        Light.transform.Rotate(180.0f, 0.0f, 0.0f, Space.Self);
        AudioManager.Instance.Play("sequence1");
        yield return new WaitForSeconds(0.5f);
    }

    public IEnumerator TurnOffLight(GameObject Light)
    {
        Light.GetComponent<MeshRenderer>().material.color = Color.black;
        yield return new WaitForSeconds(0.5f);
    }

    public IEnumerator PlayerLost()
    {
        AudioManager.Instance.Play("simonStop");
        playerSequenceList.Clear();
        playerTaskList.Clear();
        waterfalls[0].SetActive(false);
        waterfalls[1].SetActive(false);
        waterfalls[2].SetActive(false);
        _towerSpeed = 20f;
        buttonsClickable = false;
        yield return (ResetTowers(_towerSpeed));
        yield return new WaitForSeconds(2f);
        startButton.SetActive(true);
    }

    public IEnumerator StartNextRound()
    {
        playerSequenceList.Clear();
        buttonsClickable = false;
        playerTaskList.Add(Random.Range(0, 4));
        GameManager.Instance.CycleTimeOfDay();
        yield return new WaitForSeconds(1f);
        yield return TurnOffLight(mainLight);
        yield return StartCoroutine(ShuffleTowers(_towerSpeed));
        yield return new WaitForSeconds(1f);
        // Agregar sonido

        foreach (int index in playerTaskList)
        {
            StartCoroutine(HighlightLight(index, mainLight));
            StartCoroutine(HighlightLight(Random.Range(0, 4), fakeLight1));
            yield return StartCoroutine(HighlightLight(Random.Range(0, 4), fakeLight2));
            StartCoroutine(TurnOffLight(mainLight));
            StartCoroutine(TurnOffLight(fakeLight1));
            StartCoroutine(TurnOffLight(fakeLight2));
            yield return new WaitForSeconds(1f);
        }

        GameManager.Instance.CycleTimeOfDay();
        yield return new WaitForSeconds(0.5f);
        buttonsClickable = true;
        yield return null;
    }

    public IEnumerator StopGame()
    {
        playerSequenceList.Clear();
        playerTaskList.Clear();
        Debug.Log("Juego Terminado");
        yield return new WaitForSeconds(2f);
        startButton.SetActive(false);
        // Comienza Cinematica
        cinematic.StartOutro();
    }

    public IEnumerator ShuffleTowers(float speed)
    {
        var steps = Random.Range(playerTaskList.Count, playerTaskList.Count * 3);

        for (var i = 0; i < steps; i++)
        {
            Vector3[] pos = {towersPositions[0].position, towersPositions[1].position, towersPositions[2].position};
            StartCoroutine(MoveObjectToTarget(towersPositions[0], pos[0], pos[1], speed));
            StartCoroutine(MoveObjectToTarget(towersPositions[1], pos[1], pos[2], speed));
            yield return StartCoroutine(MoveObjectToTarget(towersPositions[2], pos[2], pos[0], speed));
        }
    }

    private IEnumerator ResetTowers(float speed)
    {
        Vector3[] pos = {towersPositions[0].position, towersPositions[1].position, towersPositions[2].position};
        StartCoroutine(MoveObjectToTarget(towersPositions[0], pos[0], _startPos[0], speed));
        StartCoroutine(MoveObjectToTarget(towersPositions[1], pos[1], _startPos[1], speed));
        yield return StartCoroutine(MoveObjectToTarget(towersPositions[2], pos[2], _startPos[2], speed));
    }


    private IEnumerator MoveObjectToTarget(Transform objTransform, Vector3 startPos, Vector3 target, float speed)
    {
        var startTime = Time.time;
        var journeyLength = Vector3.Distance(startPos, target);

        for (int i = 0; i < 60; i++)
        {
            float distCovered = (Time.time - startTime) * speed;
            float fractionOfJourney = distCovered / journeyLength;
            objTransform.position = Vector3.Lerp(startPos, target, fractionOfJourney);
            yield return new WaitForSeconds(0.016f);
        }
    }
}