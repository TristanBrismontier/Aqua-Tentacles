using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {
	
	
	public AudioSource efxSource;
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
	
	public void PlaySingle (AudioClip clip){
		float randomPitch = Random.Range (lowPitchRange, hightPitchRange);
		efxSource.pitch = randomPitch; 
		efxSource.clip = clip;
		efxSource.Play ();
	}
	
	
	public void RandomizeSfx( params AudioClip [] clips){
		int randomIndex = Random.Range (0, clips.Length);
		float randomPitch = Random.Range (lowPitchRange, hightPitchRange);
		
		efxSource.pitch = randomPitch; 
		efxSource.clip = clips [randomIndex];
		efxSource.Play ();
		
		
	}
}
