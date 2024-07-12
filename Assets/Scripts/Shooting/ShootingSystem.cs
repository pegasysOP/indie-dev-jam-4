using System.Collections;
using UnityEngine;

public class ShootingSystem : MonoBehaviour
{
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

        if (Physics.Raycast(transform.position, Vector3.forward, out RaycastHit hit, Mathf.Infinity))
        {
            if (hit.collider.TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                damageable.TakeDamage(damage);
                Debug.Log(hit.collider);
            }
        }
    }
}
