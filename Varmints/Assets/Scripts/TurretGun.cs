using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretGun : MonoBehaviour
{
    //Gun stats
    private int layerNumber = 8; // Layer Number for the enemies
    private int layerMask; // Layer mask for the enemies
    private RaycastHit rayHit;
    public int damage;
    public float range, timeBetweenShots;
    public Transform attackPoint;
    public TrailRenderer bulletTrail;

    public GameObject muzzleFlash, bulletHoleGraphic;

    private AudioManager manager;
    // Start is called before the first frame update
    void Start()
    {
        layerMask = 1 << layerNumber;
        manager = FindObjectOfType<AudioManager>();
        InvokeRepeating("Shoot", timeBetweenShots, timeBetweenShots);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Shoot()
    {
        //RayCast
        if (Physics.Raycast(transform.position, transform.forward, out rayHit, range))
        {
            if (rayHit.collider.tag == "Enemy") {
                manager.fire(GunType.Turret, timeBetweenShots);
                Vector3 forward = transform.TransformDirection(Vector3.forward) * 100;
                Debug.DrawRay(transform.position, forward, Color.red);

                TrailRenderer trail = Instantiate(bulletTrail, attackPoint.position, Quaternion.identity);
                StartCoroutine(SpawnTrail(trail, rayHit));

                //Debug.Log(rayHit.collider.name);
                //Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.Euler(0, 180, 0));

                if (rayHit.collider.TryGetComponent(out IDamageable damageable))
                {
                    // temporary force
                    float force = 1.0f;
                    damageable.Damage(damage, ((rayHit.collider.transform.position - transform.position) * force));
                }

                //Graphics
                Transform barrel = transform.Find("Barrel");
                GameObject flash = Instantiate(muzzleFlash, attackPoint.position, Quaternion.Euler(barrel.transform.eulerAngles));
                flash.transform.parent = this.transform;
            }
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
