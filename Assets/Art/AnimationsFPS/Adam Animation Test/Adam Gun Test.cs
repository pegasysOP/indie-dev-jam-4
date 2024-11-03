using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    public Animator armsAnimator;
    public Animator pistolAnimator;
    public GameObject gunParticleEffectsParent;
    public AudioSource gunAudioSource;

    void Update()
    {
        // Handle melee animation
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (armsAnimator != null)
            {
                armsAnimator.SetTrigger("Melee_T");
                Debug.Log("Arms melee triggered");
            }
            else
            {
                Debug.LogWarning("Arms Animator is not assigned!");
            }

            if (pistolAnimator != null)
            {
                pistolAnimator.SetTrigger("Melee_T");
                Debug.Log("Pistol melee triggered");
            }
            else
            {
                Debug.LogWarning("Pistol Animator is not assigned!");
            }
        }

        // Handle inspect animation
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (armsAnimator != null)
            {
                armsAnimator.SetTrigger("Inspect_T");
                Debug.Log("Arms inspect triggered");
            }
            else
            {
                Debug.LogWarning("Arms Animator is not assigned!");
            }

            if (pistolAnimator != null)
            {
                pistolAnimator.SetTrigger("Inspect_T");
                Debug.Log("Pistol inspect triggered");
            }
            else
            {
                Debug.LogWarning("Pistol Animator is not assigned!");
            }
        }

        // Handle fire animation, sound, and particle effects
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (armsAnimator != null)
            {
                armsAnimator.SetTrigger("Fire_T");
                Debug.Log("Fire_T set for arms");
            }
            else
            {
                Debug.LogWarning("Arms Animator is not assigned!");
            }

            if (pistolAnimator != null)
            {
                pistolAnimator.SetTrigger("Fire_T");
                Debug.Log("Fire_T set for pistol");

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

        // Handle reload animation
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (armsAnimator != null)
            {
                armsAnimator.SetTrigger("Reload_T");
                Debug.Log("Arms reload triggered");
            }
            else
            {
                Debug.LogWarning("Arms Animator is not assigned!");
            }

            if (pistolAnimator != null)
            {
                pistolAnimator.SetTrigger("Reload_T");
                Debug.Log("Pistol reload triggered");
            }
            else
            {
                Debug.LogWarning("Pistol Animator is not assigned!");
            }
        }

        // Handle empty reload animation
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            if (armsAnimator != null)
            {
                armsAnimator.SetTrigger("ReloadEMPTY_T");
                Debug.Log("Arms empty reload triggered");
            }
            else
            {
                Debug.LogWarning("Arms Animator is not assigned!");
            }

            if (pistolAnimator != null)
            {
                pistolAnimator.SetTrigger("ReloadEMPTY_T");
                Debug.Log("Pistol empty reload triggered");
            }
            else
            {
                Debug.LogWarning("Pistol Animator is not assigned!");
            }
        }

        // Handle swap reload animation
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            if (armsAnimator != null)
            {
                armsAnimator.SetTrigger("ReloadSWAP_T");
                Debug.Log("Arms swap reload triggered");
            }
            else
            {
                Debug.LogWarning("Arms Animator is not assigned!");
            }

            if (pistolAnimator != null)
            {
                pistolAnimator.SetTrigger("ReloadSWAP_T");
                Debug.Log("Pistol swap reload triggered");
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