using Photon.Pun;
using UnityEngine;

namespace PlayerScripts
{
    public class PlayerRayMouse : MonoBehaviour
    {
        [SerializeField] private Camera myCam;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private PlayerUI playerUI;

        private PlayerInput _input;

        private Item _item;

        private PlayerInventory _inventory;

        private Transform _selection;

        private void Awake()
        {
            _input = GetComponent<PlayerInput>();
            _inventory = GetComponent<PlayerInventory>();
        }

        private void Update()
        {
            if (_selection != null)
            {
                var selectionOutline = _selection.GetComponent<Outline>();
                selectionOutline.OutlineWidth = 0f;
                playerUI.infoTxt.text = "Information";
                _selection = null;
            }
            var ray = myCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit, 25f, layerMask))
            {
                var selection = hit.transform;
                if (selection.CompareTag("Item"))
                {
                    var selectionOutline = selection.GetComponent<Outline>();
                    if (selectionOutline !=null)
                    {
                        selectionOutline.OutlineWidth = 6;
                        
                        
                    }
                    _selection = selection;
                    playerUI.infoTxt.text = selection.GetComponent<Item>().itemName;


                    if (_input.MouseRightClick)
                    {
                        if (_inventory.canGrabItems)
                        {
                            for (int i = 0; i < _inventory.slots.Length; i++)
                            {
                                if (_inventory.slots[i].imFull == false)
                                {
                                    _item = _selection.GetComponent<Item>();
                                    if (Vector3.Distance(transform.position,_item.transform.position) <= 3)
                                    {
                                        _inventory.slots[i].GetItem(_item.itemId, _item.itemName, _item.value, _item.weight);
                                        _inventory.CheckWeight(_item.weight);
                                        _inventory.CheckValue(_item.value);
                                        _inventory.slots[i].inventory = _inventory;
                                        _inventory.slots[i].player = this.gameObject;
                                        _item.photonView.RPC(nameof(_item.DestroyItem), RpcTarget.AllBuffered);
                                        _inventory.slots[i].imFull = true;
                                        playerUI.infoTxt.text = "Information";

                                    }
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }
    }

}
