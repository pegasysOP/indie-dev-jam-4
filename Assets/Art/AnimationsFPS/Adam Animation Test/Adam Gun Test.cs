using UnityEngine;
using System.Collections.Generic;

public class AnimatorController : MonoBehaviour
{
   [Header("Animation")]
   [SerializeField] private Animator armsAnimator;  // Animator for arms
   [SerializeField] private Animator pistolAnimator; // Animator for pistol
   
   [Header("Animation Layers")]
   [SerializeField] private float ironSightsLayerWeight = 0f;
   [SerializeField] private float ironSightsTransitionSpeed = 10f;
   
   [Header("Key Bindings")]
   [SerializeField] private KeyCode inspectKey = KeyCode.Alpha1;
   [SerializeField] private KeyCode meleeKey = KeyCode.Alpha2;
   [SerializeField] private KeyCode fireKey = KeyCode.Alpha3;
   [SerializeField] private KeyCode reloadKey = KeyCode.Alpha4;
   [SerializeField] private KeyCode reloadEmptyKey = KeyCode.Alpha5;
   [SerializeField] private KeyCode reloadSwapKey = KeyCode.Alpha6;
   [SerializeField] private KeyCode ironSightsKey = KeyCode.Mouse1;

   [Header("Aim Settings")]
   [SerializeField] private float aimTransitionSpeed = 10f;
   [SerializeField] private float currentAimWeight = 0f;
   
   [Header("Effects")]
   [SerializeField] private GameObject gunParticleEffectsParent;
   [SerializeField] private AudioSource gunAudioSource;

   private Dictionary<KeyCode, string> animationTriggers;

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
   }

   private void Update()
   {
       HandleWalking();
       HandleAiming();
       HandleAnimationTriggers();
   }

   private void HandleWalking()
   {
       bool isWalking = Input.GetKey(KeyCode.W);

       // Set walking animation in both the arms and pistol Animator
       if (armsAnimator != null)
       {
           armsAnimator.SetBool("IsWalking", isWalking);
       }
       if (pistolAnimator != null)
       {
           pistolAnimator.SetBool("IsWalking", isWalking);
       }
   }

   private void HandleAiming()
   {
       // Target weight is 1 when aiming, 0 when not
       float targetWeight = Input.GetKey(ironSightsKey) ? 1f : 0f;
       
       // Smoothly interpolate between current and target weight
       currentAimWeight = Mathf.Lerp(currentAimWeight, targetWeight, Time.deltaTime * aimTransitionSpeed);

       // Apply the smoothed weight to the animator
       if (armsAnimator != null)
       {
           armsAnimator.SetFloat("AimWeight", currentAimWeight);
       }
   }

   private void HandleAnimationTriggers()
   {
       foreach (var trigger in animationTriggers)
       {
           if (Input.GetKeyDown(trigger.Key))
           {
               PlayAnimation(trigger.Value);
               
               if (trigger.Key == fireKey)
               {
                   HandleFireEffects();
               }
               break;
           }
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
       if (gunParticleEffectsParent == null)
       {
           Debug.LogWarning("Gun Particle Effects parent is not assigned!");
           return;
       }

       ParticleSystem[] particleSystems = gunParticleEffectsParent.GetComponentsInChildren<ParticleSystem>();
       foreach (ParticleSystem ps in particleSystems)
       {
           ps.Play();
           Debug.Log($"Playing particle system: {ps.name}");
       }
   }
}