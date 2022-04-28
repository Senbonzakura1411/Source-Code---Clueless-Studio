using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    [SerializeField] private float daytimeCycleSpeed = 0.01f;
    [SerializeField] private AudioMixer audioMixer;
    private void Start()
    {
        StartCoroutine(StartDayNightCycle());
        AudioManager.Instance.Play("menu");
    }

    private IEnumerator StartDayNightCycle()
    {
        while (true)
        {
            GameManager.Instance.CycleTimeOfDay(daytimeCycleSpeed);
            yield return new WaitForSeconds(1.5f);
            GameManager.Instance.CycleTimeOfDay(daytimeCycleSpeed);
        }
    }

    public void PlayGame()
    {
        GameManager.Instance.LoadNextScene();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetVolume(float volume)
     {
        audioMixer.SetFloat("volume", volume);
        
     }
}