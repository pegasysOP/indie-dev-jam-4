using UnityEngine;


public enum KeyCardAccess
{
    None,
    Engine,
    Helm,
    Master
}

public class KeyCard : MonoBehaviour, IInteractable
{
    public KeyCardAccess accessLevel;

    public void Interact()
    {
        PlayerInventory.Instance.AddKeycard(accessLevel);
        Debug.Log($"Picked up: {accessLevel} keycard.");
        Destroy(gameObject);
    }
}
