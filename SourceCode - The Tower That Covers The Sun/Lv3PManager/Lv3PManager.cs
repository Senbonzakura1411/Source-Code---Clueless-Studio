using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lv3PManager : MonoBehaviour
{
    public bool completePOne;
    public bool completePTwo;
    public bool completePThree;

    public GameObject firstGoal;
    public GameObject secondGoal;
    public GameObject thirdGoal;

    public GameObject obstacleOne;
    public GameObject obstacleTwo;
    public GameObject obstacleThree;
    public GameObject obstacleFour;

    public GameObject paths;
    public GameObject initialPath;

    public GameObject ending;

    public void Update()
    {
        TrackCompletePuzzles();
    }

    public void TrackCompletePuzzles()
    {
        if (completePOne)
        {
            secondGoal.SetActive(true);
            firstGoal.SetActive(false);
        }
        if (completePTwo)
        {
            thirdGoal.SetActive(true);
            secondGoal.SetActive(false);
        }
        if (completePThree)
        {
            thirdGoal.SetActive(false);

            obstacleOne.SetActive(false);
            obstacleTwo.SetActive(false);
            obstacleThree.SetActive(false);
            obstacleFour.SetActive(false);

            ending.SetActive(true);

            paths.SetActive(true);
        }
    }

}
