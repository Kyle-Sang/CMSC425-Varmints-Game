using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public int health = 100;

    public void Damage(int damage, Vector3 force)
    {
        health -= damage;
        if (health <= 0)
        {
            Debug.Log("I DIED");
        }
    }
}
