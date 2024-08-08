using UnityEngine;

public class EnemyAttackBox : MonoBehaviour
{
    public Enemy mainBody;

    private void OnTriggerEnter(Collider other)
    {
        if ((LayerDefines.PlayerLayerMask.value & (1 << other.gameObject.layer)) != 0)
            mainBody.OnPlayerHit();
    }
}
