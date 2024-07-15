using System.Collections.Generic;
using UnityEngine;

public class TriggerActionObject : MonoBehaviour
{
    public List<EventAction> actions = new List<EventAction>();

    public void Trigger()
    {
        foreach (EventAction action in actions)
        {
            action.Execute();
        }
    }
}
