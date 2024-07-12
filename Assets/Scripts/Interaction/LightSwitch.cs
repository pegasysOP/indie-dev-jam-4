using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour, IInteractable
{
    public List<Light> lightsToSwitch;

    public void Interact()
    {
        if (lightsToSwitch == null || lightsToSwitch.Count == 0)
            return;

        foreach (Light loopLight in lightsToSwitch)
        {
            if (loopLight.enabled)
            {
                loopLight.enabled = false;
            }
            else
            {
                loopLight.enabled = true;
            }
        }
    }
}
