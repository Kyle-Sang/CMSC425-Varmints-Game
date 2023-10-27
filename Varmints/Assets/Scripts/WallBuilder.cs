using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallBuilder : MonoBehaviour
{
    public GameObject wallPrefab;
    public float radius;
    // Start is called before the first frame update
    void Start()
    {
        Collider[] nearbyColliders = Physics.OverlapSphere(this.transform.position, radius);
        foreach (var collider in nearbyColliders)
        {
            if(collider.gameObject.tag == "Pillar" && gameObject != collider.gameObject)
            {
                Debug.Log(collider);
                collider.gameObject.transform.LookAt(transform.position);
                float distance = Vector3.Distance(collider.gameObject.transform.position, transform.position);
                GameObject wall = (GameObject)Instantiate(wallPrefab, transform);
                wall.transform.position = collider.gameObject.transform.position + distance / 2 * collider.gameObject.transform.forward;
                wall.transform.rotation = collider.gameObject.transform.rotation;
                wall.transform.localScale = new Vector3(wall.transform.localScale.x, wall.transform.localScale.y, distance / transform.localScale.z);
                wall.transform.parent = null;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
