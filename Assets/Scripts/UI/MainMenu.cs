using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject settingsMenu;

    public void OnStartClicked()
    {
        SceneManager.LoadScene("Main");
    }

    public void OnSettingsClicked()
    {
        settingsMenu.SetActive(!settingsMenu.activeSelf);
    }

    public void OnExitClicked()
    {
        Application.Quit();
    }

}
