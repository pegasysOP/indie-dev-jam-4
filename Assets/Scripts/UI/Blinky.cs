using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Blinky : MonoBehaviour
{
    public static Blinky Instance;

    public RectTransform top;
    public RectTransform bottom;

    public float blinkTime;

    private Coroutine blink;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void Start()
    {
        Invoke("OpenEyes", 2f);
    }

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.B))
        {
            if (blink == null)
                blink = StartCoroutine(Blink());
        }
    }

    public IEnumerator Blink()
    {
        CloseEyes();
        yield return new WaitForSeconds(blinkTime);
        OpenEyes();
        yield return new WaitForSeconds(blinkTime);
        blink = null;
    }

    private void OpenEyes()
    {
        top.DOSizeDelta(new Vector2(1920f, 0f), blinkTime * 0.5f);
        bottom.DOSizeDelta(new Vector2(1920f, 0f), blinkTime * 0.5f);
    }

    private void CloseEyes()
    {
        top.DOSizeDelta(new Vector2(1920f, 600f), blinkTime * 0.5f);
        bottom.DOSizeDelta(new Vector2(1920f, 600f), blinkTime * 0.5f);
    }
}
