using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BubbleText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        var parent = transform.parent;

        var parentRenderer = parent.GetComponent<Renderer>();
        var renderer = GetComponent<Renderer>();

        gameObject.transform.position = parent.position;
        renderer.sortingLayerID = parentRenderer.sortingLayerID;
        renderer.sortingOrder = parentRenderer.sortingOrder;

    }

}