using DG.Tweening;
using UnityEngine;

public class Doorway : MonoBehaviour, IInteractable
{
    public Transform doorToOpen;
    private Vector3 closedPosition;
    public Vector3 openPosition;
    public KeyCardAccess accessLevel;

    public bool isOpen;

    private void Start()
    {
        isOpen = false;
        closedPosition = doorToOpen.position;
    }

    public void Interact()
    {
        if (doorToOpen == null)
            return;

        if (Player.Inventory.KeyCards.Contains(accessLevel)) 
        {
            if (isOpen)
            {
                doorToOpen.DOMove(closedPosition, 0.5f);
                isOpen = false;
            }
            else
            {
                doorToOpen.DOMove(closedPosition + openPosition, 0.5f);
                isOpen = true;
            }
        }
    }
}
