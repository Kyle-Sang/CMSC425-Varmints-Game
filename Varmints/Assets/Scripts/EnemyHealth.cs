using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public int health = 100;

    public void Damage(int damage, Vector3 force)
    {
        health -= damage;
        if (health <= 0) {
            Death();
        }
    }

    public void Death()
    {
        Debug.Log("Death Called");
        if (GameObject.Find("WaveSpawner") != null)
        {
            GameObject.Find("WaveSpawner").GetComponent<WaveSpawner>().enemies.Remove(gameObject);
            Destroy(gameObject);
        }
    }
}
