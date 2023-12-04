using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float attackSpeed;
    public int damage;
    private float delay = 0.0f;
    void OnTriggerStay(Collider other) {
        if (other.gameObject.tag != "Enemy" && other.TryGetComponent(out IDamageable damageable) && Time.time > delay) {
                damageable.Damage(damage, new Vector3(0, 0, 0));
                delay = Time.time + attackSpeed;
            }
    }
}
