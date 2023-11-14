using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetClosest : MonoBehaviour
{
    private List<GameObject> enemies;
    private float turnRateRadians = 2 * Mathf.PI;

    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.Find("WaveSpawner").GetComponent<WaveSpawner>().enemies;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemies.Count > 0)
        {
            Transform target = FindClosest();

            if (target != null)
            {
                Vector3 targetDir = target.transform.position - transform.position;
                // Rotating in 2D Plane...
                targetDir = targetDir.normalized;

                Vector3 currentDir = transform.forward;

                currentDir = Vector3.RotateTowards(currentDir, targetDir, turnRateRadians * Time.deltaTime, 1.0f);

                Quaternion qDir = new Quaternion();
                qDir.SetLookRotation(currentDir, Vector3.up);
                transform.rotation = qDir;
            }
        }
    }

    public Transform FindClosest() 
    {
        float min = float.MaxValue;
        Transform ret = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = GetDistance(enemy);
            if (distance < min)
            {
                ret = enemy.transform;
                min = distance;
            }
        }

        return ret; // should never be null
    }

    private float GetDistance(GameObject enemy) 
    {
        return Vector3.Distance(enemy.transform.position, this.transform.position);
    }

}
