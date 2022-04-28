using System;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    public MouseStateManager StateMachine => GetComponent<MouseStateManager>();

    public PlaceableObject[] Items;
    public Material[] Materials;
    public Texture2D[] pointers;
    [SerializeField] LayerMask ignoreRaycastLayer;
    [HideInInspector] public GameObject objModel;
    int currentItem;


    private void Awake()
    {
        InitializeStateMachine();
    }

    private void InitializeStateMachine()
    {
        var states = new Dictionary<Type, BaseState>()
        {
            { typeof(EmptyState), new EmptyState(this) },
            { typeof (BuildingState), new BuildingState(this) },
            { typeof (ConsumableState), new ConsumableState(this) }
        };

        GetComponent<MouseStateManager>().SetStates(states);
    }

    private void Start()
    {
        Cursor.SetCursor(pointers[0], Vector2.zero, CursorMode.Auto);
    }
    private void Update()
    {
        if (objModel != null)
        {
            objModel.transform.position = StateMachine.CurrentState.hit.point;
        }
    }

    public void CursorDrag(int value)
    {
        currentItem = value;
        if (objModel != null)
        {
            Destroy(objModel.gameObject);
        }
        objModel = Instantiate(Items[currentItem]._objModel, Input.mousePosition, Quaternion.identity);
        Destroy(objModel.gameObject.GetComponent<CatObjectManager>());
        objModel.gameObject.layer = ignoreRaycastLayer;
        objModel.gameObject.GetComponentInChildren<MeshRenderer>().material = Materials[0];
    }

    public void CleanDrag()
    {
        if (objModel != null)
        {
            Destroy(objModel.gameObject);
        }
    }

    public void PlaceObject(Vector3 pos)
    {
        if (ResourcesManager.Instance.money >= Items[currentItem]._objPrice)
        {
            Instantiate(Items[currentItem]._objModel, pos, Quaternion.identity);
            ResourcesManager.Instance.SubstractMoney(Items[currentItem]._objPrice);
            PlaceableObjectManager.Instance.RefreshObjectsList(Items[currentItem]._objID);
            switch (currentItem)
            {
                case 0:
                    AudioManager.instance.Play("ToyL1");
                    break;
                case 2:
                    AudioManager.instance.Play("Food1");
                    break;
                default:
                    AudioManager.instance.Play("HouseC1");
                    break;
            }
        }
        else
        {
            AudioManager.instance.Play("ToyL3");
        }
    }
}
