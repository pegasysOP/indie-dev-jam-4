using UnityEngine;
using static Gun;

public class AmmoPickup : MonoBehaviour, IInteractable
{
    public GunType ammoType;
    public int ammount;

    public void Interact()
    {
        Debug.Log($"Picked up: {ammount} {ammoType} rounds.");
        Destroy(gameObject);
    }
}
