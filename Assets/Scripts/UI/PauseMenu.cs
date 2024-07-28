using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : SettingsMenu
{
    public void Toggle()
    {
        gameObject.SetActive(!isActiveAndEnabled);

        //PlayerController.Lock(isActiveAndEnabled);

        Cursor.visible = isActiveAndEnabled;
        Cursor.lockState = isActiveAndEnabled ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public override void OnResumeButtonClick()
    {
        //PlayerController.Lock(false);
        
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