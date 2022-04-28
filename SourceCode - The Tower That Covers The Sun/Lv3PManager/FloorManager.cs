using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    public Animator anim;
    public TowerManager towerManager;
    public Lv3PManager puzzleManager;

    public Level3KeyBehavior key;

    public int floor;

    public void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    public void Update()
    {
        ActivateFloorAnimation();
    }

    public void ActivateFloorAnimation()
    {
        if (floor == 2)
        {
            SetAnimation();
        }
        if (floor == 3)
        {
            if (puzzleManager.completePOne)
            {
                SetAnimation();
            }
        }
        if (floor == 4)
        {
            if (puzzleManager.completePTwo)
            {
                SetAnimation();
            }
        }
    }

    public void SetAnimation ()
    {
        anim.SetBool("Rise", towerManager.rise);
    }

    public void SetShake ()
    {
        towerManager.ShakeCam();
    }

    public void CanMoveKey()
    {
        key.CanMove = true;
    }

    public void CantMoveKey()
    {
        key.CanMove = false;
    }
}
