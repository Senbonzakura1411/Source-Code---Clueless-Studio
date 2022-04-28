using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Cinematics : MonoBehaviour
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
        isIntro = true;
    }

    public void Update()
    {
        if (isIntro)
        {
            
            SetCinematicOne();
        }
    }

    public void SetCinematicOne()
    {
        player.CinematicPathFind(firstStep.transform);
        StartCoroutine(MoveToIntro());

    }

    public IEnumerator MoveToIntro()
    {
        yield return new WaitForSeconds(4f);
        canvas.SetActive(true);
        isIntro = false;
        GameManager.Instance.SetCinematic(false);
        //levelManager.cinematicMode = false;
    }

    public void StartFinalCinematic()
    {
        lastStep.imActive = true;
        lastStep.possiblePaths[1].active = true;
        GameManager.Instance.SetCinematic(true);
        canvas.SetActive(false);
        isOutro = true;
        player.CinematicPathFind(lastStep.transform);
        StartCoroutine(MoveToOutro());
    }

    public IEnumerator MoveToOutro()
    {
        yield return new WaitForSeconds(0.5f);
        anim.enabled = true;
        anim.SetBool("Active", true);
    }

    public void ChangeScene()
    {
GameManager.Instance.LoadNextScene();
    }

    public void SetRocketSound()
    {
AudioManager.Instance.Play("firecracker");
    }
}
