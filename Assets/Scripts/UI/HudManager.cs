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

    private bool gameOver = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        if (gameOver)
            return;

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.P)) // escape fucky with editor :(
            PauseMenu.Toggle();
#else
        if (Input.GetKeyDown(KeyCode.Escape))
            PauseMenu.Toggle();
#endif
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
        Instance.gameOver = true;
        Instance.endscreen.DOFade(1f, 10f);
    }
}
