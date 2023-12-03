using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public int health = 100;
    public int regen = 1;
    public TextMeshProUGUI text;
    void Start() {
        text.SetText(health.ToString());
        InvokeRepeating("Regen", 1, 1);
    }

    private void Regen() {
        if (health < 100) {
            health += regen;
            text.SetText(health.ToString());
        }
    }
    public void Damage(int damage, Vector3 force)
    {
        health -= damage;
        text.SetText(health.ToString());
        if (health <= 0)
        {
            text.SetText("DEAD");
        }
    }
}
