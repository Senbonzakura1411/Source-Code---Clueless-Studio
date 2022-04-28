using System;
using System.Collections.Generic;
using UnityEngine;

public class EmptyState : BaseState 
{
    private Mouse _mouse;
    public EmptyState(Mouse mouse)
    {
        this._mouse = mouse;
    }

    public override void Enter()
    {
        Cursor.SetCursor(_mouse.pointers[0], Vector2.zero, CursorMode.Auto);
        AudioManager.instance.Play("Men1");
    }
    public override Type Tick()
    {
        return null;
    }

    public override void Exit()
    {
        Debug.Log("EmptyExit");
    }
}
