using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HudManager : MonoBehaviour
{
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private GameObject crosshair;
    [SerializeField] private GameObject interactPrompt;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI ammoText;

    public static HudManager Instance;
    public static PauseMenu PauseMenu { get { return Instance.pauseMenu; } }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            PauseMenu.Toggle();
    }

    public static void ShowInteractPrompt(bool enabled)
    {
        Instance.interactPrompt.SetActive(enabled);
    }

    public static void SetHealthText(int value, int max)
    {
        Instance.healthText.text = $"Health: {value}/{max}";
    }

    public static void SetAmmoText(int value, int max)
    {
        Instance.ammoText.text = $"Ammo: {value}/{max}";
    }

    public static void EnableCrosshairAndAmmoCount(bool enabled)
    {
        Instance.crosshair.SetActive(enabled);
        Instance.ammoText.gameObject.SetActive(enabled);
    }
}
