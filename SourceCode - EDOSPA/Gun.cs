using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityStandardAssets.Characters.FirstPerson;

public class Gun : MonoBehaviourPunCallbacks
{
    [SerializeField] Camera fpsCamera;

    //Gun stats
    public int stockMax;
    [HideInInspector] public int bulletStock;
    public float timeBetweenShooting, spread, range, reloadTime, recoil, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    int bulletsLeftInChamber, bulletsShot, lastStock;

    //bools 
    bool shooting, readyToShoot;
    [HideInInspector] public bool reloading = false;

    //Reference
    public Transform myAttackPoint, othersAttackPoint;

    //Graphics
    public GameObject muzzleFlash;
    public GameObject bulletTracer;
    public TextMeshProUGUI magTxt, stocktxt;

    //Sound
    public AudioSource gunSound;
    public AudioClip gunShot, gunReload, noBullets;

    //Other
    public float destroyTimer;
    private float startRecoil;
    public GameObject hitFXprefab, playerHitFXprefab;
    RigidbodyFirstPersonController controller;

    private void Awake()
    {
        bulletsLeftInChamber = magazineSize;
        readyToShoot = true;
        startRecoil = recoil;
        RefillAmmo();
    }

    private void Start()
    {
        controller = GetComponent<RigidbodyFirstPersonController>();
    }
    private void Update()
    {

        MyInput();
        //check Stock not go below 0
        CheckStock();
        //SetText
        magTxt.SetText(bulletsLeftInChamber.ToString());
        stocktxt.SetText(bulletStock.ToString());
    }

    private void MyInput()
    {
        if (allowButtonHold) shooting = Input.GetButton("Fire1");
        else shooting = Input.GetButtonDown("Fire1");

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeftInChamber < magazineSize && bulletStock > 0 && !reloading) Reload();

        if (!shooting && recoil > startRecoil)
        {
            recoil--;
        }

        //Shoot
        if (!controller.Running && readyToShoot && shooting && !reloading && bulletsLeftInChamber > 0)
        {
            //recoil
            recoil += 0.5f;


            bulletsShot = bulletsPerTap;
            Shoot();
        }
        else if (bulletsLeftInChamber <= 0 && Input.GetButtonDown("Fire1"))
        {
            gunSound.PlayOneShot(noBullets);
        }
    }
    private void Shoot()
    {
        readyToShoot = false;
        photonView.RPC("PlaySound", RpcTarget.All);

        //Spread
        //float x = UnityEngine.Random.Range(-spread + (recoil * 0.001f), spread + (recoil * 0.001f));
        //float y = UnityEngine.Random.Range(-spread, spread) + (recoil * 0.005f);

        float x = UnityEngine.Random.Range(-spread, spread);
        float y = UnityEngine.Random.Range(-spread, spread);

        //Calculate Direction with Spread
        Vector3 direction = fpsCamera.transform.forward + new Vector3(x, y, 0);

        //RayCast
        RaycastHit hit;

        if (Physics.Raycast(fpsCamera.transform.position, direction, out hit, range))
        {
            Debug.Log(hit.collider.gameObject.name);
            SpawnBulletTrail(hit.point, myAttackPoint.position);

            if (hit.collider.gameObject.CompareTag("Player") && !hit.collider.gameObject.GetComponent<PhotonView>().IsMine)
            {
                photonView.RPC("CreatePlayerHitEffect", RpcTarget.All, hit.point);
                hit.collider.gameObject.GetComponent<PhotonView>().RPC("OnHitDamage", RpcTarget.AllBuffered, 10.0f, PhotonNetwork.LocalPlayer);
            }

            else if (hit.collider.gameObject.CompareTag("PlayerHead") && !hit.collider.GetComponentInParent<PhotonView>().IsMine)
            {
                hit.collider.gameObject.GetComponentInParent<PhotonView>().RPC("OnHitDamage", RpcTarget.AllBuffered, 100.0f, PhotonNetwork.LocalPlayer);
            }
            else
            {
                photonView.RPC("CreateHitEffect", RpcTarget.All, hit.point);
            }
        }

        //MuzzleFlash
        SpawnMuzzleFlash(myAttackPoint.position);


        bulletsLeftInChamber--;
        bulletsShot--;

        Invoke("ResetShot", timeBetweenShooting);

        if (bulletsShot > 0 && bulletsLeftInChamber > 0)
            Invoke("Shoot", timeBetweenShots);
    }
    private void ResetShot()
    {
        readyToShoot = true;
    }
    private void Reload()
    {
        lastStock = bulletStock;
        bulletStock -= (magazineSize - bulletsLeftInChamber);
        gunSound.PlayOneShot(gunReload);
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    void CheckStock()
    {
        if (bulletStock <= 0)
        {
            bulletStock = 0;
        }
    }
    public void RefillAmmo()
    {
        bulletsLeftInChamber = magazineSize;
        bulletStock = stockMax;
    }
    private void ReloadFinished()
    {
        if (bulletStock >= magazineSize)
        {
            bulletsLeftInChamber = magazineSize;
        }
        else
        {
            bulletsLeftInChamber = lastStock;
        }
        reloading = false;
    }


    [PunRPC]
    private void SpawnMuzzleFlash(Vector3 position)
    {
        GameObject tempFlash = Instantiate(muzzleFlash, position, Quaternion.identity);
        Destroy(tempFlash, destroyTimer);
    }

    [PunRPC]
    private void PlaySound()
    {
        gunSound.PlayOneShot(gunShot);
    }
    [PunRPC]
    private void SpawnBulletTrail(Vector3 hit, Vector3 position)
    {
        GameObject bulletTrailEffect = Instantiate(bulletTracer, position, fpsCamera.transform.rotation);

        TrailRenderer trail = bulletTrailEffect.GetComponent<TrailRenderer>();

        trail.AddPosition(position);
        trail.AddPosition(hit);

        Destroy(bulletTrailEffect, destroyTimer);
    }

    [PunRPC]
    public void CreateHitEffect(Vector3 pos)
    {

        GameObject playerHitEffect = Instantiate(hitFXprefab, pos, Quaternion.identity);
        Destroy(playerHitEffect, 0.2f);
    }

    [PunRPC]
    public void CreatePlayerHitEffect(Vector3 pos)
    {
        GameObject playerHitEffect = Instantiate(playerHitFXprefab, pos, Quaternion.identity);
        Destroy(playerHitEffect, 0.2f);
    }
}
