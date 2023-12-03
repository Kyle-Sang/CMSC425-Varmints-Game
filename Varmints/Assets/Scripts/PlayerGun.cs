using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System.IO;
using System;
using UnityEngine.UI;
using System.Data.SqlTypes;

public class PlayerGun : MonoBehaviour
{
    //Gun stats
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    public int bulletsLeft, bulletsShot;

    //bools 
    bool shooting, readyToShoot, reloading;

    //Reference
    public Camera fpsCam;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;

    //Graphics
    public GameObject muzzleFlash, bulletHoleGraphic;
    // public CamShake camShake;
    public float camShakeMagnitude, camShakeDuration;
    public TextMeshProUGUI text;

    private AudioSource objAudio;
    private AudioManager manager;

    public GunData[] guns;
    private GunData curr;
    public GunType type;
    //private Boolean toggle;
    private float timeBetweenShoot;
    public TrailRenderer bulletTrail;


    private void Awake()
    {
        readyToShoot = true;
        objAudio = GetComponent<AudioSource>();
        if (objAudio == null) Debug.Log("NO AUDIO FOUND");
        manager = FindObjectOfType<AudioManager>();
        curr = guns[0];
        type = guns[0].type;
        curr.bulletsLeft = curr.magazineSize;
        timeBetweenShoot = curr.timeBetweenShooting; 

    }
    private void Update()
    {
        MyInput();
        changeWeapon();
        //SetText
        text.SetText(curr.bulletsLeft + " / " + curr.magazineSize);

    }
    private void MyInput()
    {
        if (curr.allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (Input.GetKeyDown(KeyCode.R) && curr.bulletsLeft < curr.magazineSize && !reloading) Reload(); 
        //Shoot

        if (readyToShoot && shooting && !reloading && curr.bulletsLeft > 0)
        {
            curr.bulletsShot = curr.bulletsPerTap;
            manager.fire(type, curr.timeBetweenShooting);
            Shoot();
        }
        if (readyToShoot && shooting && !reloading && curr.bulletsLeft <= 0)
        {
            manager.fire(GunType.None, curr.timeBetweenShooting);
        }
    }
    private void Shoot()
    {
        readyToShoot = false;

        //Spread
        float x = UnityEngine.Random.Range(-curr.spread, curr.spread);
        float y = UnityEngine.Random.Range(-curr.spread, curr.spread);
        float z = UnityEngine.Random.Range(-curr.spread, curr.spread);

        //Calculate Direction with Spread
        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, z);

        //RayCast
        if (Physics.Raycast(fpsCam.transform.position, direction, out rayHit, range))
        {
            // Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.Euler(0, 180, 0));

            TrailRenderer trail = Instantiate(bulletTrail, attackPoint.position, Quaternion.identity);
            StartCoroutine(SpawnTrail(trail, rayHit));

            if (rayHit.collider.TryGetComponent(out IDamageable damageable)) {
                // temporary force
                float force = 1.0f;
                damageable.Damage(curr.damage, ((rayHit.collider.transform.position - transform.position) * force));
            }
        }

        //ShakeCamera
        // camShake.Shake(camShakeDuration, camShakeMagnitude);

        //Graphics
        GameObject flash = Instantiate(muzzleFlash, attackPoint.position, Quaternion.Euler(attackPoint.transform.eulerAngles));
        flash.transform.parent = this.transform;
        
        curr.bulletsLeft--;
        curr.bulletsShot--;
        
        Invoke("ResetShot", curr.timeBetweenShooting);

        if(curr.bulletsShot > 0 && curr.bulletsLeft > 0)
        Invoke("Shoot", curr.timeBetweenShots);
    }
    private void ResetShot()
    {
        readyToShoot = true;
    }
    private void Reload()
    {
        manager.reload(type);
        reloading = true;
        StartCoroutine(ReloadFinished(curr));
    }
    private IEnumerator ReloadFinished(GunData reloadGun)
    {
        yield return new WaitForSeconds(reloadGun.reloadTime);
        reloadGun.bulletsLeft = reloadGun.magazineSize;
        reloading = false;
    }

    private void changeWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //GunData pistol = Array.Find(guns, x => x.type == GunType.Pistol);
            Reload();
            reloading = false;
            curr = guns[0];
            type = curr.type;

        } else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //GunData shotgun = Array.Find(guns, x => x.type == GunType.Shotgun);
            Reload();
            reloading = false;
            curr = guns[1];
            type = curr.type;
            
        } else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Reload();
            reloading = false;
            curr = guns[2];
            type = curr.type;

        }
    }

    
    private IEnumerator SpawnTrail(TrailRenderer trail, RaycastHit hit) {
        float time = 0;
        Vector3 start = trail.transform.position;

        while (time < 1) {
            trail.transform.position = Vector3.Lerp(start, hit.point, time);
            time += Time.deltaTime / trail.time;
            yield return null;
        }
        trail.transform.position = hit.point;
        Destroy(trail.gameObject, trail.time);
    }
}