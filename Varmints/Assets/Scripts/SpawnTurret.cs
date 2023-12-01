using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTurret : MonoBehaviour
{
    public GameObject turret;
    public float range;
    public Camera fpsCam;
    public RaycastHit rayHit;
    public PlayerCurrency playerCurrency;
    public int cost;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void MyInput()
    {
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            if (playerCurrency.count >= cost) {
                Spawn();
                playerCurrency.changeMoney(-cost);
            }
        }
    }
    private void Spawn()
    {
        Vector3 direction = fpsCam.transform.forward;

        if (Physics.Raycast(fpsCam.transform.position, direction, out rayHit, range))
        {
            Instantiate(turret, rayHit.point, Quaternion.Euler(0, 180, 0));
        }

    }
}
