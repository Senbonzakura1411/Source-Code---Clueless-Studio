using Photon.Pun;
using UnityEngine;
using TMPro;

namespace PlayerScripts
{
    public class PlayerInventory : MonoBehaviour
    {
        public PlayerUI playerUI;

        public InventorySlot[] slots;

        private PlayerInput _input;

        public GameObject inventory;

        public float maxWeight;

        public float currentWeight;

        public float currentValue;

        public bool empty;

        public bool canGrabItems;

        private InfoCollector _infoCollector;

        public TextMeshProUGUI currentWeightTxt;
        public TextMeshProUGUI MaxWeightTxt;

        private void Awake()
        {
            _input = GetComponent<PlayerInput>();
            _infoCollector = InfoCollector.Instance;
        }

        public void Start()
        {
            CheckWeight(0);
        }

        public void Update()
        {
            if (_input.InventoryInput)
            {
                if (inventory.activeSelf == false)
                {
                    _input.OnUI = true;
                    inventory.SetActive(true);
                }
                else
                {
                    _input.OnUI = false;
                    inventory.SetActive(false);
                }
            }

            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].imFull == true)
                {
                    empty = false;
                    break;
                }
                else 
                {
                    empty = true;
                }
            }

            FillMochila();
            
            Debug.Log(currentValue);
        }

        public void FillMochila ()
        {
            playerUI.mochila.fillAmount = currentWeight / maxWeight;
        }

        public void CheckValue (float value)
        {
            currentValue += value;
        }

        public void CheckWeight (float weight)
        {
            currentWeight += weight;
            currentWeightTxt.text = currentWeight.ToString();
            MaxWeightTxt.text = maxWeight.ToString();
            if (currentWeight >= maxWeight)
            {
                canGrabItems = false;
            }
            else
            {
                canGrabItems = true;
            }

        }

        public void DropItemsStunned ()
        {
            if (!empty)
            {
                int cantidadObjs = 0;
                for (int i = 0; i < slots.Length; i++)
                {
                    if (slots[i].imFull)
                    {
                        cantidadObjs++;
                    }
                }

                if (cantidadObjs > 3)
                {
                    int randomNum;
                    for (int i = 0; i < 3; i++)
                    {
                        do
                        {
                            randomNum = Random.Range(0, slots.Length);
                        } while (slots[randomNum].imFull == false);

                        slots[randomNum].DropItems();
                    }
                    
                }
                else
                {
                    for (int i = 0; i < slots.Length; i++)
                    {
                        if (slots[i].imFull)
                        {
                            slots[i].DropItems();
                        }
                    }
                }
            }
            
        }
    }
}
