using UnityEngine;
using System.Collections;


public class Player : MonoBehaviour
{
	public static Player instance = null;
	public float speedRotation;
	public float force;
	public float pow;
	public float slow;
	private Rigidbody2D rb;

	public AudioSource musicSource;

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

		Vector2 forceVect = transform.up * Mathf.Pow (force, pow);

		if (Input.GetKey (KeyCode.Z)) {
			rb.AddForce (forceVect);
		} else if (Input.GetKey (KeyCode.S)) {
			rb.AddForce (-forceVect);
		} else {
			rb.velocity = rb.velocity * slow;
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if(other.gameObject.tag == "Zone1"){
			musicSource.volume = 1;
		}
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.tag == "Zone1"){
			musicSource.volume = 0;
		}
	}

}