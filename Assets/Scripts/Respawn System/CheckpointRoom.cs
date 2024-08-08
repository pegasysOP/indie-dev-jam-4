using System.Collections.Generic;
using UnityEngine;

public class CheckpointRoom : MonoBehaviour
{
    public int Id;
    public LayerMask playerMask;
    public Transform spawnTransform;
    public List<Enemy> associatedEnemies = new List<Enemy>();

    public void Reset()
    {
        foreach (Enemy enemy in associatedEnemies)
        {
            enemy.Reset();
            enemy.Activate();
        }

        //PlayerController.Reset(spawnTransform.position);
        Player.Reset(spawnTransform.position);
    }

    private void OnTriggerStay(Collider other)
    {
        if ((playerMask.value & (1 << other.gameObject.layer)) != 0)
            GameManager.OnCheckpointEnter(this);
    }
}
