using UnityEngine;

public class SpinningBeaconLight : MonoBehaviour
{
    public Transform beacon;

    [Space(5)]
    [Header("Properties")]
    [Range(10f, 50f)]
    public float speed;
    private float multiplier = 10f;

    private void Update()
    {
        beacon.Rotate(Vector3.forward, speed * Time.deltaTime * multiplier);
    }
}
