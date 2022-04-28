using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlaceableObject 
{
    public string _objName;
    public int _objID, _objPrice;
    public GameObject _objModel;
    
    PlaceableObject(int objID, string objName, GameObject objModel, int objPrice)
    {
        this._objName = objName;
        this._objID = objID;
        this._objModel = objModel;
        this._objPrice = objPrice;
    }
}


