using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Cinematic : MonoBehaviour
{
    public bool isIntro;
    public bool isOutro;

    public Walkable firstStep;
    public Walkable lastStep;

    public PlayerController player;

    public GameObject canvas;

    public Animator anim;

    public void Start()
    {
        GameManager.Instance.SetCinematic(true);
    }

    public void Update()
    {
        if (isIntro)
        {
            SetCinematicOne();
        }
    }

    public void SetIntro ()
    {
        isIntro = true;
    }

    public void SetCinematicOne()
    {
        anim.enabled = false;
        player.CinematicPathFind(firstStep.transform);
        StartCoroutine(MoveToIntro());

    }

    public IEnumerator MoveToIntro()
    {
        yield return new WaitForSeconds(2f);
        lastStep.imActive = false;
        lastStep.possiblePaths[0].active = false;
        canvas.SetActive(true);
        isIntro = false;
        GameManager.Instance.SetCinematic(false);
        //levelManager.cinematicMode = false;
    }

    public void StartOutro()
    {
        lastStep.imActive = true;
        lastStep.possiblePaths[0].active = true;
        isOutro = true;
        GameManager.Instance.SetCinematic(true);
        canvas.SetActive(false);
        isOutro = true;
        player.CinematicPathFind(lastStep.transform);
        StartCoroutine(MoveToOutro());
    }



    public IEnumerator MoveToOutro()
    {
        yield return new WaitForSeconds(2f);
        anim.enabled = true;
        anim.SetBool("Active", true);
    }

    public void ChangeScene()
    {
      GameManager.Instance.LoadNextScene();
    }
}
