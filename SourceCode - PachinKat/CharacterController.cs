using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] float vel;

     Vector2 startPos;

    private void Start()
    {
        startPos = transform.position;
    }
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        Vector2 position = transform.position;
        position.x = position.x + vel * horizontal * Time.deltaTime;
        transform.position = position;

        if (horizontal < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (horizontal > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    public void ResetPlayerPosition()
    {
        transform.position = startPos;
    }
}
