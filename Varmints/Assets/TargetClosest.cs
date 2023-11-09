using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetClosest : MonoBehaviour
{
    private List<GameObject> enemies;
    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.Find("WaveSpawner").GetComponent<WaveSpawner>().enemies;
    }

    // Update is called once per frame
    void Update()
    {
        Transform target = FindClosest();
        transform.LookAt(target);
    }

    private Transform FindClosest() 
    {
        return enemies.Min(enemy => GetDistance(enemy));
    }

    private float GetDistance(GameObject enemy) 
    {
        return Vector3.Distance(enemy, this.transform.position);
    }

}
