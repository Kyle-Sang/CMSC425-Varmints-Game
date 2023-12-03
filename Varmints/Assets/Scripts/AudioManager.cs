using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public float volume = 1f;
    
    public AudioClip[] enemyDeath;
    public AudioClip[] playerHit;
    public AudioClip[] wallHit;
    public AudioClip[] pillarSpawn;
    public Sound[] gunSound;
    public AudioClip dryFire;
    public AudioClip torchSpawn;
    public AudioClip turretSpawn;

    private AudioSource source;
    private System.Random rand = new System.Random();

    private float lastShootTime;
    private float lastHitTime;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void onDeath()
    {
        int i = rand.Next(enemyDeath.Length);
        source.PlayOneShot(enemyDeath[i], volume);
    }

    public void onHit()
    {
        if (Time.time > lastHitTime + 0.3f)
        {
            lastHitTime = Time.time;
            int i = rand.Next(playerHit.Length);
            source.PlayOneShot(playerHit[i], volume);
        }
    }

    public void fire(GunType type, float betweenShots)
    {
        if (type == GunType.None)
        {
            if (Time.time > lastShootTime + betweenShots)
            {
                lastShootTime = Time.time;
                source.PlayOneShot(dryFire, volume);
            }
        }
        else
        {
            Sound gun = Array.Find(gunSound, x => x.type == type);
            source.PlayOneShot(gun.Fire, volume);
            lastShootTime = Time.time;
        }
    }

    public void reload(GunType type)
    {
        if (type != GunType.None)
        {
            Sound reload = Array.Find(gunSound, y => y.type == type);
            source.PlayOneShot(reload.Reload, volume);
        }
    }

    public void spawn(string type)
    {
        switch (type)
        {
            case "Turret":
                source.PlayOneShot(turretSpawn, volume);
                break;
            case "pillar":
                int i = rand.Next(pillarSpawn.Length);
                source.PlayOneShot(pillarSpawn[i], volume);
                break;
            case "torch":
                source.PlayOneShot(torchSpawn, volume);
                break;
        }
    }

}