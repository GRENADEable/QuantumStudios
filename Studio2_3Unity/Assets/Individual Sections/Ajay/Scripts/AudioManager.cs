﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioSource fxSource;
    public AudioSource mainMenuFx;
    public AudioSource inGameFx;
    public AudioClip[] soundClips;
    public static AudioManager instance = null;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    public void StopAudio()
    {
        fxSource.Stop();
        mainMenuFx.Stop();
        inGameFx.Stop();
    }
    public void AudioAccess(int index)
    {
        fxSource.PlayOneShot(soundClips[index]);
    }

    public void SplashScreenAudio()
    {
        AudioAccess(3);
        AudioAccess(6);
    }

    public void PlayGameMusicForOnline()
    {
        AudioAccess(2);
    }

    public void PlaySinglePlayerAudio()
    {
        inGameFx.Play();
        inGameFx.playOnAwake = true;
        inGameFx.loop = true;
    }

    public void MainMenuMusic()
    {
        mainMenuFx.Play();
        mainMenuFx.playOnAwake = true;
        mainMenuFx.loop = true;
    }

    public void DeathAudio()
    {
        AudioAccess(7);
        AudioAccess(1);
        AudioAccess(3);
        AudioAccess(6);
    }
}
