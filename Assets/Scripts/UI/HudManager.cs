using DG.Tweening;
using System;
using System.Diagnostics;
using TMPro;
using UnityEngine;

public class HudManager : MonoBehaviour
{
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private GameObject crosshair;
    [SerializeField] private GameObject interactPrompt;

    [SerializeField] private CanvasGroup endscreen;

    [Header("Speedrun Timer")]
    [SerializeField] private GameObject timerPanel;
    [SerializeField] private TextMeshProUGUI timerText;

    public static HudManager Instance;
    public static PauseMenu PauseMenu { get { return Instance.pauseMenu; } }

    private Stopwatch stopwatch;
    private bool gameOver = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        stopwatch = new Stopwatch();

        if (PlayerPrefs.GetInt(PrefDefines.SpeedrunTimerKey, 0) == 1)
        {
            stopwatch.Start();
            timerPanel.SetActive(true);
        }
    }

    private void Update()
    {
        if (gameOver)
            return;

        UpdateTimer();

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.P)) // escape fucky with editor :(
            PauseMenu.Toggle();
#else
        if (Input.GetKeyDown(KeyCode.Escape))
            PauseMenu.Toggle();
#endif
    }

    private void UpdateTimer()
    {
        if (stopwatch.IsRunning)
        {
            TimeSpan elapsedTime = stopwatch.Elapsed;

            // Format the elapsed time into a string.
            timerText.text = string.Format("{0:00}:{1:00}:{2:000}", elapsedTime.Minutes, elapsedTime.Seconds, elapsedTime.Milliseconds);
        }
    }

    public static void ShowInteractPrompt(bool enabled)
    {
        Instance.interactPrompt.SetActive(enabled);
    }

    public static void EnableCrosshairAndAmmoCount(bool enabled)
    {
        Instance.crosshair.SetActive(enabled);
    }

    public static void OnGameOver()
    {
        if (Instance.stopwatch != null)
        {
            Instance.stopwatch.Stop();
            Instance.UpdateTimer();
        }

        Instance.gameOver = true;
        Instance.endscreen.DOFade(1f, 10f);
    }
}
