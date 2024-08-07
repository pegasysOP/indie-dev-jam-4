using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class Flare : BaseCarryable
{
    public List<ParticleSystem> particleSystems;
    public Light flareLight;
    public MeshCollider meshCollider;

    public override void Carry()
    {
        Debug.Log("Flare carry");

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
        Debug.Log("Flare throw");
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

        foreach (ParticleSystem p in particleSystems)
        {
            p.Play();
        }

        flareLight.DOIntensity(1.5f, 1f);
    }

    public override void Activate()
    {
        Debug.Log("base has activate effect = " + hasActivateEffect + " and current activate state = " + isInActiavtedState);
        if (hasActivateEffect && !isInActiavtedState)
        {
            foreach (ParticleSystem p in particleSystems)
            {
                p.Play();
            }

            flareLight.DOIntensity(1.5f, 1f);

            base.Activate();
        }
    }
}

