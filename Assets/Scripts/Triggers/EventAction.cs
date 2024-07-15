using UnityEngine;

public abstract class EventAction : MonoBehaviour, IAction
{
    public abstract void Execute();
}
