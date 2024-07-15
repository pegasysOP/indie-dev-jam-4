using System.Collections;
using UnityEngine;

public class ShootingSystem : MonoBehaviour
{
    public LayerMask enemyMask;
    public LayerMask playerMask;

    public AudioSource GunSource;
    private Gun equippedGun;

    public GameObject bulletMark;
    public GameObject bloodMark;

    void Update()
    {
        if (equippedGun == null)
            return;

        if ((equippedGun.AllowAuto && Input.GetMouseButton(0)) || Input.GetMouseButtonDown(0))
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
        PlayerController.Animator.Reload();
        AudioController.Instance.PlayPistolReload();

        yield return new WaitForSeconds(equippedGun.reloadTime);

        equippedGun.Reload();
        UpdateAmmoUI();

        equippedGun.isReloading = false;
    }

    private IEnumerator Fire()
    {
        equippedGun.isFiring = true;
        PlayerController.Animator.Shoot();

        equippedGun.Fire();
        UpdateAmmoUI();

        AudioController.Instance.PlayGunshot(equippedGun.gunType);

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, Mathf.Infinity, enemyMask))
        {
            if (hit.collider.TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                damageable.TakeDamage(equippedGun.Damage);
                Debug.Log(hit.collider);

                GameObject decalObject = Instantiate(bloodMark, hit.point + (hit.normal * 0.025f), Quaternion.identity, hit.transform);
                decalObject.transform.rotation = Quaternion.FromToRotation(Vector3.forward, hit.normal);
                Destroy(decalObject, 10f);
            }
        }
        else if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, ~playerMask))
        {
            Debug.Log($"{hit.collider} hit at {hit.point}");

            GameObject bulletMarkGo = Instantiate(bulletMark, hit.point + (hit.normal * 0.025f), Quaternion.identity);
            bulletMarkGo.transform.rotation = Quaternion.FromToRotation(Vector3.forward, hit.normal);
            bulletMarkGo.transform.Rotate(Vector3.forward, Random.Range(0f, 360f));
            Destroy(bulletMarkGo, 10f);
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
        PlayerController.Animator.SetGun(gun.gunType);

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
