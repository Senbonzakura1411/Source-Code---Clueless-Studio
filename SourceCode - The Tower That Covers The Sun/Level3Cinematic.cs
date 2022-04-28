using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Cinematic : MonoBehaviour
{
    public bool isIntro;
    public bool isOutro;

    public Walkable firstStep;
    public Walkable lastStep;

    public PlayerController player;

    public GameObject canvas;

    public Level3KeyBehavior key;

    public Animator anim;

    public void Start()
    {
        GameManager.Instance.SetCinematic(true);
    }

    public void Update()
    {
    }

    public void SetIntro()
    {
        anim.enabled = false;
        canvas.SetActive(true);
        isIntro = false;
        GameManager.Instance.SetCinematic(false);
    }

    public void SetOutro()
    {
        GameManager.Instance.SetCinematic(true);
        canvas.SetActive(false);
        isOutro = true;
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

    public void CanMoveKey()
    {
        key.CanMove = true;
    }

    public void CantMoveKey()
    {
        key.CanMove = false;
    }

}
