using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public static bool dead = false;
    public int health = 100;
    public GameObject gameOverMenu;
    public TextMeshProUGUI text;

    public void Damage(int damage, Vector3 force)
    {
        health -= damage;
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

    private void Update()
    {
        text.SetText(health.ToString());
    }
}
