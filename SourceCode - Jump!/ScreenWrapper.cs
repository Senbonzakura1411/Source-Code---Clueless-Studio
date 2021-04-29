using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrapper : MonoBehaviour
{
    void OnBecameInvisible()
    {
        if (this.gameObject.activeSelf)
        {
            Vector2 thisPosition = transform.position;
            if (Camera.main.WorldToViewportPoint(transform.position).y > 1 || Camera.main.WorldToViewportPoint(transform.position).y < 0)
            {
                thisPosition.y *= -1;
            }
            if (Camera.main.WorldToViewportPoint(transform.position).x > 1 || Camera.main.WorldToViewportPoint(transform.position).x < 0)
            {
                thisPosition.x *= -1;    
            }
            transform.position = thisPosition;
        }
    }
}
