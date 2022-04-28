using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingScreen : MonoBehaviour
{
    public GameObject endingScreen;
    public GameControl gC;

    public void Start()
    {
        gC = GameControl.GetInstance();
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(CallOtherScreen());
        }
    }

    public IEnumerator CallOtherScreen ()
    {
        endingScreen.SetActive(true);
        yield return new WaitForSeconds(2.2f);
        gC.RestartValues();
        SceneManager.LoadScene("Ending");
    }
}
