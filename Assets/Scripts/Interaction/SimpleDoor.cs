using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDoor : MonoBehaviour, IInteractable
{
    public Animator animator;
    public AnimationClip clip;
    public Collider interactCollider;

    public void Interact()
    {
        animator.Play("DoorCloseAnimation");

        interactCollider.enabled = false;
    }
}
