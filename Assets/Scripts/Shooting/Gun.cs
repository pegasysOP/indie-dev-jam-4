public enum GunType
{
    None,
    Pistol,
    Mac10,
    Shotgun
}

[System.Serializable]
public class Gun
{
    public GunType gunType;
    public int Damage;
    public int MagSize;
    public float FireDelay;
    public bool AllowAuto;
    public float reloadTime;
    public float swapInTime;
    public float swapOutTime;

    public int loadedAmmo;
    public bool isReloading;
    public bool isFiring;
    public bool isSwapping;

    public void Initialise()
    {
        loadedAmmo = MagSize;
        isReloading = false;
        isFiring = false;
        isSwapping = false;
    }

    public void Reload(int missingShells = 0)
    {
        loadedAmmo = MagSize - missingShells;
    }

    public void Fire()
    {
        loadedAmmo--;
    }

    public bool CanReload()
    {
        return !isReloading && !isFiring && loadedAmmo < MagSize && !isSwapping;
    }
    
    public bool CanFire()
    {
        return !isReloading && !isFiring && loadedAmmo > 0 && !isSwapping;
    }

    public int GetMissingShellCount()
    {
        return MagSize - loadedAmmo;
    }

    public bool CanSwap()
    {
        return !isReloading && !isFiring && !isSwapping;
    }
}
