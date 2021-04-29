using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public bool isActivated;
    bool inRange;
    [SerializeField] GameObject item;
    [SerializeField] Sprite[] images;

    private void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.F) && !isActivated)
        {
            isActivated = true;
            item.SetActive(true);
            AudioManager.instance.Play("PAint");
            gameObject.GetComponent<SpriteRenderer>().sprite = images[1];
        }
        else if (inRange && Input.GetKeyDown(KeyCode.F))
        {
            isActivated = false;
            item.SetActive(false);
            AudioManager.instance.Play("PAint");
            gameObject.GetComponent<SpriteRenderer>().sprite = images[0];
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        inRange = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        inRange = false;
    }
}
