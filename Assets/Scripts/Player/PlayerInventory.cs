using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<KeyCardAccess> KeyCards = new List<KeyCardAccess> { KeyCardAccess.None };
    public List<Gun> Guns = new List<Gun>();

    public void AddKeycard(KeyCardAccess card)
    {
        if (!KeyCards.Contains(card))
        {
            KeyCards.Add(card);
        }
    }

    public void AddGun(Gun gun)
    {
        gun.Initialise();
        Guns.Add(gun);

        Player.Shooting.EquipGun(gun);
    }

    public bool CanSwapTo(int newPosition, GunType currentGunType)
    {
        if (newPosition > Guns.Count)
            return false;

        if (Guns[newPosition - 1].gunType == currentGunType) // cuurent gun already equipped
            return false;

        return true;
    }

    public Gun GetGunAt(int position)
    {
        return Guns[position - 1];
    }
}
