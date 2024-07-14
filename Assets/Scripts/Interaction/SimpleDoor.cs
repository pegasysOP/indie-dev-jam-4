using UnityEngine;

public class SimpleDoor : MonoBehaviour, IInteractable
{
    public Animator animator;
    public AnimationClip clip;
    public Collider interactCollider;
    public AudioClip doorOpenSound;

    public void Interact()
    {
        animator.Play("DoorCloseAnimation");
        AudioController.Instance.PlayDoorOpen(doorOpenSound);
        interactCollider.enabled = false;
    }
}
