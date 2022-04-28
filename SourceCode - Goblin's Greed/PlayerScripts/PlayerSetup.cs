using System;
using Photon.Pun;
using Photon.Realtime;
using playerProperties = ExitGames.Client.Photon.Hashtable;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace PlayerScripts
{
    public class PlayerSetup : MonoBehaviourPunCallbacks
    {
        [SerializeField] private GameObject playerCamera;

        [SerializeField] private Sprite cursorSprite;

        [SerializeField] private GameObject playerUI;

        private void Awake()
        {
            if (photonView.IsMine)
            {
                GetComponent<PlayerInput>().enabled = true;
                GetComponent<PlayerController>().enabled = true;
                GetComponent<PlayerAbilities>().enabled = true;
                GetComponent<PlayerRayMouse>().enabled = true;
                GetComponent<PlayerInventory>().enabled = true;
                gameObject.AddComponent<PlayerInteraction>();
                playerCamera.SetActive(true);
                playerUI.SetActive(true);
            }
            else
            {
                gameObject.tag = "EnemyPlayer";
                GameManager.Instance.cameras.Add(playerCamera.GetComponent<Camera>());
            }
            //private void Start() => Cursor.SetCursor(cursorSprite.texture, Vector2.zero, CursorMode.Auto);
        }

        private void Update()
        {
            Debug.Log(PhotonNetwork.PlayerListOthers[0].NickName + PhotonNetwork.PlayerListOthers[0].CustomProperties["Value"]);
        }
        
        public void SetData()
        {
            var hash = new Hashtable
            {
                {"Value", GetComponent<PlayerInventory>().currentValue},
                {"Weight", GetComponent<PlayerInventory>().currentWeight}
            };

            photonView.Owner.SetCustomProperties(hash);
        }
    }
}

