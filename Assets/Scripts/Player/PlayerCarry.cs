using UnityEngine;

public class PlayerCarry : MonoBehaviour
{
    public float carryRange;
    public Transform[] carryTransforms;
    public LayerMask interactLayer;
    /// <summary>
    /// ?
    /// </summary>
    public Transform cameraTransform;

    private BaseCarryable highlightedBaseCarryable;
    private bool paused;
    protected BaseCarryable currentObject;

    private void Update()
    {
        // only check for new carryables if not carrying
        if (currentObject == null)
            HandleCarryHighlight();

        if (paused)
            return;

        if (Input.GetKeyDown(KeyCode.E) && highlightedBaseCarryable != null)
        {
            highlightedBaseCarryable.Carry();
            currentObject = highlightedBaseCarryable;
            currentObject.transform.SetParent(carryTransforms[0]);
            currentObject.transform.rotation = Quaternion.identity;
        }
        else if (Input.GetKeyDown(KeyCode.Q) && currentObject != null)
        {
            Throw();
            currentObject = null;
        }
    }

    private void HandleCarryHighlight()
    {
        //Debug.DrawRay(transform.position, transform.forward * carryRange, Color.blue);

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, carryRange, interactLayer))
        {
            if (hit.collider.TryGetComponent<BaseCarryable>(out BaseCarryable interactable))
            {
                if (interactable == null)
                    return;

                if (highlightedBaseCarryable == null)
                {
                    highlightedBaseCarryable = interactable;
                    //HudManager.ShowInteractPrompt(true);
                    Debug.Log($"{hit.collider.name}");
                }
            }
            else
            {
                if (highlightedBaseCarryable == null)
                    return;

                highlightedBaseCarryable = null;
                //HudManager.ShowInteractPrompt(false);
                Debug.Log($"nothing to interact with");
            }
        }
        else
        {
            if (highlightedBaseCarryable == null)
                return;

            highlightedBaseCarryable = null;
            HudManager.ShowInteractPrompt(false);
            Debug.Log($"nothing to interact with");
        }
    }

    //public void Carry(BaseCarryable newCarryable)
    //{
    //    currentObject = newCarryable;
    //}

    public void Drop()
    {

    }

    public void Throw()
    {
        if (currentObject != null)
        {
            currentObject.Throw(GetThrowDirection());
            currentObject = null;
        }
        else
        {
            Debug.Log("tried to throw but no object was being carried");
        }
    }

    public Vector3 GetThrowDirection()
    {
        Vector3 forceDirection = cameraTransform.transform.forward;

        RaycastHit hit;

        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, 500f))
        {
            forceDirection = (hit.point - carryTransforms[0].position).normalized;
        }

        return forceDirection;
    }

    public void Pause(bool paused)
    {
        this.paused = paused;
    }
}

