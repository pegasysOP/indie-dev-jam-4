public class PickupPistolAction : EventAction
{
    public override void Execute()
    {
        if (TryGetComponent(out SimpleDoor door))
            door.Interact();
    }
}
