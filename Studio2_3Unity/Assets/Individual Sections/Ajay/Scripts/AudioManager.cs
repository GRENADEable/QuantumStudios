using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public AudioSource fxSource;
	public AudioSource musicSource;
	public static AudioManager instance = null;
	public float lowestPitchRange = 0.90f;
	public float highestPitchRange = 1.05f;

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			Destroy (gameObject);
		}
		DontDestroyOnLoad (gameObject);
	}

	public void PlaySingle (AudioClip clip)
	{
		fxSource.clip = clip;
		fxSource.Play();
	}
	public void RandomizeFX(params AudioClip[] clips)
	{	
		int randomIndex = Random.Range(0, clips.Length);

		float randomPitch = Random.Range(lowestPitchRange, highestPitchRange);
		fxSource.pitch = randomPitch;
		fxSource.clip = clips[randomIndex];
		fxSource.Play();
	}
	void Start () 
	{
		
	}
	
	
	void Update () 
	{
		
	}
}
