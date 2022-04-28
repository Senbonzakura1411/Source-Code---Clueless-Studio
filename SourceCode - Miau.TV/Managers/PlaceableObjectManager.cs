using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableObjectManager : Singleton<PlaceableObjectManager>
{
    public List<GameObject> toys;
    public List<GameObject> food;
    public List<GameObject> houses;
    public List<GameObject> litter;

    public void RefreshObjectsList(int listID)
    {
        switch (listID)
        {
            case 1:
                Object[] objs = FindObjectsOfType<CatObjectManager>();
                toys.Clear();
                foreach (CatObjectManager obj in objs)
                {
                    if (obj.myName == "ball")
                    {
                        toys.Add(obj.gameObject);
                    }
                }
                break;
            case 2:
                objs = FindObjectsOfType<CatObjectManager>();
                houses.Clear();
                foreach (CatObjectManager obj in objs)
                {
                    if (obj.myName == "bed")
                    {
                        houses.Add(obj.gameObject);
                    }
                }
                break;
            case 3:
                objs = FindObjectsOfType<CatObjectManager>();
                food.Clear();
                foreach (CatObjectManager obj in objs)
                {
                    if (obj.myName == "food")
                    {
                        food.Add(obj.gameObject);
                    }
                }
                break;
            case 4:
                objs = FindObjectsOfType<CatObjectManager>();
                litter.Clear();
                foreach (CatObjectManager obj in objs)
                {
                    if (obj.myName == "litter")
                    {
                        litter.Add(obj.gameObject);
                    }
                }
                break;

            default:
                break;
        }
    }
}
