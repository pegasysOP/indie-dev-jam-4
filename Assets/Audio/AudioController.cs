using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance;

    public AudioSource MusicSource;
    public AudioSource SFXSource;
    public AudioSource PlayerSource;
    public AudioSource AmbienceSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void PlayMusic(AudioClip music)
    {
        if (MusicSource == null)
            return;

        if (MusicSource.isPlaying) {
            //transition between music tracks
            // not sure how to do this
            return;
        }

        MusicSource.clip = music;
        MusicSource.Play();
    }

    public void StopMusic()
    {
        if (MusicSource == null)
            return;

        if (MusicSource.isPlaying)
        {
            MusicSource.Stop();
        }
    }
}
