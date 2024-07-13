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
        // if already dead stop taking damage
        if (health <= 0)
            return;

        health -= damage;
        if (health <= 0)
        {
            health = 0;
            GameManager.OnPlayerDeath();
        }

        HudManager.SetHealthText(health, maxHealth);
    }
}
