using UnityEngine;

public class PlayAfterSeconds : MonoBehaviour
{
    public float seconds;
    private AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
        Invoke(nameof(PlayAudioAfter), seconds);
    }

    void PlayAudioAfter()
    {
        source.Play();
    }
}
