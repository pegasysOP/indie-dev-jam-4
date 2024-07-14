using System.Collections;
using UnityEngine;

public class ShootingSystem : MonoBehaviour
{
    public LayerMask enemyMask;
    public AudioSource GunSource;
    private Gun equippedGun;

    public GameObject bulletMark;

    void Update()
    {
        if (equippedGun == null)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            if (equippedGun.CanFire())
                StartCoroutine(Fire());
            else if (equippedGun.CanReload()) // if no ammo then auto reload on click
                StartCoroutine(Reload());
        }
        else if (Input.GetKeyDown(KeyCode.R) && equippedGun.CanReload())
        {
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        equippedGun.isReloading = true;

        yield return new WaitForSeconds(equippedGun.reloadTime);

        equippedGun.Reload();
        UpdateAmmoUI();

        equippedGun.isReloading = false;
    }

    private IEnumerator Fire()
    {
        equippedGun.isFiring = true;

        equippedGun.Fire();
        UpdateAmmoUI();

        AudioController.Instance.PlayGunshot(AmmoType.Pistol);

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, Mathf.Infinity, enemyMask))
        {
            if (hit.collider.TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                damageable.TakeDamage(equippedGun.Damage);
                Debug.Log(hit.collider);

                GameObject decalObject = Instantiate(bulletMark, hit.point + (hit.normal * 0.025f), Quaternion.identity);
                decalObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
                //Destroy(decalObject, 5f);
            }
        }

        yield return new WaitForSeconds(equippedGun.FireDelay);

        equippedGun.isFiring = false;
    }

    private void UpdateAmmoUI()
    {
        if (equippedGun != null)
            HudManager.SetAmmoText(equippedGun.loadedAmmo, equippedGun.MagSize);
    }

    public void EquipGun(Gun gun)
    {
        HudManager.EnableCrosshairAndAmmoCount(true);

        StopAllCoroutines();
        if (equippedGun != null)
        {
            equippedGun.isFiring = false;
            equippedGun.isReloading = false;
        }

        equippedGun = gun;
        UpdateAmmoUI();
    }

    public void Reset()
    {
        HudManager.EnableCrosshairAndAmmoCount(true);

        StopAllCoroutines();
        if (equippedGun != null)
            equippedGun.Initialise();

        UpdateAmmoUI();
    }
}
