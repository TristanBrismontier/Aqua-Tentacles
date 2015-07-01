using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {
	
	
	public AudioSource efxSource;
	public AudioSource ambianceSource;
	public AudioSource interfaceSource;

	public static SoundManager instance = null;
	
	public float lowPitchRange = .85f;
	public float hightPitchRange = 1.15f;
	
	
	// Use this for initialization
	void Awake () {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);
		
		DontDestroyOnLoad (gameObject);
	}
	
	public void PlaySingle (AudioClip clip, AudioSource source, float volume){
		float randomPitch = Random.Range (lowPitchRange, hightPitchRange);
		source.pitch = randomPitch; 
		source.clip = clip;
		source.volume = volume;
		source.Play ();
	}

	public void PlaySingleLoop (AudioClip clip, AudioSource source, float volume){
		float randomPitch = Random.Range (lowPitchRange, hightPitchRange);
		source.pitch = randomPitch; 
		source.clip = clip;
		source.volume = volume;
		source.loop = true;
		source.Play ();
	}
	
	
	public void RandomizeSfx( AudioSource source, params AudioClip [] clips){
		int randomIndex = Random.Range (0, clips.Length);
		float randomPitch = Random.Range (lowPitchRange, hightPitchRange);
		
		source.pitch = randomPitch; 
		source.clip = clips [randomIndex];
		source.Play ();
		
	}

	public IEnumerator FadeOut(AudioSource audioSource, float volumeMax)
	{
		for(float i=0; i<=volumeMax; i=i+0.01f)
		{	
			yield return 0;
			audioSource.volume = 1-Mathf.Pow(i, 2f);

		}
		audioSource.Stop();
	}
	
	public IEnumerator FadeIn(AudioSource audioSource, float volumeMax)
	{
		for(float i=0; i<=volumeMax; i=i+0.01f)
		{
			yield return 0;
			audioSource.volume = 1-Mathf.Pow((i-1),2f);
		}
	}

}
