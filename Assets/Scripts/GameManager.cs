using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private CheckpointRoom currentCheckpoint;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Player.Initialise();
    }

    public static void OnPlayerDeath()
    {
        Debug.Log("PLAYER DIED > RESETTING CHECKPOINT");
        Instance.StartCoroutine(WaitThenDie());
    }

    private static IEnumerator WaitThenDie()
    {
        Player.Animator.Die();
        //PlayerController.Lock(true);

        yield return new WaitForSeconds(1f);

        Blinky.Instance.CloseEyes();

        yield return new WaitForSeconds(1f);

        if (Instance.currentCheckpoint != null)
            Instance.currentCheckpoint.Reset();

        //PlayerController.Lock(false);

        Blinky.Instance.OpenEyes();
        yield return new WaitForSeconds(0.5f);
    }

    public static void OnCheckpointEnter(CheckpointRoom checkpointRoom)
    {
        // ignore earlier checkpoints
        if (Instance.currentCheckpoint != null && checkpointRoom.Id <= Instance.currentCheckpoint.Id)
            return;

        Instance.currentCheckpoint = checkpointRoom;
        Debug.Log($"CHECKPOINT UPDATED [{checkpointRoom.Id}]");
    }
}
