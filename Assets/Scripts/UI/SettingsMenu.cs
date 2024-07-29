using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Slider sensitivitySlider;
    public Slider volumeSlider;
    public Toggle timerToggle;

    private void Awake()
    {
        // Sensitivity
        sensitivitySlider.value = PlayerPrefs.GetFloat(PrefDefines.SensitivityKey, 1f);
        PlayerPrefs.SetFloat(PrefDefines.SensitivityKey, sensitivitySlider.value);
        sensitivitySlider.onValueChanged.AddListener(OnSensitivityValueChanged);

        // Volume
        volumeSlider.value = PlayerPrefs.GetFloat(PrefDefines.MasterVolumeKey, 0.5f);
        PlayerPrefs.SetFloat(PrefDefines.MasterVolumeKey, volumeSlider.value);
        volumeSlider.onValueChanged.AddListener(OnVolumeValueChanged);

        // Timer
        timerToggle.isOn = PlayerPrefs.GetInt(PrefDefines.SpeedrunTimerKey, 0) == 1;
        PlayerPrefs.SetInt(PrefDefines.SpeedrunTimerKey, timerToggle.isOn ? 1 : 0);
        timerToggle.onValueChanged.AddListener(OnTimerToggleClick);

        PlayerPrefs.Save();
    }

    public virtual void OnResumeButtonClick()
    {
        gameObject.SetActive(false);
    }

    protected virtual void OnSensitivityValueChanged(float newValue)
    {
        SaveNewSensitivity(newValue);
    }

    protected void SaveNewSensitivity(float newValue)
    {
        PlayerPrefs.SetFloat(PrefDefines.SensitivityKey, newValue);
        PlayerPrefs.Save();
    }

    protected virtual void OnVolumeValueChanged(float newValue)
    {
        SaveNewVolume(newValue);

        // set the menu volume stuff here
    }

    protected void SaveNewVolume(float newValue)
    {
        PlayerPrefs.SetFloat(PrefDefines.MasterVolumeKey, newValue);
        PlayerPrefs.Save();
    }

    protected virtual void OnTimerToggleClick(bool isOn)
    {
        SaveNewTimer(isOn);
    }

    protected void SaveNewTimer(bool isOn)
    {
        PlayerPrefs.SetInt(PrefDefines.SpeedrunTimerKey, isOn ? 1 : 0);
        PlayerPrefs.Save();
    }
}
