using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

	public static MusicManager instance = null;

	public float timeVolume;
	public float volumeMaxMusique;
	public AudioSource musicSource1;
	public AudioSource musicSource2;
	public AudioSource musicSource3;
	public AudioSource musicSource4;
	public AudioSource musicSource5;
	private bool activateMusique;


	void Awake () {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);
		
		DontDestroyOnLoad (gameObject);
	}

	public void resetMusique(){
		activateMusique = true;
		musicSource1.volume = volumeMaxMusique;
		musicSource2.volume = 0;
		musicSource3.volume = 0;
		musicSource4.volume = 0;
		musicSource5.volume = 0;
	}

	public void resetM(){
		activateMusique = false;
	}

	public void exitZone(int zone){
		if(!activateMusique)
			return;

		switch (zone)
		{
		case 1:
			StartCoroutine (VolumeDown(musicSource1));
			StartCoroutine (Volumeup(musicSource2));
			break;
		case 2:
			StartCoroutine (VolumeDown(musicSource2));
			StartCoroutine (Volumeup(musicSource3));
			break;
		case 3:
			StartCoroutine (VolumeDown(musicSource3));
			StartCoroutine (Volumeup(musicSource4));
			break;
		case 4:
			StartCoroutine (VolumeDown(musicSource4));
			StartCoroutine (Volumeup(musicSource5));
			break;
		}
	}
	public void enterZone(int zone){
		if(!activateMusique)
			return;
		
		switch (zone)
		{
		case 1:
			StartCoroutine (Volumeup(musicSource1));
			StartCoroutine (VolumeDown(musicSource2));
			break;
		case 2:
			StartCoroutine (Volumeup(musicSource2));
			StartCoroutine (VolumeDown(musicSource3));
			break;
		case 3:
			StartCoroutine (Volumeup(musicSource3));
			StartCoroutine (VolumeDown(musicSource4));
			break;
		case 4:
			StartCoroutine (Volumeup(musicSource4));
			StartCoroutine (VolumeDown(musicSource5));
			break;
		}
	}

	private IEnumerator Volumeup (AudioSource source) {
		while(source.volume <volumeMaxMusique){
			if(!activateMusique)
				break;
			yield return new WaitForSeconds(1/100);
			source.volume = source.volume + 1/(timeVolume*100);
		}
	}

	private IEnumerator VolumeDown (AudioSource source) {
		while(source.volume > 0){
			if(!activateMusique)
				break;
			yield return new WaitForSeconds(1/100);
			source.volume = source.volume - 1/(timeVolume*100);
		}
	}
}
