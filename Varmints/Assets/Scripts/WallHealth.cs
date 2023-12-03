using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHealth : MonoBehaviour, IDamageable
{
    public int health = 100;
    public float cooldown = 5.0f;
    public int regenRate = 5;
    public MeshRenderer mesh;
    public BoxCollider box;

    void Start() {
        InvokeRepeating("Revive", cooldown, cooldown);
    }

    public void Damage(int damage, Vector3 force)
    {
        health -= damage;
        if (health <= 0)
        {
            Death();
        }
    }
    public void Death() {
        mesh.enabled = false;
        box.enabled = false;
    }

    public void Revive() {
        mesh.enabled = true;
        box.enabled = true;
        if (health < 100) {
            health += regenRate;
        }
    }
}
