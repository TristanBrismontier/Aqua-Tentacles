using UnityEngine;
using System.Collections;


public class Player : MonoBehaviour
{
	public static Player instance = null;
	public float speedRotation;
	public float force;
	public float pushForce;
	public float slowDown;
	public float timeVolume;

	public SpriteRenderer debugSprite;
	public AudioSource musicSource;
	private Rigidbody2D rb;

	void Awake () {
		if (instance == null){
			instance = this;
		}
		else if(instance != this)
		{
			Destroy(gameObject);
		}
		
		DontDestroyOnLoad(gameObject);
	}

	void Start()
	{
		musicSource.volume = 0;
		rb = GetComponent<Rigidbody2D>();

		debugSprite.enabled = false;
	}
	
	void FixedUpdate ()
	{
		if (Input.GetKey (KeyCode.Q)) {
			rb.angularVelocity = speedRotation;
		} else if (Input.GetKey (KeyCode.D)) {
			rb.angularVelocity = -speedRotation;
		} else {
			rb.angularVelocity = 0f;
		}

		Vector2 forceVect = transform.up * Mathf.Pow (force, pushForce);

		if (Input.GetKey (KeyCode.Z)) {
			rb.AddForce (forceVect);
		} else if (Input.GetKey (KeyCode.S)) {
			rb.AddForce (-forceVect);
		} else {
			rb.velocity = rb.velocity * slowDown;
		}
	}


	void OnTriggerExit2D(Collider2D other) {
		Debug.Log("Collide");
		if(other.gameObject.tag == "Zone1"){
			StartCoroutine (Volumeup(musicSource));
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log("Collide");
		if(other.gameObject.tag == "Zone1"){
			StartCoroutine (VolumeDown(musicSource));
		}
	}

	private IEnumerator Volumeup (AudioSource source) {
		while(source.volume <1){
			yield return new WaitForSeconds(1/100);
			source.volume = source.volume + timeVolume/100;
		}
	}

	private IEnumerator VolumeDown (AudioSource source) {
		while(source.volume > 0){
			yield return new WaitForSeconds(1/100);
			source.volume = source.volume - timeVolume/100;
		}
	}

}