using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    public Animator armsAnimator; // Animator for arms
    public Animator pistolAnimator; // Animator for pistol
    public GameObject gunParticleEffectsParent; // Parent object containing the particle effects
    public AudioSource gunAudioSource; // AudioSource for the gun sound

    void Update()
    {
        // Handle reload animation
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (armsAnimator != null)
            {
                armsAnimator.SetTrigger("ArmsReloadTrigger"); // Trigger for arms reload animation
                Debug.Log("Arms reload triggered");
            }
            else
            {
                Debug.LogWarning("Arms Animator is not assigned!");
            }

            if (pistolAnimator != null)
            {
                pistolAnimator.SetTrigger("PistolReloadTrigger"); // Trigger for pistol reload animation
                Debug.Log("Pistol reload triggered");
            }
            else
            {
                Debug.LogWarning("Pistol Animator is not assigned!");
            }
        }

        // Handle fire animation, sound, and particle effects
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (armsAnimator != null)
            {
                armsAnimator.SetTrigger("FireTrigger"); // Trigger for arms fire animation
                Debug.Log("FireTrigger set for arms");
            }
            else
            {
                Debug.LogWarning("Arms Animator is not assigned!");
            }

            if (pistolAnimator != null)
            {
                pistolAnimator.SetTrigger("FireTrigger"); // Trigger for pistol fire animation
                Debug.Log("FireTrigger set for pistol");

                // Play gun sound
                PlayGunSound();

                // Play particle effects
                PlayGunParticleEffects();
            }
            else
            {
                Debug.LogWarning("Pistol Animator is not assigned!");
            }
        }
    }

    void PlayGunSound()
    {
        if (gunAudioSource != null)
        {
            gunAudioSource.Play();
            Debug.Log("Gun sound played");
        }
        else
        {
            Debug.LogWarning("Gun AudioSource is not assigned!");
        }
    }

    void PlayGunParticleEffects()
    {
        if (gunParticleEffectsParent != null)
        {
            ParticleSystem[] particleSystems = gunParticleEffectsParent.GetComponentsInChildren<ParticleSystem>();
            
            foreach (ParticleSystem ps in particleSystems)
            {
                ps.Play();
                Debug.Log("Playing particle system: " + ps.name);
            }
        }
        else
        {
            Debug.LogWarning("Gun Particle Effects parent is not assigned!");
        }
    }
}
