[System.Serializable]
public class Gun
{
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

    public void Reload()
    {
        loadedAmmo = MagSize;
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
}
