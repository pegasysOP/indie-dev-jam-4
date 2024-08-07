using DG.Tweening;
using UnityEngine;

public class GenericCarryable : BaseCarryable
{
    public MeshCollider meshCollider;

    public override void Carry()
    {
        Debug.Log("generic carryable carry");

        base.Carry();

        // disable gravity on object
        objectRigidbody.useGravity = false;

        // enable is kinematic so rb is not affected by collisions etc
        objectRigidbody.isKinematic = true;

        // disable actual flare collider
        meshCollider.enabled = false;
    }

    public override void Throw(Vector3 throwDirection)
    {
        Debug.Log("generic carryable throw");
        base.Throw(throwDirection);

        //transform.SetParent(null, false);
        //// This will preserve the bat’s world position
        transform.SetParent(null, true);

        // enable is kinematic
        objectRigidbody.isKinematic = false;

        // enable use gravity
        objectRigidbody.useGravity = true;

        Vector3 throwForce = throwDirection * forwardThrowForce + transform.up * upwardThrowForce;

        // apply throwing force
        objectRigidbody.AddForce(throwForce, ForceMode.Impulse);

        // enable actual flare collider
        meshCollider.enabled = true;
    }
}

