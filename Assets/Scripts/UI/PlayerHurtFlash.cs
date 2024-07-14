using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerHurtFlash : MonoBehaviour
{
    public static PlayerHurtFlash Instance;
    public Volume volume;

    private void Start()
    {
        if (Instance == null)
            Instance = this;
    }

    public void SetAmmount(float value)
    {
        volume.profile.TryGet<Vignette>(out Vignette vig);
        if (vig == null)
            Debug.Log($"what");

        vig.intensity.value = 1f;
    }
}
