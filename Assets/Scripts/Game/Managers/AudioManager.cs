using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public List<AudioClip> musicList;
    private AudioSource audioSource;
    public bool isPlayingMusic = false;
    public static AudioManager instance;
    public bool saved;
    public bool actual;


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
    }

    private void Update()
    {
        actual = DayNightCycle.instance.isNight;
        if (DayNightCycle.instance.isNight && !isPlayingMusic)
        {
            PlayMusic(musicList[1]);
        }
        else if (!DayNightCycle.instance.isNight && !isPlayingMusic)
        {
            PlayMusic(musicList[0]);
        }
        Verifier(actual);


    }

    private void PlayMusic(AudioClip musicClip)
    {
        audioSource.clip = musicClip;
        audioSource.Play();

        isPlayingMusic = true;
    }
    private void Verifier(bool actual)
    {
        if (saved != actual)
        {

            isPlayingMusic = false;
            saved = actual;
        }
    }

}