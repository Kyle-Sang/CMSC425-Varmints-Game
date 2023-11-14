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
    // Start is called before the first frame update
    void Start()
    {
        layerMask = 1 << layerNumber;
    }

    // Update is called once per frame
    void Update()
    {
        InvokeRepeating("Shoot", timeBetweenShots, timeBetweenShots);
    }

    private void Shoot()
    {
        //RayCast
        if (Physics.Raycast(transform.position, transform.forward, out rayHit, range, layerMask))
        {
            //Debug.Log(rayHit.collider.name);
            //Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.Euler(0, 180, 0));

            if (rayHit.collider.TryGetComponent(out IDamageable damageable))
            {
                // temporary force
                float force = 1.0f;
                damageable.Damage(damage, ((rayHit.collider.transform.position - transform.position) * force));
            }
        }
    }
}
