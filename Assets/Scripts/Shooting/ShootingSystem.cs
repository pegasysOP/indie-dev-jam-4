using System.Collections;
using TMPro;
using UnityEngine;

public class ShootingSystem : MonoBehaviour
{
    public LayerMask enemyMask;
    public int bulletsPerMag;
    public int totalBulletCount;
    public int currentBulletsInMag;
    public int damage;

    public float reloadSpeed;

    public bool isReloading;

    private void Start()
    {
        isReloading = false;
        currentBulletsInMag = bulletsPerMag;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isReloading)
        {
            Fire();
        }
        if (Input.GetKeyDown(KeyCode.R) && !isReloading)
        {
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        isReloading = true;

        if (totalBulletCount < bulletsPerMag)
        {
            currentBulletsInMag = totalBulletCount;
        }
        else
        {
            currentBulletsInMag = bulletsPerMag;
        }

        if (totalBulletCount <= 0)
            totalBulletCount = 0;

        yield return new WaitForSeconds(reloadSpeed);
        isReloading = false;

        HudManager.SetAmmoText(currentBulletsInMag);
    }

    private bool CanReload()
    {
        return totalBulletCount > 0;
    }

    private void Fire()
    {
        if (currentBulletsInMag <= 0 && !CanReload())
            return;

        if (currentBulletsInMag <= 0 && CanReload())
        {
            StartCoroutine(Reload());
            return;
        }

        currentBulletsInMag--;
        totalBulletCount--;

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, Mathf.Infinity, enemyMask))
        {
            if (hit.collider.TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                damageable.TakeDamage(damage);
                Debug.Log(hit.collider);
            }
        }

        HudManager.SetAmmoText(currentBulletsInMag);
    }
}
