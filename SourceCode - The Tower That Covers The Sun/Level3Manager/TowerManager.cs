using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerManager : MonoBehaviour
{

    public CinemachineShake shake;
    public Lv3PManager puzzleManager;
    [SerializeField] private Button sunButton, moonButton;
    public bool levelComplete;
    public Level3KeyBehavior key;

    public bool rise;

    private void Update()
    {
        CheckLevelComplete();
        if (levelComplete)
        {
            RiseTower();
        }     
    }
    public void RiseTower()
    {
        rise = true;
    }

    public void HideTower()
    {
        rise = false;
    }

    public void ShakeCam()
    {
        shake.ShakeCamera(1f, 1.6f);
    }

    public void CheckLevelComplete ()
    {
        if (puzzleManager.completePOne && puzzleManager.completePTwo && puzzleManager.completePThree)
        {
            levelComplete = true;
        }
    }

    public void CycleTime()
    {
        if (key.CanMove)
        {
            key.CanMove = false;
            if (GameManager.Instance.IsDay)
            {
                if (!levelComplete)
                    HideTower();
                sunButton.interactable = false;
                sunButton.GetComponent<Image>().raycastTarget = false;
                moonButton.interactable = true;
                moonButton.GetComponent<Image>().raycastTarget = true;
            }
            else
            {
                if (!levelComplete)
                    RiseTower();
                sunButton.interactable = true;
                sunButton.GetComponent<Image>().raycastTarget = true;
                moonButton.interactable = false;
                moonButton.GetComponent<Image>().raycastTarget = false;
            }
            GameManager.Instance.CycleTimeOfDay();
        }  
    }
}
