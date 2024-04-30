using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json.Bson;

public class AudioManager : MonoBehaviour
{
    //Creating a static instance of this code to easily access if from anywhere.
    public static AudioManager Instance;

    //Two different arrays from the sound class.
    public Sound[] MusicSounds, SfxSounds;
    public AudioSource MusicSource, SfxSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic("Theme");
    }

    //Playing the music.
    public void PlayMusic(string name)
    {
        Sound s = Array.Find(MusicSounds, x => x.Name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            MusicSource.clip = s.Clip;
            MusicSource.Play();
        }
    }

    //Playing the sound effects.
    public void PlaySFX(string name)
    {
        Sound s = Array.Find(SfxSounds, x => x.Name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            SfxSource.PlayOneShot(s.Clip);
            SfxSource.Play();
        }
    }

    public void ToggleMusic()
    {
        MusicSource.mute = !MusicSource.mute;
    }

    public void ToggleSFX()
    {
        SfxSource.mute = !SfxSource.mute;
    }

    public void MusicVolume(float volume)
    {
        MusicSource.volume = volume;
    }

    public void SFXVolume(float volume)
    {
        MusicSource.volume = volume;
    }
}

//Source: https://www.youtube.com/watch?v=rdX7nhH6jdM