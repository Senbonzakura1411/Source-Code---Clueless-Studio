using Photon.Pun;
using UnityEngine;

namespace Player
{
    public class PlayerSetup : MonoBehaviour
    {

        [SerializeField] private GameObject playerUI;
        private void Start()
        {
            if (GetComponent<PhotonView>().IsMine)
            {
                GetComponent<PlayerInput>().enabled = true;
                GetComponent<PlayerController>().enabled = true;
                GetComponent<PlayerRotation>().enabled = true;
                GetComponent<Gun>().enabled = true;
                playerUI.SetActive(true);
            }
        }
    }
}