using static HumanoidEnemy;

public class HumanoidEnemyDamagePoint : EnemyDamagePoint
{
    public DamageLocation location;

    public override void TakeDamage(int damage)
    {
        ((HumanoidEnemy)mainBody).TakeDamage(damage * multiplier, location);
    }
}
