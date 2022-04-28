using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState
{
    public abstract void Enter();
    public abstract Type Tick();
    public abstract void Exit();

    public RaycastHit hit;
    public virtual void MouseAction()
    {
        throw new NotImplementedException();
    }
    public void CursorPoint(string tag)
    {

        Ray ray = Camera.main.ScreenPointToRay((Input.mousePosition));
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag(tag) && Input.GetKeyDown(KeyCode.Mouse0))
            {
                MouseAction();
            }
            else if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                AudioManager.instance.Play("ToyL3");
            }
        }
    }
}
