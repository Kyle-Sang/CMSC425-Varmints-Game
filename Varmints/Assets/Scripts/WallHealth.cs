using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHealth : MonoBehaviour, IDamageable
{
    public int health = 100;
    public float cooldown = 5.0f;
    public MeshRenderer mesh;
    public BoxCollider box;

    public void Damage(int damage, Vector3 force)
    {
        health -= damage;
        if (health <= 0)
        {
            StartCoroutine(Death());
        }
    }
    IEnumerator Death() {
        mesh.enabled = false;
        box.enabled = false;
        yield return new WaitForSecondsRealtime(cooldown);
        Revive();
    }

    public void Revive() {
        mesh.enabled = true;
        box.enabled = true;
        health = 100;
    }
}
