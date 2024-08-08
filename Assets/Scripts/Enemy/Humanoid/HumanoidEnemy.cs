using UnityEngine;

public class HumanoidEnemy : Enemy
{
    public HumanoidEnemyAnimator animator;

    public enum DamageLocation
    {
        Body,
        Head,
        LegLeft,
        LegRight
    }

    protected override void Update()
    {
        base.Update();

        Vector3 horizontalVelocity = new Vector3(agent.velocity.x, 0f, agent.velocity.z);
        animator.SetMoveSpeed(horizontalVelocity.magnitude / agent.speed);
    }
    public override void TakeDamage(int damage)
    {
        TakeDamage(damage, DamageLocation.Body);
    }

    public void TakeDamage(int damage, DamageLocation location)
    {
        base.TakeDamage(damage);

        if (health > 0)
            animator.Hit(location);

        AudioController.Instance.PlayZombieHurt();
    }

    protected override void Death()
    {
        base.Death();

        animator.Die();
    }

    public override void OnPlayerHit()
    {
        base.OnPlayerHit();

        animator.Attack();
    }

    public override void Reset()
    {
        base.Reset();

        animator.Reset();
    }
}
