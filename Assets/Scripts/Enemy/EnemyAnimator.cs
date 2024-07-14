using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyDamagePoint;

public class EnemyAnimator : MonoBehaviour
{
    public Animator animator;

    public void SetMoveSpeed(float speed)
    {
        animator.SetFloat("MoveSpeed", speed);
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");
    }

    public void Die()
    {
        animator.SetTrigger("Death");
    }

    public void Hit(DamageLocation location)
    {
        switch(location)
        {
            case DamageLocation.Body:
                animator.SetTrigger("HitBody");
                break;
            case DamageLocation.Head:
                animator.SetTrigger("HitHead");
                break;
            case DamageLocation.LegRight:
                animator.SetTrigger("HitLegRight");
                break;
            case DamageLocation.LegLeft:
                animator.SetTrigger("HitLegLeft");
                break;
        }

    }

    public void Reset()
    {
        animator.SetTrigger("Reset");
    }
}
