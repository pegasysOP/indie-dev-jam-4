using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public AudioSource AudioSource;
    [Range(0f, 1f)]
    public float footstepTimer;

    private Coroutine footstepAudio;

    private void Update()
    {
        if (footstepAudio == null)
            footstepAudio = StartCoroutine(Footstep());
    }

    private IEnumerator Footstep()
    {
        if (Player.Movement.IsGrounded && Player.Movement.MoveDirection.magnitude > 0f)
        {
            //AudioController.Instance.PlayFootstep();
            yield return new WaitForSeconds(footstepTimer);
            footstepAudio = null;
        }
    }
}
