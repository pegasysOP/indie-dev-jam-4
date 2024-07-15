using UnityEngine;

public class CollisionTrigger : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerController playerController))
        {
            if (TryGetComponent(out TriggerActionObject actionObject))
            {
                actionObject.Trigger();
            }
        }
    }
}
