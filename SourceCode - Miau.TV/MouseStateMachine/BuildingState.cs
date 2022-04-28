using System;
using System.Collections.Generic;
using UnityEngine;

public class BuildingState : BaseState
{

    private Mouse _mouse;
    string tag = "Ground";
    bool isNegative;
    public BuildingState(Mouse mouse) 
    {
        this._mouse = mouse;
    }

    public override void Enter()
    {
        Cursor.SetCursor(_mouse.pointers[1], Vector2.zero, CursorMode.Auto);
    }
    public override Type Tick()
    {
        CursorPoint(tag);

        if (!base.hit.collider.CompareTag(tag) && !isNegative)
        {
            _mouse.objModel.gameObject.GetComponentInChildren<MeshRenderer>().material = _mouse.Materials[1];
            isNegative = true;
        }
        else if (isNegative && base.hit.collider.CompareTag(tag))
        {
            _mouse.objModel.gameObject.GetComponentInChildren<MeshRenderer>().material = _mouse.Materials[0];
            isNegative = false;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            _mouse.CleanDrag();
            return typeof(EmptyState);
        }

        return null;
    }

    public override void Exit()
    {
        _mouse.CleanDrag();
    }

    public override void MouseAction()
    {
        _mouse.PlaceObject(base.hit.point);
    }
}
