using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler3 : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static GameObject itemDragging;

    public AudioClip incorrect;
    public Transform dragParent;

    private Vector3 _startPosition;
    private Transform _startParent;
    private CanvasGroup _canvasGroup;




    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        dragParent = dragParent.transform;
    }

    #region DragFunctions

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        GetComponent<AudioSource>().Play();
        itemDragging = gameObject;

        _startPosition = transform.position;
        _startParent = transform.parent;
        transform.SetParent(dragParent);

        _canvasGroup.blocksRaycasts = false;


    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        itemDragging = null;

        _canvasGroup.blocksRaycasts = true;
        if (transform.parent == dragParent)
        {
            transform.position = _startPosition;
            transform.SetParent(_startParent);
            GetComponent<AudioSource>().PlayOneShot(incorrect);
        }
    }

    #endregion

    private void Update()
    {

    }
}
