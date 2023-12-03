using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public int health = 100;
    public static bool dead = false;
    public int regen = 1;
    public TextMeshProUGUI text;
    private AudioManager manager;
    public GameObject gameOverMenu;
    void Start() {
        text.SetText(health.ToString());

        manager = FindObjectOfType<AudioManager>();

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
        manager.onHit();
        text.SetText(health.ToString());
        if (health <= 0)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            gameOverMenu.SetActive(true);
            Time.timeScale = 0f;
            dead = true;
        }
    }
}
