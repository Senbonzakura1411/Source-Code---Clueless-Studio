using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public static GameObject itemDragging;
    public AudioClip incorrect;


    private int _indexNumber;
    private Vector3 _startPosition;
    private Transform _startParent, _dragParent;
    private CanvasGroup _canvasGroup;



    private void Start()
    {
        //Initialise the Sibling Index to Randoms
        _indexNumber = Random.Range((int)1f, (int)10f);
        //Set the Sibling Index
        transform.SetSiblingIndex(_indexNumber);
        _canvasGroup = GetComponent<CanvasGroup>();
        _dragParent = GameObject.FindGameObjectWithTag("DragParent").transform;
    }

    #region DragFunctions

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        GetComponent<AudioSource>().Play();
        itemDragging = gameObject;

        _startPosition = transform.position;
        _startParent = transform.parent;
        transform.SetParent(_dragParent);

        _canvasGroup.blocksRaycasts = false;


    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        itemDragging = null;

        _canvasGroup.blocksRaycasts = true;
        if (transform.parent == _dragParent)
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