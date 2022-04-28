using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    [SerializeField]
    private Animator transition;
    [SerializeField]
    private float fadeSpeed;
    [SerializeField]
    private GameObject skip;
    private CanvasGroup buttonCanvasGroup;
    private int nextLevel;

    //private bool _hasPlay;
    private VideoPlayer _videoPlayer;
    void Awake()
    {
        _videoPlayer = GetComponent<VideoPlayer>();
        buttonCanvasGroup = skip.GetComponent<CanvasGroup>();
        nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
        //if (_hasPlay)
        //{
        //    SkipVideo();
        //}
    }

    private void Start()
    {
        StartCoroutine(FadeIn(4f));
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Click");
            StartCoroutine(FadeIn(4f));
            
        }
        if ((_videoPlayer.frame) > 0 && (_videoPlayer.isPlaying == false))
        {
            StartCoroutine(SkipVideo(1f, nextLevel));
        }
    }


    public void SkipButton()
    {
        StartCoroutine(SkipVideo(1f, nextLevel));
    }
    IEnumerator SkipVideo(float time, int level)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(level);
    }

    IEnumerator FadeIn(float time)
    {
        Debug.Log("FadingIn");
        buttonCanvasGroup.interactable = true;
        while (buttonCanvasGroup.alpha < 1)
        {
            buttonCanvasGroup.alpha += fadeSpeed;
            yield return null;
        }
        yield return new WaitForSeconds(time);
        while (buttonCanvasGroup.alpha > 0)
        {
            buttonCanvasGroup.alpha -= fadeSpeed;
        }
        buttonCanvasGroup.interactable = false;  
    }
   
}
