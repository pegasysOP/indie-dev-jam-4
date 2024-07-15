using UnityEngine;

public class GunItem : MonoBehaviour, IInteractable
{
    public Gun Gun;

    public void Interact()
    {
        if (Gun.gunType == GunType.Pistol)
        {
            if (TryGetComponent(out TriggerActionObject action))
                action.Trigger();
        }

        PlayerController.Inventory.AddGun(Gun);

        Destroy(gameObject);
    }
}
