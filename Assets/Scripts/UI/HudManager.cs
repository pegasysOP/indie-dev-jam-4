using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HudManager : MonoBehaviour
{
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private GameObject crosshair;
    [SerializeField] private GameObject interactPrompt;

    [SerializeField] private CanvasGroup endscreen;

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

    public static void EnableCrosshairAndAmmoCount(bool enabled)
    {
        Instance.crosshair.SetActive(enabled);
    }

    public static void ShowEndScreen()
    {
        Instance.endscreen.DOFade(0f, 4f);
    }
}
