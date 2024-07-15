using UnityEngine;
using UnityEngine.UI;

public class PlayerHurtFlash : MonoBehaviour
{
    public static PlayerHurtFlash Instance;

    public Sprite bloodOne;
    public Sprite bloodTwo;

    private Image image;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        image = GetComponent<Image>();
    }

    public void ShowBloodFX(int health)
    {
        if (health == 1)
        {
            image.sprite = bloodTwo;
            image.color = Color.white;
        }
        else if (health == 2)
        {
            image.sprite = bloodOne;
            image.color = Color.white;
        }
        else
        {
            image.color = new Color(1f,1f, 1f, 0f);
        }
    }
}
