using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IDamageable;

public class LilEnemyDamagePoin : MonoBehaviour, IDamageable
{
    public LilEnemy mainBody;

    public void TakeDamage(int damage)
    {
        mainBody.TakeDamage(damage);
    }

    public DamageType GetDamageType()
    {
        return mainBody.GetDamageType();
    }
}
