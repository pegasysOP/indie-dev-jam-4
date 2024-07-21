using TMPro;
using UnityEngine;

public class DeskNotesPopupPanel : MonoBehaviour
{
    public TextMeshProUGUI text;

    public void Init(string text)
    {
        this.text.text = text;
    }

    private void Update()
    {
        if (isActiveAndEnabled)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                PopupPanel.Instance.HidePopup();
            }
        }
    }
}
