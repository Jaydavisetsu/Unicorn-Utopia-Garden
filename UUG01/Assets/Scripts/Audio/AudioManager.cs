using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json.Bson;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance; //Creating a static instance of this code to easily access if from anywhere.

    //Two different arrays from the sound class.
    public Sound[] MusicSounds, SfxSounds;
    public AudioSource MusicSource, SfxSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);

            // Reparenting the GameObject AudioManager because DontDestroyOnLoad only works on root GameObjects and not child game objects.
            Instance.transform.SetParent(null); // Makeing it a root GameObject.
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

    public void PlayMusic(string name) //Playing the music.
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

    //-----------------------SFX THINGS-------------------------------------------------------

    public void PlaySFX(string name) //Playing the sound effect.
    {
        Sound s = Array.Find(SfxSounds, x => x.Name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found: " + name);
        }
        else
        {
            SfxSource.PlayOneShot(s.Clip);
            SfxSource.Play();
        }
    }

    public void StopSFX() //Stoppingg the sound effect.
    {
        SfxSource.Stop();
    }

    public void FadeInSFX(string name, float duration, float targetVolume)
    {
        Sound s = System.Array.Find(SfxSounds, x => x.Name == name);
        if (s != null)
        {
            StartCoroutine(FadeIn(SfxSource, s.Clip, duration, targetVolume, true)); // Enable looping.
        }
        else
        {
            Debug.LogWarning("Sound not found: " + name);
        }
    }

    public void FadeOutSFX(float duration)
    {
        StartCoroutine(FadeOut(SfxSource, duration));
    }

    private IEnumerator FadeIn(AudioSource audioSource, AudioClip clip, float duration, float targetVolume, bool loop)
    {
        audioSource.clip = clip;
        audioSource.loop = true; // Set looping.
        audioSource.Play();
        audioSource.volume = 0f;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(0f, targetVolume, t / duration);
            yield return null;
        }

        audioSource.volume = targetVolume;
    }

    private IEnumerator FadeOut(AudioSource audioSource, float duration)
    {
        float startVolume = audioSource.volume;
        float targetVolume = 0f;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, t / duration);
            yield return null;
        }

        audioSource.volume = targetVolume;
        audioSource.Stop();
        audioSource.loop = false; // Reset looping to false when stopped.
    }

    //-----------------------UI THINGS-------------------------------------------------------

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
        SfxSource.volume = volume;
    }
}

//Source: https://www.youtube.com/watch?v=rdX7nhH6jdM