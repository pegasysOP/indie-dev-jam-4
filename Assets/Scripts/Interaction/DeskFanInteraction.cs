using UnityEngine;

public class DeskFanInteraction : MonoBehaviour, IInteractable
{
    public Animator animator;
    public AudioSource source;

    public void Interact()
    {
        if (animator.GetBool("On"))
        {
            animator.SetTrigger("TurnOff");
            animator.SetBool("On", false);
            Debug.Log($"Desk fan turned OFF");
        }
        else
        {
            animator.SetTrigger("TurnOn");
            animator.SetBool("On", true);
            Debug.Log($"Desk fan turned ON");
        }

        source.Play();
    }
}
