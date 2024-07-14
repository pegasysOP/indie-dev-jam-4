using UnityEngine;

public class RotateSeagull : MonoBehaviour
{
    public float rotateSpeed;

    public void Update()
    {
        if (rotateSpeed > 0)
        {
            transform.Rotate(rotateSpeed * Time.deltaTime * Vector3.up);
        }
    }
}
