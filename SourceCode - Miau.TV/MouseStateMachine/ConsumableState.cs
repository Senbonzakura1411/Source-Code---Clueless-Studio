using System;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableState : BaseState
{
    private Mouse _mouse;
    public ConsumableState(Mouse mouse)
    {
        this._mouse = mouse;
    }

    public override void Enter()
    {
        Cursor.SetCursor(_mouse.pointers[2], Vector2.zero, CursorMode.Auto);
    }
    public override Type Tick()
    {
        CursorPoint("Poop");

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            return typeof(EmptyState);
        }

        return null;
    }

    public override void Exit()
    {
        Debug.Log("ConsumableExit");
    }
    public override void MouseAction()
    {
        UnityEngine.Object.Destroy(hit.collider.gameObject);
        AudioManager.instance.Play("UI1");
    }

}
