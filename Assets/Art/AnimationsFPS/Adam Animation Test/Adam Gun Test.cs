using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    public Animator animator; // Animator with multiple layers
    public GameObject gunParticleEffectsParent; // Parent object containing all particle effects

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (animator != null)
            {
                animator.SetTrigger("ArmsReloadTrigger"); // Trigger for arms reload animation
                animator.SetTrigger("PistolReloadTrigger"); // Trigger for pistol reload animation
            }
            else
            {
                Debug.LogWarning("Animator is not assigned!");
            }
        }

        // Play fire animation and particle effects when "1" is pressed
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (animator != null)
            {
                animator.SetTrigger("FireTrigger"); // Trigger for fire animation

                // Play the particle effects
                PlayGunParticleEffects();
            }
            else
            {
                Debug.LogWarning("Animator is not assigned!");
            }
        }
    }

    // Method to play all particle effects under the parent object
    void PlayGunParticleEffects()
    {
        if (gunParticleEffectsParent != null)
        {
            // Get all ParticleSystem components in the children of the parent
            ParticleSystem[] particleSystems = gunParticleEffectsParent.GetComponentsInChildren<ParticleSystem>();
            
            // Play each particle system
            foreach (ParticleSystem ps in particleSystems)
            {
                ps.Play();
            }
        }
        else
        {
            Debug.LogWarning("Gun Particle Effects parent is not assigned!");
        }
    }
}
