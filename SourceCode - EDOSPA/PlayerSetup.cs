using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using Photon.Pun;
using TMPro;
public class PlayerSetup : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject fpsCamera;
    public GameObject[] fpsHandsChildGameObjects;
    public GameObject[] soldierChildGameObjects;
    Animator anim;

    [SerializeField] TextMeshProUGUI playerNameText;
    [SerializeField] Canvas playerCanvas;
    void Start()
    {
        anim = GetComponent<Animator>();

        if (photonView.IsMine)
        {
            foreach (GameObject gameObject in fpsHandsChildGameObjects)
            {
                gameObject.SetActive(true);
            }
            foreach (GameObject gameObject in soldierChildGameObjects)
            {
                gameObject.SetActive(false);
            }

            GetComponent<RigidbodyFirstPersonController>().enabled = true;
            GetComponent<Gun>().enabled = true;
            fpsCamera.SetActive(true);
            playerNameText.gameObject.SetActive(false);
            playerCanvas.gameObject.SetActive(true);
            anim.SetBool("isSoldier", false);
        }
        else
        {
            foreach (GameObject gameObject in fpsHandsChildGameObjects)
            {
                gameObject.SetActive(false);
            }
            foreach (GameObject gameObject in soldierChildGameObjects)
            {
                gameObject.SetActive(true);
            }

            GetComponent<RigidbodyFirstPersonController>().enabled = false;
            GetComponent<Gun>().enabled = false;
            GetComponent<Pause>().enabled = false;
            fpsCamera.SetActive(false);
            playerNameText.gameObject.SetActive(true);
            playerCanvas.gameObject.SetActive(false);
            anim.SetBool("isSoldier", true);
        }

        SetPlayerUI();
    }
    void SetPlayerUI()
    {
        if (playerNameText != null)
        playerNameText.text = photonView.Owner.NickName;
    }

}
