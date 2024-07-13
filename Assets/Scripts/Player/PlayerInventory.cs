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

        PlayerController.ShootingSystem.EquipGun(gun);
    }
}
