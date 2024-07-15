using UnityEngine;

public abstract class EventTrigger : MonoBehaviour, ITrigger
{
    public EventAction action;

    public virtual void Trigger()
    {
        action.Execute();
    }
}
