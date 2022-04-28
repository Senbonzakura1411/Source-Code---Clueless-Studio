using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadManager : MonoBehaviour
{
    public LevelManager lM;
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
        if (lM.player._speed <= 0)
        {
            xVel = 0;
        }
        else
        {
            xVel = 0.2f;
        }
        offSet = new Vector2(xVel, yVel);
        render.material.mainTextureOffset += offSet * Time.deltaTime;
    }
}
