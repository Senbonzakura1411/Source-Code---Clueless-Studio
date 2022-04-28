using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackQuad : MonoBehaviour
{
    private Renderer render;
    Vector2 offSet;

    public float xVel;
    public float yVel;

    public void Awake()
    {
        render = gameObject.GetComponent<Renderer>();
    }

    public void Update()
    {
        offSet = new Vector2(xVel, yVel);
        render.material.mainTextureOffset += offSet * Time.deltaTime;
    }
}
