using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurretHealth : MonoBehaviour, IDamageable
{
    public int health = 100;
    public void Damage(int damage, Vector3 force)
    {
        health -= damage;
        if (health <= 0)
        {
            Death();
        }
    }
    public void Death()
    {
        Debug.Log("something broke!");
        Destroy(gameObject);
    }
}
