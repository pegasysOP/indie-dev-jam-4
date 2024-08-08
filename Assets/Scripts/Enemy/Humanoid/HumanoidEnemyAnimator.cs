using System.Collections;
using UnityEngine;
using static HumanoidEnemy;

public class HumanoidEnemyAnimator : MonoBehaviour
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

    public IEnumerator SetCrawling(bool crawling, bool left = false)
    {
        yield return new WaitForSeconds(0.2f);

        animator.SetBool("Crawling", crawling);
        animator.SetBool("Left", left);
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
                StartCoroutine(SetCrawling(true, false));
                break;
            case DamageLocation.LegLeft:
                animator.SetTrigger("HitLegLeft");
                StartCoroutine(SetCrawling(true, true));
                break;
        }

    }

    public void Reset()
    {
        StopAllCoroutines();
        animator.SetTrigger("Reset");
        SetCrawling(false);
    }
}
