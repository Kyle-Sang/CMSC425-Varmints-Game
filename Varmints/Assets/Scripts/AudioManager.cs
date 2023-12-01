using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public float volume = 1f;
    
    public AudioClip[] enemyDeath;
    public Sound[] gunSound;
    public AudioClip dryFire;


    private AudioSource source;
    private System.Random rand = new System.Random();

    private float lastShootTime;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void onDeath()
    {
        int i = rand.Next(enemyDeath.Length);
        source.PlayOneShot(enemyDeath[i], volume);
    }

    public void fire(GunType type, PlayerGun gunData)
    {
        if (type == GunType.None)
        {
            if (Time.time > lastShootTime + 0.2f)//gunData.bulletsPerTap)
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

}