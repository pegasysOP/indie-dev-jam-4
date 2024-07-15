public enum GunType
{
    None,
    Pistol,
    Uzi,
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

    public int loadedAmmo;
    public bool isReloading;
    public bool isFiring;

    public void Initialise()
    {
        loadedAmmo = MagSize;
        isReloading = false;
        isFiring = false;
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
        return !isReloading && !isFiring && loadedAmmo < MagSize;
    }
    
    public bool CanFire()
    {
        return !isReloading && !isFiring && loadedAmmo > 0;
    }

    public int GetMissingShellCount()
    {
        return MagSize - loadedAmmo;
    }
}
