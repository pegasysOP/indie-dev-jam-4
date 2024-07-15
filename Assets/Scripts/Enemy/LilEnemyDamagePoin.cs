using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilEnemyDamagePoin : MonoBehaviour, IDamageable
{
    public LilEnemy mainBody;

    public void TakeDamage(int damage)
    {
        mainBody.TakeDamage(damage);
    }
}
