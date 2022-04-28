using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropHandler : MonoBehaviour, IDropHandler
{
    private GameObject _item;
    public string tagName;
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Drop");

        if (!_item)
        {
            _item = DragHandler.itemDragging;
            if (_item.tag == tagName)
            {
                GetComponent<AudioSource>().Play();
                _item.transform.SetParent(transform);
                _item.transform.position = transform.position;
                Destroy(_item, 0.5f);
                GameHandler5.score++;
            }
        }
    }

    private void Update()
    {
        if (_item != null && _item.transform.parent != transform)
        {
            Debug.Log("Remover");
            _item = null;
        }
    }

  
}
