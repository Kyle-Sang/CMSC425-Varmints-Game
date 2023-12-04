using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ThrowObject : MonoBehaviour
{
    // https://www.youtube.com/watch?v=F20Sr5FlUlE
    public Transform cam;
    public Transform attackPoint;
    public GameObject objectToThrow;
    public PlayerCurrency playerCurrency;
    public KeyCode throwKey;
    public float throwForce;
    public float cooldown = 0.5f;
    public float throwUpwardForce;
    public int cost;

    bool readyToThrow;

    private AudioManager manager;
    private void Start()
    {
        manager = manager = FindObjectOfType<AudioManager>();
        readyToThrow = true;
    }

    private void Update()
    {
        if(Input.GetKeyDown(throwKey) && readyToThrow)
        {
            manager.popFlare();
            Throw();
        }
    }

    private void Throw()
    {
        readyToThrow = false;

        // instantiate object to throw
        GameObject projectile = Instantiate(objectToThrow, attackPoint.position, cam.rotation);

        // get rigidbody component
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        // calculate direction
        Vector3 forceDirection = cam.transform.forward;

        RaycastHit hit;

        if(Physics.Raycast(cam.position, cam.forward, out hit, 500f))
        {
            forceDirection = (hit.point - attackPoint.position).normalized;
        }

        // add force
        Vector3 forceToAdd = forceDirection * throwForce + transform.up * throwUpwardForce;

        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);
        playerCurrency.changeMoney(-cost);

        // implement throwCooldown
        Invoke(nameof(ResetThrow), cooldown);
    }

    private void ResetThrow()
    {
        readyToThrow = true;
    }
}