using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public float interactRange;
    public bool canInteract;
    public LayerMask interactLayer;

    private IInteractable highlightedInteractable;
    private bool paused;

    private void Update()
    {
        HandleInteractionHighlight();

        if (paused)
            return;

        if (Input.GetKeyDown(KeyCode.E) && highlightedInteractable != null)
        {
            highlightedInteractable.Interact();
        }
    }

    private void HandleInteractionHighlight()
    {
        Debug.DrawRay(transform.position, transform.forward * interactRange, Color.red);

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, interactRange, interactLayer))
        {
            if (hit.collider.TryGetComponent<IInteractable>(out IInteractable interactable))
            {
                canInteract = true;
                if (interactable == null)
                    return;

                if (highlightedInteractable == null)
                {
                    highlightedInteractable = interactable;
                    HudManager.ShowInteractPrompt(true);
                    Debug.Log($"{hit.collider.name}");
                }
            }
            else
            {
                if (highlightedInteractable == null)
                    return;

                highlightedInteractable = null;
                HudManager.ShowInteractPrompt(false);
                Debug.Log($"nothing to interact with");
                canInteract = false;
            }
        }
        else
        {
            if (highlightedInteractable == null)
                return;

            highlightedInteractable = null;
            HudManager.ShowInteractPrompt(false);
            Debug.Log($"nothing to interact with");
            canInteract = false;
        }
    }

    public void Pause(bool paused)
    {
        this.paused = paused;
    }
}
