using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance;

    public AudioSource MusicSource;
    public AudioSource SFXSource;
    public AudioSource PlayerSource;
    public AudioSource AmbienceSource;


    public List<AudioClip> footsteps = new List<AudioClip>();

    [Space(5)]
    [Header("Metal Creeking")]
    [Range(0f, 10f)]
    public float creakMinTimer;
    [Range(10f, 30f)]
    public float creakMaxTimer;
    public List<AudioClip> metalCreaks = new List<AudioClip>();
    private int lastPlayedCreak = 0;

    private Coroutine creekCoroutine;


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
        
        PlayShipCreek();
    }

    public void PlayFootstep()
    {
        if (PlayerSource == null || footsteps == null || footsteps.Count == 0)
        {
            // Debug.Log($"something null boi"); get out my console boi
            return;
        }

        int step = Random.Range(0, footsteps.Count);
        PlayerSource.clip = footsteps[step];
        PlayerSource.pitch = Random.Range(0.8f, 1.2f);
        PlayerSource.PlayOneShot(footsteps[step]);
    }

    private IEnumerator CreakCoroutine()
    {
        while (true)
        {
            float timer = Random.Range(creakMinTimer, creakMaxTimer);

            if (AmbienceSource == null || metalCreaks == null || metalCreaks.Count == 0)
                yield return null;

            int creek = 0;
            while (creek != lastPlayedCreak)
            {
                creek = Random.Range(0, metalCreaks.Count);
            }

            AmbienceSource.pitch = Random.Range(0.8f, 1.2f);
            AmbienceSource.PlayOneShot(metalCreaks[creek]);
            lastPlayedCreak = creek;

            yield return new WaitForSeconds(timer);
        }
    }

    public void PlayShipCreek()
    {
        if (PlayerController.GetLock())
        {
            StopCoroutine(CreakCoroutine());
            creekCoroutine = null;
            return;
        }

        creekCoroutine = StartCoroutine(CreakCoroutine());
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
