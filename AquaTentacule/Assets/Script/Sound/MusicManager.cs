using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

	public static MusicManager instance = null;

	public float timeFade;
	public float volumeMaxMusique;
	public AudioSource musicSource1;
	public AudioSource musicSource2;
	public AudioSource musicSource3;
	public AudioSource musicSource4;
	public AudioSource musicSource5;
	private bool activateMusique;

	private IEnumerator fadeInZone1;
	private IEnumerator fadeInZone2;
	private IEnumerator fadeInZone3;
	private IEnumerator fadeInZone4;
	private IEnumerator fadeInZone5;

	private IEnumerator fadeOutZone1;
	private IEnumerator fadeOutZone2;
	private IEnumerator fadeOutZone3;
	private IEnumerator fadeOutZone4;
	private IEnumerator fadeOutZone5;

	private float privateTimeFade
	{
		get
		{
			return Mathf.Pow (10, -Mathf.Clamp(timeFade, 1, 4));
		}
	}

	void Awake () {
		if (instance == null){

			Debug.Log("INSTANTTMUSIC");
			instance = this;
		}else if (instance != this)
			Destroy (gameObject);
		
		DontDestroyOnLoad (gameObject);
	}

	void Start()
	{
		fadeInZone1 = FadeInEqualGain(musicSource1);
		fadeInZone2 = FadeInEqualGain(musicSource2);
		fadeInZone3 = FadeInEqualGain(musicSource3);
		fadeInZone4 = FadeInEqualGain(musicSource4);
		fadeInZone5 = FadeInEqualGain(musicSource5);

		fadeOutZone1 = FadeOutEqualGain(musicSource1);
		fadeOutZone2 = FadeOutEqualGain(musicSource2);
		fadeOutZone3 = FadeOutEqualGain(musicSource3);
		fadeOutZone4 = FadeOutEqualGain(musicSource4);
		fadeOutZone5 = FadeOutEqualGain(musicSource5);

	}

	public void resetMusique(){
		activateMusique = true;
		musicSource1.volume = 0;
		musicSource2.volume = 0;
		musicSource3.volume = 0;
		musicSource4.volume = 0;
		musicSource5.volume = 0;
	}

	public void resetM(){
		activateMusique = false;
	}

	public void exitZone(int zone)
	{

		switch (zone)
		{
		case 1:
			StopCoroutine(fadeInZone1);
			fadeInZone1 = FadeInEqualGain(musicSource1);
			StartCoroutine (fadeOutZone1);
			break;
		case 2:
			StopCoroutine(fadeInZone2);
			fadeInZone2 = FadeInEqualGain(musicSource2);
			StartCoroutine (fadeOutZone2);
			break;
		case 3:
			StopCoroutine(fadeInZone3);
			fadeInZone3 = FadeInEqualGain(musicSource3);
			StartCoroutine (fadeOutZone3);
			break;
		case 4:
			StopCoroutine(fadeInZone4);
			fadeInZone4 = FadeInEqualGain(musicSource4);
			StartCoroutine (fadeOutZone4);
			break;
		}
	}
	public void enterZone(int zone)
	{
		if(zone == 0){
			StopCoroutine(fadeOutZone1);
			fadeOutZone1 = FadeOutEqualGain(musicSource1);
			StartCoroutine (fadeInZone1);
			musicSource2.volume = 0;
			musicSource3.volume = 0;
			musicSource4.volume = 0;
			musicSource5.volume = 0;
		}

		switch (zone)
		{
		case 1:
			StopCoroutine(fadeOutZone1);
			fadeOutZone1 = FadeOutEqualGain(musicSource1);
			StartCoroutine (fadeInZone1);
			break;
		case 2:
			StopCoroutine(fadeOutZone2);
			fadeOutZone2 = FadeOutEqualGain(musicSource2);
			StartCoroutine (fadeInZone2);
			break;
		case 3:
			StopCoroutine(fadeOutZone3);
			fadeOutZone3 = FadeOutEqualGain(musicSource3);
			StartCoroutine (fadeInZone3);
			break;
		case 4:
			StopCoroutine(fadeOutZone4);
			fadeOutZone4 = FadeOutEqualGain(musicSource4);
			StartCoroutine (fadeInZone4);
			break;
		}
	}


	IEnumerator FadeInEqualPower(AudioSource audioSource)
	{
		float startVolume = audioSource.volume;
		for(float i=0; i<=1f; i=i+privateTimeFade)
		{
			yield return 0;
			audioSource.volume = startVolume+(1-Mathf.Pow((i-1),2f));
		}
	}


	IEnumerator FadeOutEqualPower(AudioSource audioSource)
	{
		float startVolume=audioSource.volume;
		for(float i=0; i<=1f; i=i+privateTimeFade)
		{
			yield return 0;
			audioSource.volume = startVolume-Mathf.Pow(i, 2f);
		}
	}

	IEnumerator FadeInEqualGain(AudioSource audioSource)
	{
		float startVolume = audioSource.volume;
		for(float i=0; i<=1f; i=i+privateTimeFade)
		{
			yield return 0;
			audioSource.volume = startVolume+i;
		}
	}

	IEnumerator FadeOutEqualGain(AudioSource audioSource)
	{
		float startVolume=audioSource.volume;
		for(float i=0; i<=1f; i=i+privateTimeFade)
		{
			yield return 0;
			audioSource.volume = startVolume-i;
		}
	}
}
