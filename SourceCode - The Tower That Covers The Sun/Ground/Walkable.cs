using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walkable : MonoBehaviour
{
    public List<WalkPath> possiblePaths = new List<WalkPath>();

    public Transform previousBlock;


    public bool isStair = false;

    public bool imActive;


    public float walkPointOffset = 0.5f;
    public float stairOffset = 0f;

    public Vector3 GetWalkPoint ()
    {
        float stair = isStair ? stairOffset : 0;
        return transform.position + transform.up * walkPointOffset - transform.up * stair;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        float stair = isStair ? 0.4f : 0;
        Gizmos.DrawSphere(GetWalkPoint(), 0.1f);

        if (possiblePaths == null)
            return;

        foreach (WalkPath p in possiblePaths)
        {
            if (p.target == null)
                return;
            Gizmos.color = p.active ? Color.black : Color.clear;
            Gizmos.DrawLine(GetWalkPoint(), p.target.GetComponent<Walkable>().GetWalkPoint());
        }
    }
}

[System.Serializable]
public class WalkPath
{
    public Transform target;
    public bool active = true;
} 
