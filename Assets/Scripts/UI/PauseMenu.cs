using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : SettingsMenu
{
    public void Toggle()
    {
        bool pausing = !isActiveAndEnabled;

        gameObject.SetActive(pausing);

        Cursor.visible = pausing;
        Cursor.lockState = isActiveAndEnabled ? CursorLockMode.None : CursorLockMode.Locked;

        GameManager.Pause(pausing);
    }

    public override void OnResumeButtonClick()
    {
        Toggle();
    }

    public void OnQuitButtonClick()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SceneManager.LoadScene("Main Menu");
    }

    protected override void OnSensitivityValueChanged(float newValue)
    {
        Player.SetSensitivity(newValue);

        base.OnSensitivityValueChanged(newValue);
    }

    protected override void OnVolumeValueChanged(float newValue)
    {
        AudioController.Instance.SetVolume(newValue);

        base.OnVolumeValueChanged(newValue);
    }
}