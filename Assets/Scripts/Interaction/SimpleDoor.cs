using UnityEngine;

public class SimpleDoor : MonoBehaviour, IInteractable
{
    public Animator animator;
    public AnimationClip clip;
    public Collider interactCollider;
    public AudioClip doorOpenSound;
    private AudioSource source;

    public void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void Interact()
    {
        animator.Play("DoorCloseAnimation");
        source.Play();
        interactCollider.enabled = false;
    }
}
