using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConductivityLever : MonoBehaviour
{
    public bool isActivated;
    [SerializeField] RightLeftPlatform platform;
    bool inRange;
    [SerializeField]Transform maxLeverRange;
    [SerializeField] Sprite[] images;

    private void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.F) && !isActivated)
        {
            isActivated = true;
            platform.maxRange = maxLeverRange;
            AudioManager.instance.Play("PAint");
            gameObject.GetComponent<SpriteRenderer>().sprite = images[1];
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
