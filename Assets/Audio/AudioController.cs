using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance;

    public AudioSource MusicSource;
    public AudioSource SFXSource;
    public AudioSource PlayerSource;
    public AudioSource AmbienceSource;


    public List<AudioClip> footsteps = new List<AudioClip>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void Start()
    {
        PlayerSource = PlayerController.Instance.AudioSource;
    }

    public void PlayFootstep()
    {
        if (PlayerSource == null || footsteps == null || footsteps.Count == 0)
        {
            Debug.Log($"something null boi");
            return;
        }

        int step = Random.Range(0, footsteps.Count);
        PlayerSource.clip = footsteps[step];
        PlayerSource.pitch = Random.Range(0.8f, 1.2f);
        PlayerSource.PlayOneShot(footsteps[step]);
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
