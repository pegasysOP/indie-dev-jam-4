using UnityEngine;

public enum CarryableState
{
    None,
    BeingCarried,
    Thrown
}

//public enum ThrowForm
//{
//    Regular,
//    Parabola
//}

public class BaseCarryable : MonoBehaviour
{
    public float forwardThrowForce;
    public float upwardThrowForce;

    public Rigidbody objectRigidbody;
    /// <summary>
    /// Bool to determine if carryable has an activation effect (behaviour implemented in classes that inherit)
    /// </summary>
    public bool hasActivateEffect;
    /// <summary>
    /// Bool to determine if carryable has been activated
    /// </summary>
    protected bool isInActiavtedState;

    //public bool throwable;
    protected CarryableState state;
    protected Vector3 throwDirection;

    virtual public void Carry()
    {
        Debug.Log("base carry");

        state = CarryableState.BeingCarried;
    }

    virtual public void Throw(Vector3 throwDirection)
    {
        Debug.Log("base throw");
        this.throwDirection = throwDirection;
        state = CarryableState.Thrown;
    }

    virtual public void Activate()
    {
        isInActiavtedState = true;
    }
}

