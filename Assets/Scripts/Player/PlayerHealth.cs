using System.Collections;
using UnityEngine;
using static IDamageable;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public int maxHealth;
    public int health;

    public float regenTimer;

    public void Initialise()
    {
        health = maxHealth;
        PlayerHurtFlash.Instance.ShowBloodFX(health);
        HudManager.SetHealthText(health, maxHealth);
    }

    public void TakeDamage(int damage)
    {
        StopCoroutine(HealthRegen());
        // if already dead stop taking damage
        if (health <= 0)
            return;

        health -= damage;
        if (health <= 0)
        {
            health = 0;
            GameManager.OnPlayerDeath();
        }
        else
        {
            PlayerController.Animator.Hit();
            StartCoroutine(HealthRegen());
        }

        PlayerHurtFlash.Instance.ShowBloodFX(health);
        AudioController.Instance.PlayHurt();
        HudManager.SetHealthText(health, maxHealth);
    }

    public IEnumerator HealthRegen()
    {
        yield return new WaitForSeconds(regenTimer);
        
        while (health < maxHealth)
        {
            health++;
            PlayerHurtFlash.Instance.ShowBloodFX(health);
            yield return new WaitForSeconds(1f);
        }
    }

    public DamageType GetDamageType()
    {
        return DamageType.None;
    }
}
