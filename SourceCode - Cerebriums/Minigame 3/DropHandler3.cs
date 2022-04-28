using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropHandler3 : MonoBehaviour, IDropHandler

{
    public GameObject handler, player, panel;
    private GameObject _item;
    public string tagName;
    public Coroutine co2;
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Drop");

        if (!_item)
        {
            _item = DragHandler3.itemDragging;
            if (_item.tag == tagName)
            {
                GetComponent<AudioSource>().Play();
                _item.transform.SetParent(transform);
                _item.transform.position = transform.position;
                Destroy(_item, 0.5f);
                GameHandler3.treasureCount += 1;
                StartCoroutine(player.GetComponent<PlayerController>().SearchAgain(1));
                StartCoroutine(ActivateDelay(1, panel));
            }
        }
    }
    void Update()
    {
        if (_item != null && _item.transform.parent != transform)
        {
            Debug.Log("Remover");
            _item = null;
        }
    }

    IEnumerator ActivateDelay(float time, GameObject item)
    {
        yield return new WaitForSeconds(time);
        item.SetActive(false);
    }
}
