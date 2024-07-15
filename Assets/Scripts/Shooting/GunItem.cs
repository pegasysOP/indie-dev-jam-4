using UnityEngine;

public class GunItem : MonoBehaviour, IInteractable
{
    public Gun Gun;

    public void Interact()
    {
        if (TryGetComponent(out TriggerActionObject action))
            action.Trigger();

        PlayerController.Inventory.AddGun(Gun);

        Destroy(gameObject);
    }
}
