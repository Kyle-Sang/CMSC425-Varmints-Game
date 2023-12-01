using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class SpawnUpgrade : MonoBehaviour
{
    public GameObject wall;
    public float range;
    public Camera fpsCam;
    public RaycastHit rayHit;
    public PlayerCurrency playerCurrency;
    public int cost;

    public KeyCode key;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MyInput();
    }

    private void MyInput()
    {
        if (Input.GetKeyDown(key)) 
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
            Instantiate(wall, rayHit.point, Quaternion.Euler(0, 180, 0));
        }

    }
}
