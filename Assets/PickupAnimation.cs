using DG.Tweening;
using System.Collections;
using UnityEngine;

public class PickupAnimation : MonoBehaviour
{
    [Header("Rotation")]
    [Range(0f, 100f)]
    public float rotateSpeed;
    public Vector3 rotateAxis;

    [Space(5)]
    [Header("Bobbing Motion")]
    public float bobHeight;
    public float bobTime;

    public Transform objectToRotate;
    private Vector3 startPos;

    private void Start()
    {
        startPos = objectToRotate.position;

        StartCoroutine(Bob());
    }

    private void Update()
    {
        objectToRotate.Rotate(rotateAxis, rotateSpeed * Time.deltaTime);
        
    }

    private IEnumerator Bob()
    {
        while(true)
        {
            objectToRotate.DOMoveY(startPos.y + bobHeight, bobTime);
            yield return new WaitForSeconds(bobTime);
            objectToRotate.DOMoveY(startPos.y, bobTime);
            yield return new WaitForSeconds(bobTime);
        }
    }
}
