using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [Header("Components")]
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private PlayerAnimator animator;
    [SerializeField] private PlayerInventory inventory;
    [SerializeField] private PlayerHealth health;
    [SerializeField] private ShootingSystem shooting;
    [SerializeField] private PlayerInteract interact;
    [SerializeField] private PlayerAudio sound;

    public static PlayerMovement Movement { get { return Instance.movement; } }
    public static PlayerAnimator Animator { get { return Instance.animator; } }
    public static PlayerInventory Inventory { get { return Instance.inventory; } }
    public static PlayerHealth Health { get { return Instance.health; } }
    public static ShootingSystem Shooting { get {  return Instance.shooting; } }
    public static PlayerInteract Interact {  get { return Instance.interact; } }
    public static PlayerAudio Sound { get { return Instance.sound; } }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public static void Initialise()
    {
        // initialise the player instance + all subsystems
    }

    public static void Reset(Vector3 spawnPosition)
    {
        // reset the player instance + all subsystems
    }

    public static void SetSensitivity(float sensitivity)
    {
        Movement.SetSensitivity(sensitivity);
    }

    public static void Pause(bool paused)
    {
        Movement.Pause(paused);
        Shooting.Pause(paused);
        Interact.Pause(paused);
    }
}
