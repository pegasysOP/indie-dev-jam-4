using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceListener : MonoBehaviour
{
    public static List<Collider> PlayerColliders = new List<Collider>();
    void Start() => gameObject.GetComponentsInChildren(true, PlayerColliders);
    public void AddCollider(Collider collider)
    {
        if (collider && !PlayerColliders.Contains(collider))
            PlayerColliders.Add(collider);
    }
    public void RemoveCollider(Collider collider) => PlayerColliders.Remove(collider);
}
