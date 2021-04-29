using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Parallax : MonoBehaviour
{
    [SerializeField]  float parallaxspeed;
    [SerializeField] RawImage background;


    private void Update()
    {
        ParallaxEffect();
    }
    private void ParallaxEffect()
    {
        float finalspeed = parallaxspeed * Time.deltaTime;
        background.uvRect = new Rect(background.uvRect.x + finalspeed, 0f, 1f, 1f);
    }
}
