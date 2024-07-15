using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public GameObject pistolArms;
    public Animator fpsAnimator;
    public Animator pistolAnimator;
    public Animator shotgunAnimator;
    public Animator uziAnimator;

    private Animator gunAnimator;
    private GunType gunType = GunType.None;

    public void SetGun(GunType gunType)
    {
        this.gunType = gunType;

        switch (gunType)
        {
            case GunType.None:
                gunAnimator?.gameObject.SetActive(false);
                gunAnimator = null;

                pistolArms.SetActive(false);
                fpsAnimator.gameObject.SetActive(true);
                break;
            case GunType.Pistol:
                gunAnimator?.gameObject.SetActive(false);
                gunAnimator = pistolAnimator;
                gunAnimator.gameObject.SetActive(true);

                fpsAnimator.gameObject.SetActive(true);
                pistolArms.SetActive(true);
                break;
            case GunType.Shotgun:
                gunAnimator?.gameObject.SetActive(false);
                gunAnimator = shotgunAnimator;
                gunAnimator.gameObject.SetActive(true);

                fpsAnimator.gameObject.SetActive(false);
                pistolArms.SetActive(false);
                break;
            case GunType.Uzi:
                gunAnimator?.gameObject.SetActive(false);
                gunAnimator = uziAnimator;
                gunAnimator.gameObject.SetActive(true);

                fpsAnimator.gameObject.SetActive(false);
                pistolArms.SetActive(false);
                break;
        }
    }

    public void SetMoveSpeed(float speed)
    {
        if (gunType == GunType.None | gunType == GunType.Pistol)
            fpsAnimator.SetFloat("MoveSpeed", speed);

        if (gunAnimator != null)
            gunAnimator.SetFloat("MoveSpeed", speed);
    }

    public void Shoot()
    {
        if (gunType == GunType.None | gunType == GunType.Pistol)
            fpsAnimator.SetTrigger("Shoot");

        if (gunAnimator != null)
            gunAnimator.SetTrigger("Shoot");
    }
    
    public void Reload()
    {
        if (gunType == GunType.None | gunType == GunType.Pistol)
            fpsAnimator.SetTrigger("Reload");

        if (gunAnimator != null)
            gunAnimator.SetTrigger("Reload");
    }

    public void Die()
    {
        if (gunType == GunType.None | gunType == GunType.Pistol)
            fpsAnimator.SetTrigger("Death");

        if (gunAnimator != null)
            gunAnimator.SetTrigger("Death");
    }
    public void Hit()
    {
        if (gunType == GunType.None | gunType == GunType.Pistol)
            fpsAnimator.SetTrigger("Hit");

        if (gunAnimator != null)
            gunAnimator.SetTrigger("Hit");
    }

    public void Reset()
    {
        fpsAnimator.SetTrigger("Reset");

        if (gunAnimator != null)
            gunAnimator.SetTrigger("Reset");
    }
}
