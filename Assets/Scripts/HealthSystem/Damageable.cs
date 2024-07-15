using System;
using UnityEngine;
using static IDamageable;

public class Damageable : MonoBehaviour, IDamageable
{
    public int health; 

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        Debug.Log($"{gameObject.name} died.");
        Destroy(gameObject);
    }

    public DamageType GetDamageType()
    {
        return DamageType.None;
    }
}
