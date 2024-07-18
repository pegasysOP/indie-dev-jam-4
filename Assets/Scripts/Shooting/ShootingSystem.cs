using System.Collections;
using UnityEngine;

public class ShootingSystem : MonoBehaviour
{
    public LayerMask ignoreMask;

    private Gun equippedGun;

    public GameObject bulletMark;
    public GameObject bloodMark;

    public GameObject muzzleFlash;
    public Transform muzzleFlashPoint;

    private void Start()
    {
        //AudioController.Instance.GunSource = GunSource;
    }

    void Update()
    {
        if (PlayerController.GetLock())
            return;

        if (equippedGun == null)
            return;

        if (Input.GetKeyDown(KeyCode.R) && equippedGun.CanReload())
        {
            StartCoroutine(Reload());
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1) && equippedGun.CanSwap() && PlayerController.Inventory.CanSwapTo(1, equippedGun.gunType))
        {
            StartCoroutine(SwapWeapon(1));
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && equippedGun.CanSwap() && PlayerController.Inventory.CanSwapTo(2, equippedGun.gunType))
        {
            StartCoroutine(SwapWeapon(2));
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && equippedGun.CanSwap() && PlayerController.Inventory.CanSwapTo(3, equippedGun.gunType))
        {
            StartCoroutine(SwapWeapon(3));
        }
        else if ((equippedGun.AllowAuto && Input.GetMouseButton(0)) || Input.GetMouseButtonDown(0))
        {
            if (equippedGun.CanFire())
                StartCoroutine(Fire());
            else if (equippedGun.CanReload()) // if no ammo then auto reload on click
                StartCoroutine(Reload());
        }
    }

    private IEnumerator SwapWeapon(int newPosition)
    {
        Gun oldGun = equippedGun;
        equippedGun = PlayerController.Inventory.GetGunAt(newPosition);

        oldGun.isSwapping = true;
        equippedGun.isSwapping = true;

        yield return new WaitForSeconds(oldGun.swapOutTime);

        PlayerController.Animator.SetGun(GunType.None);

        yield return new WaitForSeconds(equippedGun.swapInTime);

        PlayerController.Animator.SetGun(equippedGun.gunType);
        oldGun.isSwapping = false;
        equippedGun.isSwapping = false;
        UpdateAmmoUI();
    }

    private IEnumerator Reload()
    {
        equippedGun.isReloading = true;
        int shellsToLoad = equippedGun.gunType == GunType.Shotgun ? equippedGun.GetMissingShellCount() : 1;
        PlayerController.Animator.Reload(shellsToLoad);
        AudioController.Instance.PlayReload(equippedGun.gunType);

        yield return new WaitForSeconds(equippedGun.reloadTime * shellsToLoad);

        equippedGun.Reload();
        UpdateAmmoUI();

        
        equippedGun.isReloading = false;
    }

    public void StopReload(int missingShells)
    {
        StopCoroutine(Reload());

        equippedGun.Reload(missingShells);
        UpdateAmmoUI();

        // wait for exit time

        equippedGun.isReloading = false;
    }

    private IEnumerator Fire()
    {
        equippedGun.isFiring = true;
        PlayerController.Animator.Shoot();

        equippedGun.Fire();
        UpdateAmmoUI();

        AudioController.Instance.PlayGunshot(equippedGun.gunType);

        //muzzle flash
        GameObject flash = Instantiate(muzzleFlash, muzzleFlashPoint.position, Quaternion.identity, muzzleFlashPoint);
        Destroy(flash, 0.1f);

        if (equippedGun.gunType != GunType.Shotgun)
            FireAt(Vector3.zero);
        else
            FireSpread();

        yield return new WaitForSeconds(equippedGun.FireDelay);

        equippedGun.isFiring = false;
    }
    private void FireAt(Vector3 directionOffset)
    {
        if (Physics.Raycast(transform.position, transform.forward + directionOffset, out RaycastHit hit, Mathf.Infinity, ~ignoreMask))
        {
            if (hit.collider.TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                damageable.TakeDamage(equippedGun.Damage);
                Debug.Log(hit.collider);

                if (damageable.GetDamageType() == IDamageable.DamageType.Bigboi)
                    return;

                GameObject decalObject = Instantiate(bloodMark, hit.point /*+ (hit.normal * 0.025f)*/, Quaternion.identity, hit.transform);
                decalObject.transform.rotation = Quaternion.FromToRotation(Vector3.forward, hit.normal);
                Destroy(decalObject, 10f);
            }
            else
            {
                Debug.Log($"{hit.collider} hit at {hit.point}");

                GameObject bulletMarkGo = Instantiate(bulletMark, hit.point + (hit.normal * 0.025f), Quaternion.identity);
                bulletMarkGo.transform.rotation = Quaternion.FromToRotation(Vector3.forward, hit.normal);
                bulletMarkGo.transform.Rotate(Vector3.forward, Random.Range(0f, 360f));
                Destroy(bulletMarkGo, 10f);
            }
        }
    }

    private void FireSpread()
    {
        Vector3[] directions = new Vector3[9];
        directions[0] = transform.forward;
        float angleStep = 360f / 8;

        for (int i = 1; i <= 8; i++)
        {
            float angle = angleStep * (i - 1);
            float radian = angle * Mathf.Deg2Rad;
            directions[i] = new Vector3(Mathf.Cos(radian), 0, Mathf.Sin(radian)) * 0.05f;
        }

        foreach (Vector3 direction in directions)
        {
            FireAt(direction);
        }
    }

    private void UpdateAmmoUI()
    {
        //if (equippedGun != null)
        //    HudManager.SetAmmoText(equippedGun.loadedAmmo, equippedGun.MagSize);
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
