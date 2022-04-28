using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameObject handler, panel;
    public GameObject[] dragItems;
    public  static Coroutine co1;
    bool isCollision;
    Color theColor;
    public AudioSource audioSource;
    public AudioClip[] clips;
    

    private void OnTriggerEnter2D(Collider2D col)
    {
        audioSource.PlayOneShot(clips[0], 3f);
        theColor = col.gameObject.GetComponent<SpriteRenderer>().color;
        theColor.a = 1f;
        col.gameObject.GetComponent<SpriteRenderer>().color = theColor;
        Destroy(col.gameObject, 0.5f);
        Debug.Log("collision name = " + col.gameObject.name);
        StartCoroutine (ActivateDelay(1, panel));
        foreach (var item in dragItems)
        {
            if (item)
            {
                StartCoroutine(ActivateDelay(1, item));
                break;
            }

        }
        isCollision = true;
        
    }

    private void Start()
    {
        isCollision = false;
        co1 = StartCoroutine(SearchAgain(2));

    }
    private void Update()
    {
        if (isCollision)
        {
            Debug.Log("Stop");
            StopCoroutine(co1);
            isCollision = false;
        }
    }
        public void MoveUp()
    {
        if (GameHandler3.isFound && transform.position.y < 2)
        {
            audioSource.PlayOneShot(clips[1], 1.5f);
            transform.position += new Vector3(0, 3);
            if (GameHandler3.direction == "Up")
            {
                audioSource.PlayOneShot(clips[2], 3f);
            }
            else
            {
                audioSource.PlayOneShot(clips[3], 3f);
            }
            GameHandler3.direction = null;
            GameHandler3.isFound = false;
            co1 = StartCoroutine(SearchAgain(2));
        }
    }

    public void MoveDown()
    {
        if (GameHandler3.isFound && transform.position.y > -2)
        {
            audioSource.PlayOneShot(clips[1], 1.5f);
            transform.position += new Vector3(0, -3);
            if (GameHandler3.direction == "Down")
            {
                audioSource.PlayOneShot(clips[2], 3f);
            }
            else
            {
                audioSource.PlayOneShot(clips[3], 3f);
            }
            GameHandler3.direction = null;
            GameHandler3.isFound = false;
            co1 = StartCoroutine(SearchAgain(2));
        }
    }
    public void MoveLeft()
    {
        if (GameHandler3.isFound && transform.position.x > -5)
        {
            audioSource.PlayOneShot(clips[1], 1.5f);
            transform.position += new Vector3(-3, 0);
            if (GameHandler3.direction == "Left")
            {
                audioSource.PlayOneShot(clips[2], 3f);
            }
            else
            {
                audioSource.PlayOneShot(clips[3], 3f);
            }
            GameHandler3.direction = null;
            GameHandler3.isFound = false;
            co1 = StartCoroutine(SearchAgain(2));
        }
    }
    public void MoveRight()
    {
        if (GameHandler3.isFound && transform.position.x < 5)
        {
            audioSource.PlayOneShot(clips[1], 1.5f);
            transform.position += new Vector3(3, 0);
            if (GameHandler3.direction == "Right")
            {
                audioSource.PlayOneShot(clips[2], 3f);
            }
            else
            {
                audioSource.PlayOneShot(clips[3], 3f);
            }
            GameHandler3.direction = null;
            GameHandler3.isFound = false;
            co1 = StartCoroutine(SearchAgain(2));
        }
    }

    IEnumerator ActivateDelay(float time, GameObject item)
    {
        yield return new WaitForSeconds(time);
        item.SetActive(true);
    }
    public IEnumerator SearchAgain(float time)
    {
        yield return new WaitForSeconds(time);
        handler.GetComponent<GameHandler3>().CheckCloseDistance();
    }

}
