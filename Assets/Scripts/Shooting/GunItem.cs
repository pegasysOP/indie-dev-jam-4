using UnityEngine;

public class GunItem : MonoBehaviour, IInteractable
{
    public Gun Gun;

    public void Interact()
    {
        PlayerController.Inventory.AddGun(Gun);

        Destroy(gameObject);
    }
}
