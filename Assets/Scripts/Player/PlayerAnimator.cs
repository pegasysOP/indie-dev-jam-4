using UnityEngine;


public class PlayerAnimator : MonoBehaviour
{
    public GameObject arms;
    public Animator fpsAnimator;
    public Animator pistolAnimator;

    private Animator equippedAnimator;

    public void SetGun(GunType gunType)
    {
        switch (gunType)
        {
            case GunType.None:
                equippedAnimator?.gameObject.SetActive(false);
                equippedAnimator = null;
                arms.SetActive(false);
                break;
            case GunType.Pistol:
                equippedAnimator?.gameObject.SetActive(false);
                equippedAnimator = pistolAnimator;
                equippedAnimator.gameObject.SetActive(true);
                arms.SetActive(true);
                break;
        }
    }

    public void SetMoveSpeed(float speed)
    {
        fpsAnimator.SetFloat("MoveSpeed", speed);
        if (equippedAnimator != null)
            equippedAnimator.SetFloat("MoveSpeed", speed);
    }

    public void Shoot()
    {
        fpsAnimator.SetTrigger("Shoot");

        if (equippedAnimator != null)
            equippedAnimator.SetTrigger("Shoot");
    }
    
    public void Reload()
    {
        fpsAnimator.SetTrigger("Reload");

        if (equippedAnimator != null)
            equippedAnimator.SetTrigger("Reload");
    }

    public void Die()
    {
        fpsAnimator.SetTrigger("Death");

        if (equippedAnimator != null)
            equippedAnimator.SetTrigger("Death");
    }
    public void Hit()
    {
        fpsAnimator.SetTrigger("Hit");

        if (equippedAnimator != null)
            equippedAnimator.SetTrigger("Hit");
    }

    public void Reset()
    {
        fpsAnimator.SetTrigger("Reset");

        if (equippedAnimator != null)
            equippedAnimator.SetTrigger("Reset");
    }
}
