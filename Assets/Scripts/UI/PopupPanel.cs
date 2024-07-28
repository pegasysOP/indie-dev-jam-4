using UnityEngine;

public class PopupPanel : MonoBehaviour
{
    public static PopupPanel Instance;

    public GameObject notesPopupPanel;
    public DeskNotesPopupPanel deskNotesPopupPanel;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Start()
    {
        //notesPopupPanel.SetActive(false);

        if (notesPopupPanel != null)
            deskNotesPopupPanel = notesPopupPanel.GetComponent<DeskNotesPopupPanel>();
    }

    public void ShowPopup(string text)
    {
        deskNotesPopupPanel.Init(text);
        //notesPopupPanel.SetActive(true);
    }

    public void HidePopup()
    {
        //notesPopupPanel.SetActive(false);
    }
}
