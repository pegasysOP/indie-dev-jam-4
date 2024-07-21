using UnityEngine;

public class DeskNotesInteraction : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        PopupPanel.Instance.ShowPopup("This is some text to display");
    }
}
