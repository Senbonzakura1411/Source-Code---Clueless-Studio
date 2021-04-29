using UnityEngine;
using TMPro;
using EZCameraShake;
using UnityEngine.InputSystem;
using System.Net.Sockets;
using System;
using SensorToolkit.Example;

public class FPSGun : MonoBehaviour
{
    //Gun stats
    public int damage, stockMax;
    [HideInInspector] public int bulletStock;
    public float timeBetweenShooting, spread, range, reloadTime, recoil, timeBetweenShots;
    public int magazineSize, bulletsPerTap, shotgunShotPellets;
    public bool isShotgun, allowButtonHold;
    int bulletsLeftInChamber, bulletsShot, lastStock;

    //bools 
    bool shooting, readyToShoot;
    [HideInInspector] public bool reloading = false;

    //Reference
    public Camera fpsCam;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;

    //Graphics
    public GameObject muzzleFlash;
    public GameObject bulletTracer, bulletFleshImpact;
    public CameraShakeInstance camShake;
    public float camShakeMagnitude, camShakeDuration;
    public TextMeshProUGUI magTxt, stocktxt, nameLevel;
    public GameObject gunIcon;

    //Animator
    public Animator gunAnimator;

    //Sound
    public AudioSource gunSound;
    public AudioClip gunShot, gunReload, noBullets, fleshImpactSound;

    //Other
    public float destroyTimer;
    private float startRecoil;


    private void OnEnable()
    {
        gunIcon.SetActive(true);
    }
    private void Awake()
    {
        bulletsLeftInChamber = magazineSize;
        readyToShoot = true;
        startRecoil = recoil;
        RefillAmmo();
    }


    private void Update()
    {
        if (!Pause.isPaused)
        {
            MyInput();
            //check Stock not go below 0
            CheckStock();
            //SetText
            magTxt.SetText(bulletsLeftInChamber.ToString());
            stocktxt.SetText(bulletStock.ToString());
            nameLevel.SetText(gameObject.name + " Lvl. " + PlayersLevelManager.Instance.playersLevel);
        }
    }

    private void OnDisable()
    {
        gunIcon.SetActive(false);
    }
    private void MyInput()
    {
        if (allowButtonHold) shooting = Gamepad.current.rightTrigger.isPressed;
        else shooting = Gamepad.current.rightTrigger.wasPressedThisFrame;

        if  (Gamepad.current.buttonWest.wasPressedThisFrame && bulletsLeftInChamber < magazineSize && bulletStock > 0 && !reloading) Reload();
        
        if (!shooting && recoil > startRecoil)
        {
            recoil--;
        }

        //Shoot
        if (readyToShoot && shooting && !reloading && bulletsLeftInChamber > 0)
        {
            //recoil
            recoil++;
            bulletsShot = bulletsPerTap;
            Shoot();
        }
        else if (bulletsLeftInChamber <= 0 && Gamepad.current.rightTrigger.wasPressedThisFrame)
        {
            gunSound.PlayOneShot(noBullets);
        }
    }
    private void Shoot()
    {
        readyToShoot = false;
        gunSound.PlayOneShot(gunShot);
        gunAnimator.Play("Fire", 0);
        if (!isShotgun)
        {
            //Spread
            float x = UnityEngine.Random.Range(-spread + (recoil * 0.001f), spread + (recoil * 0.001f));
            float y = UnityEngine.Random.Range(-spread, spread) + (recoil * 0.005f);

            //Calculate Direction with Spread
            Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);

            //RayCast
            if (Physics.Raycast(fpsCam.transform.position, direction, out rayHit, range, whatIsEnemy))
            {
                SpawnBulletTrail(rayHit.point);

                if (rayHit.collider.CompareTag("Enemy"))
                {
                    SpawnFleshImpact();
                    rayHit.collider.GetComponentInParent<Enemy>().TakeDamage(damage, fpsCam.GetComponentInParent<FPSController>().transform);
                }

            }
            else
            {
                GameObject bulletTrailEffect = Instantiate(bulletTracer, attackPoint.position, fpsCam.transform.rotation);
                bulletTrailEffect.GetComponent<Rigidbody>().AddForce(direction * 20000);
                Destroy(bulletTrailEffect, destroyTimer);
            }
        }
        else
        {
            ShotgunShot();
        }

        //ShakeCamera
        CameraShaker.Instance.ShakeOnce(camShakeMagnitude, 10, 0, camShakeDuration);

        //MuzzleFlash
        SpawnMuzzleFlash();


        bulletsLeftInChamber--;
        bulletsShot--;

        Invoke("ResetShot", timeBetweenShooting);

        if (bulletsShot > 0 && bulletsLeftInChamber > 0)
            Invoke("Shoot", timeBetweenShots);
    }

    private void ShotgunShot()
    {
        for (int i = 0; i <= shotgunShotPellets; i++)
        {
            //Spread
            float x = UnityEngine.Random.Range(-spread + (recoil * 0.001f), spread + (recoil * 0.001f));
            float y = UnityEngine.Random.Range(-spread, spread) + (recoil * 0.005f);

            //Calculate Direction with Spread
            Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);

            //RayCast
            if (Physics.Raycast(fpsCam.transform.position, direction, out rayHit, range, whatIsEnemy))
            {
                SpawnBulletTrail(rayHit.point);

                if (rayHit.collider.CompareTag("Enemy"))
                {
                    SpawnFleshImpact();
                    rayHit.collider.GetComponentInParent<Enemy>().TakeDamage(damage, fpsCam.GetComponentInParent<FPSController>().transform);
                }

            }
            else
            {
                GameObject bulletTrailEffect = Instantiate(bulletTracer, attackPoint.position, fpsCam.transform.rotation);
                bulletTrailEffect.GetComponent<Rigidbody>().AddForce(direction * 20000);
                Destroy(bulletTrailEffect, destroyTimer);
            }
        }
    }
    private void SpawnFleshImpact()
    {
        gunSound.PlayOneShot(fleshImpactSound, 10);
        GameObject temphit = Instantiate(bulletFleshImpact, rayHit.point, rayHit.collider.gameObject.transform.rotation);
        Destroy(temphit, 1f);

    }

    private void SpawnMuzzleFlash()
    {
        GameObject tempFlash = Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);
        Destroy(tempFlash, destroyTimer);
    }

    private void SpawnBulletTrail(Vector3 hit)
    {
        GameObject bulletTrailEffect = Instantiate(bulletTracer, attackPoint.position, fpsCam.transform.rotation);

        TrailRenderer trail = bulletTrailEffect.GetComponent<TrailRenderer>();

        trail.AddPosition(attackPoint.position);
        trail.AddPosition(hit);

        Destroy(bulletTrailEffect, destroyTimer);
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }
    private void Reload()
    {
        lastStock = bulletStock;
        bulletStock -= (magazineSize - bulletsLeftInChamber);
        gunAnimator.Play("Reload", 0);
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
}