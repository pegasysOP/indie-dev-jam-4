using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilEnemyAttackBox : MonoBehaviour
{
    public LilEnemy mainBody;
    public LayerMask playerMask;

    private void OnTriggerEnter(Collider other)
    {
        if ((playerMask.value & (1 << other.gameObject.layer)) != 0)
            mainBody.OnPlayerHit();
    }
}
