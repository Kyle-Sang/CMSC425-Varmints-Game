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
    public int increment = 0;

    public KeyCode key;

    private AudioManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<AudioManager>();
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
            }
        }
    }
    private void Spawn()
    {
        Vector3 direction = fpsCam.transform.forward;

        if (Physics.Raycast(fpsCam.transform.position, direction, out rayHit, range))
        {
            if (rayHit.transform.CompareTag("Ground")) {
                manager.spawn(wall.name);
                Instantiate(wall, rayHit.point, Quaternion.Euler(0, 180, 0));
                playerCurrency.changeMoney(-cost);
                cost += increment;
            }  
        }

    }
}
