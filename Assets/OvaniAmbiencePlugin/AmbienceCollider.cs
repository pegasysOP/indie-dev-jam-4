using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceCollider : MonoBehaviour
{
    private Collider _collider;
    [HideInInspector] public AmbienceArea Area;
    // Start is called before the first frame update
    void Start()
    {
        if (!_collider)
        {
            _collider = GetComponent<Collider>();
            if (!_collider)
                _collider = gameObject.AddComponent<BoxCollider>();
        }
        _collider.isTrigger = true;
        if (!Area)
            Area = transform.parent.GetComponent<AmbienceArea>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (Area && AmbienceListener.PlayerColliders.Contains(other))
        {
            PlayerInside = true;
            Area.OnPlayerColEntry();
        }
    }
    [HideInInspector] public bool PlayerInside;
    private void OnTriggerExit(Collider other)
    {
        if (Area && AmbienceListener.PlayerColliders.Contains(other))
        {
            PlayerInside = false;
            Area.OnPlayerColExit();
        }
    }
}
