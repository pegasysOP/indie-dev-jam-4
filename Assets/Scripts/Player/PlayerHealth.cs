using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public int maxHealth;
    public int health;

    public void Initialise()
    {
        health = maxHealth;

        HudManager.SetHealthText(health, maxHealth);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        HudManager.SetHealthText(health, maxHealth);
    }
}
