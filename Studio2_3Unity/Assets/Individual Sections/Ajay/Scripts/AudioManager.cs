using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioSource fxSource;
    public AudioSource oceanFx;
    public AudioSource seagulFx;
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

    /*public void PlaySingle(AudioClip clip)
    {
        fxSource.clip = clip;
        fxSource.Play();
    }


    public void MainMenuMusic(AudioClip clip)
    {
        fxSource.clip = clip;
        fxSource.Play();
    }*/
    public void StopAudio()
    {
        fxSource.Stop();
    }
    public void AudioAccess(int index)
    {
        fxSource.PlayOneShot(soundClips[index]);
    }

    public void StopSplashScreenAudio()
    {
        seagulFx.Stop();
        oceanFx.Stop();
    }
}
