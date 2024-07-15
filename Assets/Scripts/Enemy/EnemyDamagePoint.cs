using UnityEngine;
using static IDamageable;

[RequireComponent(typeof(Collider))]
public class EnemyDamagePoint : MonoBehaviour, IDamageable
{
    public enum DamageLocation
    {
        Body,
        Head,
        LegLeft,
        LegRight

    }

    public Enemy controller;
    public int multiplier;
    public DamageLocation location;

    public void TakeDamage(int damage)
    {
        controller.TakeDamage(damage * multiplier, location);
    }

    public DamageType GetDamageType()
    {
        return controller.GetDamageType();
    }
}
