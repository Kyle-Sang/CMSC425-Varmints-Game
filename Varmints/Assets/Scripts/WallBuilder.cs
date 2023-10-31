using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class wallBuilder : MonoBehaviour
{
    public GameObject wallPrefab;
    public float radius;
    public int maxWalls;
    private int currentWalls = 0;
    // Start is called before the first frame update
    void Start()
    {
        Collider[] nearbyColliders = Physics.OverlapSphere(this.transform.position, radius);
        nearbyColliders = nearbyColliders.OrderBy(x => Vector3.Distance (this.transform.position,x.transform.position)).ToArray();
        foreach (var collider in nearbyColliders)
        {
            if(collider.gameObject.tag == "Pillar" && gameObject != collider.gameObject && currentWalls < maxWalls)
            {
                collider.gameObject.transform.LookAt(transform.position);
                float distance = Vector3.Distance(collider.gameObject.transform.position, transform.position);
                GameObject wall = (GameObject)Instantiate(wallPrefab, transform);
                wall.transform.position = collider.gameObject.transform.position + distance / 2 * collider.gameObject.transform.forward;
                wall.transform.rotation = collider.gameObject.transform.rotation;
                wall.transform.localScale = new Vector3(wall.transform.localScale.x, wall.transform.localScale.y, distance / transform.localScale.z);
                wall.transform.parent = null;
                currentWalls += 1;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
