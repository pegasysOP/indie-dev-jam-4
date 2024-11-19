using UnityEngine;
using System.Collections.Generic;

public class AnimatorController : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] private Animator armsAnimator;  // Animator for arms
    [SerializeField] private Animator pistolAnimator; // Animator for pistol

    [Header("Key Bindings")]
    [SerializeField] private KeyCode strafeLeftKey;
    [SerializeField] private KeyCode strafeRightKey;
    [SerializeField] private KeyCode walkBackwardsKey;
    [SerializeField] private KeyCode walkForwardKey;
    [SerializeField] private KeyCode inspectKey = KeyCode.Alpha1;
    [SerializeField] private KeyCode meleeKey = KeyCode.Alpha2;
    [SerializeField] private KeyCode fireKey = KeyCode.Alpha3;
    [SerializeField] private KeyCode reloadKey = KeyCode.Alpha4;
    [SerializeField] private KeyCode reloadEmptyKey = KeyCode.Alpha5;
    [SerializeField] private KeyCode reloadSwapKey = KeyCode.Alpha6;
    [SerializeField] private KeyCode ironSightsKey = KeyCode.Mouse1;

    [Header("Effects")]
    [SerializeField] private GameObject gunParticleEffectsParent;
    [SerializeField] private AudioSource gunAudioSource;

    [Header("Iron Sights Settings")]
    [SerializeField] private float aimTransitionSpeed = 10f; // Serialized transition speed

    private Dictionary<KeyCode, string> animationTriggers;
    private ParticleSystem[] cachedParticleSystems;
    private float currentAimWeight = 0f; // Internal aim weight variable

    private void Awake()
    {
        // Initialize dictionary with serialized keys
        animationTriggers = new Dictionary<KeyCode, string>
        {
            { inspectKey, "Inspect_T" },
            { meleeKey, "Melee_T" },
            { fireKey, "Fire_T" },
            { reloadKey, "Reload_T" },
            { reloadEmptyKey, "ReloadEMPTY_T" },
            { reloadSwapKey, "ReloadSWAP_T" }
        };

        // Cache particle systems
        if (gunParticleEffectsParent != null)
        {
            cachedParticleSystems = gunParticleEffectsParent.GetComponentsInChildren<ParticleSystem>();
        }
    }

    private void Update()
    {
        Debug.Log("Update method called");
        HandleWalking();
        HandleTilt(); // Adjust for 2D Freeform Directional
        HandleAnimationTriggers();
        HandleIronSights();
    }

   private void HandleWalking()
{
    bool isWalking = Input.GetKey(walkForwardKey) || Input.GetKey(strafeLeftKey) ||
                     Input.GetKey(strafeRightKey) || Input.GetKey(walkBackwardsKey);
    Debug.Log("Walking: " + isWalking);

    // Set walking animation in both the arms and pistol Animator
    armsAnimator?.SetBool("IsWalking", isWalking);
    pistolAnimator?.SetBool("IsWalking", isWalking);
}

private void HandleTilt()
{
    float targetVerticalTilt = 0f;
    float targetHorizontalTilt = 0f;

    if (Input.GetKey(strafeLeftKey))
    {
        targetHorizontalTilt = -1f; // Tilt left
    }
    else if (Input.GetKey(strafeRightKey))
    {
        targetHorizontalTilt = 1f; // Tilt right
    }

    if (Input.GetKey(walkBackwardsKey))
    {
        targetVerticalTilt = -1f; // Walk backwards tilt (adjust as needed)
    }

    // Smoothly transition to the target values using Mathf.Lerp
    if (armsAnimator != null)
    {
        float verticalTilt = Mathf.Lerp(armsAnimator.GetFloat("VerticalTilt"), targetVerticalTilt, Time.deltaTime * 10f);
        float horizontalTilt = Mathf.Lerp(armsAnimator.GetFloat("HorizontalTilt"), targetHorizontalTilt, Time.deltaTime * 10f);

        armsAnimator.SetFloat("VerticalTilt", verticalTilt);
        armsAnimator.SetFloat("HorizontalTilt", horizontalTilt);
    }
    else
    {
        Debug.LogWarning("Arms Animator is not assigned!");
    }
}



    private void HandleAnimationTriggers()
    {
        foreach (var trigger in animationTriggers)
        {
            if (Input.GetKeyDown(trigger.Key))
            {
                Debug.Log("Trigger key pressed: " + trigger.Key);
                PlayAnimation(trigger.Value);

                if (trigger.Key == fireKey)
                {
                    HandleFireEffects();
                }
                break;
            }
        }
    }

   private void HandleIronSights()
{
    if (armsAnimator != null)
    {
        // Verify the layer index for "Iron Sights"
        int ironSightsLayerIndex = armsAnimator.GetLayerIndex("Iron Sights");

        if (ironSightsLayerIndex != -1)
        {
            float targetWeight = Input.GetKey(ironSightsKey) ? 1f : 0f;
            Debug.Log("IronSights targetWeight: " + targetWeight);

            // Update aim weight to reach the target value
            currentAimWeight = Mathf.MoveTowards(currentAimWeight, targetWeight, Time.deltaTime * aimTransitionSpeed);

            armsAnimator.SetFloat("IronSights", currentAimWeight);
            armsAnimator.SetLayerWeight(ironSightsLayerIndex, currentAimWeight);
            Debug.Log($"IronSights value: {currentAimWeight}");
        }
        else
        {
            Debug.LogWarning("Layer 'Iron Sights' not found!");
        }
    }
    else
    {
        Debug.LogWarning("Arms Animator is not assigned!");
    }
}

    private void PlayAnimation(string triggerName)
    {
        TryTriggerAnimation(armsAnimator, triggerName, "Arms");
        TryTriggerAnimation(pistolAnimator, triggerName, "Pistol");
    }

    private void TryTriggerAnimation(Animator animator, string triggerName, string animatorName)
    {
        if (animator != null)
        {
            animator.SetTrigger(triggerName);
            Debug.Log($"{animatorName} {triggerName} triggered");
        }
        else
        {
            Debug.LogWarning($"{animatorName} Animator is not assigned!");
        }
    }

    private void HandleFireEffects()
    {
        PlayGunSound();
        PlayGunParticleEffects();
    }

    private void PlayGunSound()
    {
        if (gunAudioSource != null)
        {
            gunAudioSource.Play();
            Debug.Log("Gun sound played");
            return;
        }
        Debug.LogWarning("Gun AudioSource is not assigned!");
    }

    private void PlayGunParticleEffects()
    {
        if (cachedParticleSystems == null)
        {
            Debug.LogWarning("Gun Particle Effects not initialized!");
            return;
        }

        foreach (ParticleSystem ps in cachedParticleSystems)
        {
            ps.Play();
            Debug.Log($"Playing particle system: {ps.name}");
        }
    }

    private void OnDestroy()
    {
        // Clean up cached particle systems
        if (cachedParticleSystems != null)
        {
            foreach (var ps in cachedParticleSystems)
            {
                Destroy(ps.gameObject);
            }
        }
    }
}
