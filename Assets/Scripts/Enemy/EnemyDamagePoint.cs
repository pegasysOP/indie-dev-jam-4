using UnityEngine;

public class EnemyDamagePoint : MonoBehaviour, IDamageable
{
    public Enemy mainBody;
    public int multiplier = 1;

    public virtual void TakeDamage(int damage)
    {
        mainBody.TakeDamage(damage * multiplier);
    }
}
