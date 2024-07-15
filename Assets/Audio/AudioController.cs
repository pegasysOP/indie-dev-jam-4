using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance;

    public AudioMixer mainMix;

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


    [Space(5)]
    [Header("Player Hurt")]
    public List<AudioClip> playerHurtSounds = new List<AudioClip>();
    private int lastPlayedHurt = 0;

    [Space(5)]
    [Header("Gun sounds")]
    public AudioClip pistolReload;

    [Space(5)]
    [Header("Zombie Hurt")]
    public List<AudioClip> zombieHurtSounds = new List<AudioClip>();
    private int lastPlayedZombie = 0;
    
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

    public void SetVolume(float volume)
    {
        float masterVolume = Map(volume, 0f, 1f, -40f, 0f);
        mainMix.SetFloat("MasterVolume", masterVolume);
    }

    public void PlayDoorOpen(AudioClip audioClip)
    {
        if (audioClip == null)
            return;

        //SFXSource.PlayOneShot(audioClip);
    }

    private float Map(float x, float in_min, float in_max, float out_min, float out_max)
    {
        return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }

    public void PlayGunshot(GunType ammoType)
    {
        switch (ammoType) 
        {
            case GunType.Pistol:
                PlayPistolShot();
                break;
            default: break;
        }
    }

    private void PlayPistolShot()
    {
        if (SFXSource == null)
            return;

        SFXSource.pitch = 0.8f;
        SFXSource.volume = 1f;
        SFXSource.Play();
    }

    public void PlayReload(GunType gunType)
    {
        switch (gunType)
        {
            case GunType.Pistol:
                PlayPistolReload();
                break;
            default: break;
        }
    }

    private void PlayPistolReload()
    {
        if (SFXSource == null)
            return;

        SFXSource.pitch = 1f;
        SFXSource.volume = 0.5f;
        SFXSource.PlayOneShot(pistolReload);
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

    public void PlayHurt()
    {
        if (PlayerSource == null || playerHurtSounds == null || playerHurtSounds.Count == 0)
            return;

        int hurt = 0;
        while (hurt != lastPlayedHurt)
        {
            hurt = Random.Range(0, playerHurtSounds.Count);
        }

        lastPlayedHurt = hurt;
        PlayerSource.pitch = Random.Range(0.9f, 1.1f);
        PlayerSource.PlayOneShot(playerHurtSounds[hurt]);
    }

    public void PlayZombieHurt()
    {
        if (PlayerSource == null || zombieHurtSounds == null || zombieHurtSounds.Count == 0)
            return;

        int hurt = Random.Range(0, zombieHurtSounds.Count);
        while (hurt == lastPlayedZombie)
        {
            hurt = Random.Range(0, zombieHurtSounds.Count);
        }

        lastPlayedZombie = hurt;
        PlayerSource.pitch = Random.Range(0.9f, 1.1f);
        PlayerSource.PlayOneShot(zombieHurtSounds[hurt]);
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
