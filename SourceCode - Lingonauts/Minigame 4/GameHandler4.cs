using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using JetBrains.Annotations;
using UnityEngine.UI;

public class GameHandler4 : MonoBehaviour
{

    private int _bubblesSpawns;
    public int maxSpawns = 5;
    private int bubblesSpawned;
    public GameObject[] bubbles;
    private int score = 0;
    public TextMeshProUGUI scoreText;
    private bool _notGameOver;
    private bool tappedScreen = false;
    private Vector2 tappedPos;
    float pos = -30;
    //float dir = 1;
    private float timer;
    //public TextMeshProUGUI timerText;
    public Button exitButton;
    public GameObject finalMessage;
    public GameObject startMessage;
    public TextMeshProUGUI finalMessageText;
    public AudioClip[] pronounsSounds;
    public AudioClip correct;
    public AudioClip incorrect;
    public AudioSource audioSource;
    public TextMeshProUGUI pronounText;
    public string[] pronouns;
    private string _chosenPronoun;
    private string correctTag;
    private bool _isNotTurn;
    private int turnReset;
    private int _nextLevel = 3;
    public static int bubblesCounter;
    private void Start()
    {
        Time.timeScale = 0f;
        startMessage.SetActive(true);
        exitButton.interactable = false;
        bubblesCounter = 0;
        score = 0;
        _notGameOver = true;
        _isNotTurn = true;
        bubblesSpawned = 0;
        turnReset = 0;


    }
    private void Update()
    {
        Debug.Log(_notGameOver);
        timer = 40f - Time.timeSinceLevelLoad;
        //timerText.text = "Tiempo:" + Mathf.Round(timer);
        if (Mathf.Round(timer) == 0 || score == 1500 || turnReset == 6)
        {
            Debug.Log("Game Over");
            _notGameOver = false;
        }
        if (_notGameOver)
        {
            #region BubbleSpawner & Turn Reset
            if (_isNotTurn == false && bubblesCounter == bubblesSpawned)
            {
                Debug.Log("TurnReset");
                turnReset++;
                _chosenPronoun = null;
                correctTag = null;
                StartCoroutine(SetTurn(0f));
                _isNotTurn = true;
                bubblesSpawned = 0;
                bubblesCounter = 0;
            }
            else if (_isNotTurn)
            {
                if (_chosenPronoun == "He" || _chosenPronoun == "She" || _chosenPronoun == "It")
                {
                    correctTag = "IsPronouns";
                    SpawnBubble(2);
                    for (int i = 0; i < _bubblesSpawns; i++)
                    {
                        SpawnBubble(Random.Range(0, bubbles.Length));
                    }
                    if (_bubblesSpawns < maxSpawns)
                    {
                        _bubblesSpawns++;
                    }

                    _isNotTurn = false;

                }
                else if (_chosenPronoun == "You" || _chosenPronoun == "We" || _chosenPronoun == "They")
                {
                    correctTag = "ArePronouns";
                    SpawnBubble(1);
                    for (int i = 0; i < _bubblesSpawns; i++)
                    {
                        SpawnBubble(Random.Range(0, bubbles.Length));
                    }
                    if (_bubblesSpawns < maxSpawns)
                    {
                        _bubblesSpawns++;
                    }

                    _isNotTurn = false;


                }
                else if (_chosenPronoun == "I")
                {
                    correctTag = "AmPronouns";
                    SpawnBubble(0);
                    for (int i = 0; i < _bubblesSpawns; i++)
                    {
                        SpawnBubble(Random.Range(0, bubbles.Length));
                    }
                    if (_bubblesSpawns < maxSpawns)
                    {
                        _bubblesSpawns++;
                    }

                    _isNotTurn = false;
                }

            }
            #endregion

            #region TapHandler
            tappedScreen = Input.GetMouseButtonDown(0);

            if (tappedScreen)
            {
                tappedPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D tap = Physics2D.Raycast(tappedPos, tappedPos, 0f);

                if (tap && tap.collider.gameObject.layer == LayerMask.NameToLayer("Game"))
                {
                    tap.collider.SendMessage("GetDown", SendMessageOptions.DontRequireReceiver);
                    if (tap.collider.gameObject.CompareTag(correctTag))
                    {
                        score += 100;
                        scoreText.text = "Puntos = " + score;
                        audioSource.PlayOneShot(correct, 1);
                    }
                    else
                    {
                        score -= 50;
                        scoreText.text = "Puntos = " + score;
                        audioSource.PlayOneShot(incorrect, 1);
                    }
                }
            }
        }
        #endregion

        if (!_notGameOver)
        {
            Time.timeScale = 0f; 
            exitButton.interactable = false;
            finalMessageText.text = "¡Felicidades!\n\n Completaste el minijuego.\n\n" +
                "Tu marcador final fue de " + score + " puntos.\n\n Has recibido " + score/10 + " Verbis.";          
            finalMessage.SetActive(true);
            
        }
        Debug.Log("Turn " + _isNotTurn);
        Debug.Log("deadbubbles " + bubblesCounter);
        Debug.Log("bubbles " + _bubblesSpawns);

    }

    public void SpawnBubble(int bubble)
    {
        bubblesSpawned++;
        float newScale = Random.Range(8f, 12f);
        Vector3 randomPositionX = new Vector3(Random.Range(-25, 25), pos);
        GameObject newBubble = Instantiate(bubbles[bubble], randomPositionX, Quaternion.identity);
        newBubble.transform.localScale = new Vector3(newScale, newScale, 1f);

        //if (Hard)
        //{
        //  newBubble.SendMessage("SetDirection", dir, SendMessageOptions.DontRequireReceiver);
        //  dir *= -1;
        //  pos *= -1;
        //}

    }


    public void OnOkClick()
    {
        turnReset = 0;
        timer = 40f;
        if (LevelManager.reachedLevel < _nextLevel)
        {
            LevelManager.reachedLevel = _nextLevel;
        }
        Game.Instance.Coins += score / 10;
        score = 0;      
        bubblesCounter = 0;  
        SceneManager.LoadScene(5);
        Time.timeScale = 1f;
    }


    public void OnStartClick()
    {
        startMessage.SetActive(false);
        exitButton.interactable = true;
        Time.timeScale = 1f;
        StartCoroutine(SetTurn(0f));
    }
    IEnumerator SetTurn(float time)
    {
        yield return new WaitForSeconds(time);
        pronounText.text = pronouns[Random.Range(0, pronouns.Length)];
        _chosenPronoun = pronounText.text;
        if (turnReset <= 5)
        {
            if (_chosenPronoun == pronouns[0]) audioSource.PlayOneShot(pronounsSounds[0], 2);
            if (_chosenPronoun == pronouns[1]) audioSource.PlayOneShot(pronounsSounds[1], 2);
            if (_chosenPronoun == pronouns[2]) audioSource.PlayOneShot(pronounsSounds[2], 2);
            if (_chosenPronoun == pronouns[3]) audioSource.PlayOneShot(pronounsSounds[3], 2);
            if (_chosenPronoun == pronouns[4]) audioSource.PlayOneShot(pronounsSounds[4], 2);
            if (_chosenPronoun == pronouns[5]) audioSource.PlayOneShot(pronounsSounds[5], 2);
            if (_chosenPronoun == pronouns[6]) audioSource.PlayOneShot(pronounsSounds[6], 2);
        }
    }
}
