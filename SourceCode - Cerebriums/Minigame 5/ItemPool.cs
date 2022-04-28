using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemPool : MonoBehaviour, IDropHandler
{
   
    public void OnDrop(PointerEventData eventData)
    {
        if (DragHandler.itemDragging == null) return;
        DragHandler.itemDragging.transform.SetParent(transform);
    }

}
