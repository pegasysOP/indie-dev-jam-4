using UnityEngine;

public enum AmmoType
{
    Pistol,
    SMG,
    Shotgun,
    Special
}

public class AmmoPickup : MonoBehaviour, IInteractable
{
    public AmmoType ammoType;
    public int ammount;

    public void Interact()
    {
        Debug.Log($"Picked up: {ammount} {ammoType} rounds.");
        Destroy(gameObject);
    }
}
