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

    public GameObject muzzleFlash, bulletHoleGraphic;
    // Start is called before the first frame update
    void Start()
    {
        layerMask = 1 << layerNumber;
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
            Vector3 forward = transform.TransformDirection(Vector3.forward) * 100;
            Debug.DrawRay(transform.position, forward, Color.red);
            //Debug.Log(rayHit.collider.name);
            //Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.Euler(0, 180, 0));

            Debug.Log("Rayhit Collider: " + rayHit.collider);
            if (rayHit.collider.TryGetComponent(out IDamageable damageable))
            {
                // temporary force
                float force = 1.0f;
                damageable.Damage(damage, ((rayHit.collider.transform.position - transform.position) * force));
            }

            //Graphics
            Transform barrel = transform.Find("Barrel");
            GameObject flash = Instantiate(muzzleFlash, barrel.position, Quaternion.Euler(barrel.transform.eulerAngles));
            flash.transform.parent = this.transform;
        }
    }
}
