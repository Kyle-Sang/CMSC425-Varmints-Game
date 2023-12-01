using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System.IO;

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
    public GunType type;
    private AudioManager manager;
 

    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
        objAudio = GetComponent<AudioSource>();
        if (objAudio == null) Debug.Log("NO AUDIO FOUND");
        manager = FindObjectOfType<AudioManager>();
    }
    private void Update()
    {
        MyInput();

        //SetText
        text.SetText(bulletsLeft + " / " + magazineSize);

    }
    private void MyInput()
    {
        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload(); 
        //Shoot

        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = bulletsPerTap;
            manager.fire(type, this);
            Shoot();
        }
        if (readyToShoot && shooting && !reloading && bulletsLeft <= 0)
        {
            manager.fire(GunType.None, this);
        }
    }
    private void Shoot()
    {
        readyToShoot = false;

        //Spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);
        float z = Random.Range(-spread, spread);

        //Calculate Direction with Spread
        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, z);

        //RayCast
        if (Physics.Raycast(fpsCam.transform.position, direction, out rayHit, range))
        {
            Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.Euler(0, 180, 0));

            if (rayHit.collider.TryGetComponent(out IDamageable damageable)) {
                // temporary force
                float force = 1.0f;
                damageable.Damage(damage, ((rayHit.collider.transform.position - transform.position) * force));
            }
        }

        //ShakeCamera
        // camShake.Shake(camShakeDuration, camShakeMagnitude);

        //Graphics
        GameObject flash = Instantiate(muzzleFlash, attackPoint.position, Quaternion.Euler(attackPoint.transform.eulerAngles));
        flash.transform.parent = this.transform;
        
        bulletsLeft--;
        bulletsShot--;

        Invoke("ResetShot", timeBetweenShooting);

        if(bulletsShot > 0 && bulletsLeft > 0)
        Invoke("Shoot", timeBetweenShots);
    }
    private void ResetShot()
    {
        readyToShoot = true;
    }
    private void Reload()
    {
        manager.reload(type);
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }
    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }
}