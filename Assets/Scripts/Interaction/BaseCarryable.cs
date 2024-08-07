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
    public bool throwable;
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
}

