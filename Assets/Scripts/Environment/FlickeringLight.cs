using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class FlickeringLight : MonoBehaviour
{
    public enum FlickerSpeed
    {
        Slow,
        Normal,
        Fast
    }

    private Light Light;

    [Header("Properties")]
    public FlickerSpeed flickerSpeed; 

    private void Awake()
    {
        Light = GetComponent<Light>();
    }

    private void Start()
    {
        StartCoroutine(Flicker());
    }

    private IEnumerator Flicker()
    {
        while (true)
        {
            float nextFlickerTime = GetNextFlickerTime();

            Light.enabled = !Light.enabled;
            yield return new WaitForSeconds(nextFlickerTime);
        }
    }

    private float GetNextFlickerTime()
    {
        switch (flickerSpeed)
        {
            case FlickerSpeed.Slow:
                return Random.Range(1f, 3f);
            case FlickerSpeed.Normal:
                return Random.Range(0.5f, 2f);
            case FlickerSpeed.Fast:
                return Random.Range(0.1f, 1f);
        }

        return Random.Range(0.1f, 3f);
    }
}
