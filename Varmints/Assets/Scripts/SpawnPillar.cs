using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPillar : MonoBehaviour
{
    public GameObject wall;
    public float range;
    public Camera fpsCam;
    public RaycastHit rayHit;
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
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            Spawn();
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
