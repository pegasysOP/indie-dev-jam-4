public interface IDamageable
{
    public enum DamageType
    {
        None,
        Normal,
        Bigboi,
        Lil
    }

    DamageType GetDamageType();

    void TakeDamage(int damage);
}