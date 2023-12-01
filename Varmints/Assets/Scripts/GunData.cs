using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GunData
{
    public GunType type;
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    public int bulletsLeft, bulletsShot;
}
