using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : EventAction
{
    public Animator animator;
    public Camera cam;

    public override void Execute()
    {
        PlayerController.Instance.gameObject.SetActive(false);
        cam.gameObject.SetActive(true);
        animator.SetTrigger("TakeOff");

        HudManager.ShowEndScreen();
    }
}
