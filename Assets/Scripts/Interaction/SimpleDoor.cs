using UnityEngine;

public class SimpleDoor : MonoBehaviour, IInteractable
{
    public Animator animator;
    public string animationName = "DoorCloseAnimation";
    public Collider interactCollider;
    public AudioClip doorOpenSound;
    private AudioSource source;

    public void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void Interact()
    {
        animator.Play(animationName);
        source.Play();
        interactCollider.enabled = false;
    }
}
