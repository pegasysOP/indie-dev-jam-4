using System.Collections;
using System.Collections.Generic;
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
        PlayerController.PlayerHealth.Initialise();
    }

    public static void OnPlayerDeath()
    {
        Debug.Log("PLAYER DIED > RESETTING CHECKPOINT");
        Instance.StartCoroutine(WaitThenDie());
    }

    private static IEnumerator WaitThenDie()
    {
        PlayerController.Animator.Die();
        PlayerController.Lock(true);

        yield return new WaitForSeconds(2f);

        if (Instance.currentCheckpoint != null)
            Instance.currentCheckpoint.Reset();

        PlayerController.Lock(false);
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
