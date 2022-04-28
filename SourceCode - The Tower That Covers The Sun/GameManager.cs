using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{

    [SerializeField][Range(0.01f, 0.05f)] private float daytimeDefaultTransitionSpeed = 0.025f;
    
    private DateTime _sessionStartTime;
    private DateTime _sessionEndTime;

    public bool IsCinematic { get;  private set; }

    public bool IsDay { get; private set; } = true;
    public bool IsTransitioning { get; private set; }
    private Coroutine _dayTimeCoroutine;

    private BOXOPHOBIC.Polyverse_Skies.Core.Runtime.PolyverseSkies _skyboxManager;

    private void Start()
    {
        
        _sessionStartTime = DateTime.Now;
        Debug.Log(
            "Game session start @: " + DateTime.Now);
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            LoadNextScene();
        }
    }
    
    private void OnApplicationQuit()
    {
        _sessionEndTime = DateTime.Now;

        TimeSpan timeDifference = 
            _sessionEndTime.Subtract(_sessionStartTime);
        
        Debug.Log(
            "Game session ended @: " + DateTime.Now);
        Debug.Log(
            "Game session lasted: " + timeDifference);
    }
    
    // Only for testing purposes, remove on build
      private void OnGUI()
      {
          if (GUI.Button(new Rect(10, 70, 100, 50), "Next Scene"))
          {
              LoadNextScene();
          }
      }

    #region Public Methods

    public void SetCinematic(bool value)
    {
        IsCinematic = value;
    }
    public void LoadNextScene()
    {
        IsTransitioning = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void CycleTimeOfDay()
    {
        AudioManager.Instance.Play(IsDay ? "transitionNight" : "transitionDay");
        
        if (_dayTimeCoroutine != null)
        {
            StopCoroutine(_dayTimeCoroutine);
        }
        
        if ((_skyboxManager == null))
        {
            _skyboxManager = GameObject.Find("Skybox Manager").GetComponent<BOXOPHOBIC.Polyverse_Skies.Core.Runtime.PolyverseSkies>();
        }

        _dayTimeCoroutine = StartCoroutine(IsDay == false ?
            SubstractToValueUntilLimit(_skyboxManager.timeOfDay, 0.05f, 0f, daytimeDefaultTransitionSpeed, x=>_skyboxManager.timeOfDay=x) : 
            AddToValueUntilLimit(_skyboxManager.timeOfDay, 0.05f, 1f, daytimeDefaultTransitionSpeed, x=>_skyboxManager.timeOfDay=x));
    }
    
    public void CycleTimeOfDay(float speed)
    {
        if (_dayTimeCoroutine != null)
        {
            StopCoroutine(_dayTimeCoroutine);
        }
        
        if ((_skyboxManager == null))
        {
            _skyboxManager = GameObject.Find("Skybox Manager").GetComponent<BOXOPHOBIC.Polyverse_Skies.Core.Runtime.PolyverseSkies>();
        }

        _dayTimeCoroutine = StartCoroutine(IsDay == false ?
            SubstractToValueUntilLimit(_skyboxManager.timeOfDay, 0.0001f, 0f, speed, x=>_skyboxManager.timeOfDay=x) : 
            AddToValueUntilLimit(_skyboxManager.timeOfDay, 0.0001f, 1f, speed, x=>_skyboxManager.timeOfDay=x));
    }

    #endregion

    #region Private Methods

    private IEnumerator AddToValueUntilLimit(float value, float step, float limit, float speed, Action<float> var)
    {
        IsTransitioning = true;
        while (value <= limit)
        {
            value += step;
            var(value);
            yield return new WaitForSeconds(speed);
        }
        IsTransitioning = false;
        IsDay = false;
        Debug.Log("day");
    }
    
    private IEnumerator SubstractToValueUntilLimit(float value, float step, float limit, float speed, Action<float> var)
    {
        IsTransitioning = true;
        while (value >= limit)
        {
            value -= step;
            var(value);
            yield return new WaitForSeconds(speed);
        }
        IsTransitioning = false;
        IsDay = true;
        Debug.Log("night");
    }

    #endregion


}