using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public float interactRange;
    public bool canInteract;
    public LayerMask interactLayer;

    private IInteractable highlightedInteractable;

    private void Update()
    {
        HandleInteractionHighlight();

        if (Input.GetKeyDown(KeyCode.F) && highlightedInteractable != null)
        {
            highlightedInteractable.Interact();
        }
    }

    private void HandleInteractionHighlight()
    {
        Debug.DrawRay(transform.position, transform.forward * interactRange, Color.red);

        if (Physics.Raycast(transform.position, Vector3.forward, out RaycastHit hit, interactRange, interactLayer))
        {
            Debug.Log($"{hit.collider.name}");
            if (hit.collider.TryGetComponent<IInteractable>(out IInteractable interactable))
            {
                canInteract = true;
                if (interactable == null)
                    return;

                if (highlightedInteractable == null)
                {
                    highlightedInteractable = interactable;
                    Debug.Log($"{hit.collider.name}");
                }
            }
            else
            {
                if (highlightedInteractable == null)
                    return;

                highlightedInteractable = null;
                Debug.Log($"nothing to interact with");
                canInteract = false;
            }
        }
        else
        {
            if (highlightedInteractable == null)
                return;

            highlightedInteractable = null;
            Debug.Log($"nothing to interact with");
            canInteract = false;
        }
    }
}
