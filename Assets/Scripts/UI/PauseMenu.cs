using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Button resumeButton;
    public Button quitButton;

    public Slider volumeSlider;
    public Slider sensitivitySlider;

    public static PauseMenu Instance;

    public static void Toggle()
    {
        Instance.gameObject.SetActive(!Instance.isActiveAndEnabled);
        PlayerController.Lock(Instance.isActiveAndEnabled);
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        Instance.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        resumeButton.onClick.AddListener(OnResumeButtonClick);
        quitButton.onClick.AddListener(OnQuitButtonClick);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void OnDisable()
    {
        resumeButton.onClick.RemoveListener(OnResumeButtonClick);
        quitButton.onClick.RemoveListener(OnQuitButtonClick);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnResumeButtonClick()
    {
        PlayerController.Lock(false);

        gameObject.SetActive(false);
    }

    private void OnQuitButtonClick()
    {
        Application.Quit();
    }
}