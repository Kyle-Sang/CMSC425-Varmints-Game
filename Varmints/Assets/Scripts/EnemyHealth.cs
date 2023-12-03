using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public int health = 100;
    public int value = 30;
    public PlayerCurrency playerCurrency;

    private void Start()
    {
        GameObject player = GameObject.FindWithTag("CurrencyManager");
        if (player != null)
        {
            playerCurrency = player.GetComponent<PlayerCurrency>();
        }
        else
        {
            Debug.LogError("Player object not found");
        }
    }

    public void Damage(int damage, Vector3 force)
    {
        health -= damage;
        if (health <= 0)
        {
            Death();
            if (playerCurrency != null)
            {
                playerCurrency.changeMoney(value);  // access count field on PlayerCurrency component
            }
            else
            {
                Debug.LogError("PlayerCurrency component not found");
            }
        }
    }

    public void Death()
    {
        if (GameObject.Find("WaveSpawner") != null)
        {
            GameObject.Find("WaveSpawner").GetComponent<WaveSpawner>().enemies.Remove(gameObject);
            Destroy(gameObject);
        }
    }
}
