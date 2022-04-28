using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlThreeGoals : MonoBehaviour
{
    public int imGoal;
    public Lv3PManager puzzleManager;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        { 
            switch(imGoal)
            {
                case 1:
                    puzzleManager.completePOne = true;
                    other.gameObject.GetComponent<Level3KeyBehavior>().lastGoal = this.gameObject;
                    gameObject.SetActive(false);
                    break;
                case 2:
                    puzzleManager.completePTwo = true;
                    other.gameObject.GetComponent<Level3KeyBehavior>().lastGoal = this.gameObject;
                    gameObject.SetActive(false);
                    break;
                case 3:
                    puzzleManager.completePThree = true;
                    other.gameObject.GetComponent<Level3KeyBehavior>().lastGoal = this.gameObject;
                    gameObject.SetActive(false);
                    other.gameObject.SetActive(false);
                    break;
            }
        }
    }
}
