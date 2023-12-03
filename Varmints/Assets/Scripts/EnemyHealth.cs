using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public int health = 100;
    public int value = 30;
    public int cost = 10;
    public PlayerCurrency playerCurrency;
    public Enemy_Audio enmAudio;
    Rigidbody rb;
    AudioSource source;
    AudioManager manager;

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
        rb = GetComponent<Rigidbody>();
        source = rb.GetComponent<AudioSource>();
        StartCoroutine(enmAudio.PlayAudio(source));
        manager = FindObjectOfType<AudioManager>();
    }

    public void Damage(int damage, Vector3 force)
    {
        if (gameObject.tag == "Enemy") {
            health -= damage;
        }
        
        if (health <= 0)
        {
            manager.onDeath();
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
