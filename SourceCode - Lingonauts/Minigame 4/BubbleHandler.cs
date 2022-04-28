using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class BubbleHandler : MonoBehaviour
{
    public float ySpeed = 0, ySpeedRandom = 0, xSpeedRandom = 0;

    private float _xSpeed = 0;
    //private bool _isAlive = true;

    private SpriteRenderer theSpriteRenderer;
    Collider2D m_Collider;
    private Animator anim;
    public float A = 1f;
    void Start()
    {
        theSpriteRenderer = GetComponent<SpriteRenderer>();
        m_Collider = GetComponent<Collider2D>();
       
        ySpeed = ySpeed + Random.Range(-ySpeedRandom, ySpeedRandom);
        _xSpeed = Random.Range(-xSpeedRandom, xSpeedRandom);
        StartCoroutine(DisappearSec(7f));
        anim = GetComponent<Animator>();

    }
    void LateUpdate()
    {
        transform.Translate(-1 * _xSpeed * Time.deltaTime, ySpeed * Time.deltaTime, 0f);

    }


    //public void SetDirection(float dir)
    //{
    //    ySpeed *= dir;
    //}
    public void GetDown()
    {
        //_isAlive = false;
        m_Collider.enabled = false;
        ySpeed = 0f;
        StartCoroutine(DestroyAnimation(0f));
        StartCoroutine(DisappearSec(0.5f));

    }
    IEnumerator DisappearSec(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
        GameHandler4.bubblesCounter++;
    }
    IEnumerator DestroyAnimation(float time)
    {
        yield return new WaitForSeconds(time);
        A = 2f;
        anim.SetFloat("X", A);     
    }
}


