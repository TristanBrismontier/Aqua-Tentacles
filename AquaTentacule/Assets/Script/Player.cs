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
	private Animator animator;
	public float dampTime = 0.2f;
	private Vector3 velocity = Vector3.zero;

	public float scaleRatio;
	public SpriteRenderer debugSprite;
	public AudioSource musicSource;
	private Rigidbody2D rb;
	private Vector3 startScale; 

	public GameObject ten1;
	public GameObject ten2;
	public GameObject ten3;

	private float inverted = 1.0f;

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
		animator = GetComponent<Animator> ();	
		musicSource.volume = 0;
		rb = GetComponent<Rigidbody2D>();

		debugSprite.enabled = false;
		GameManager.instance.setPlayer(this);
		scaleRatio = 1;
		startScale = new Vector3(transform.localScale.x,transform.localScale.y,transform.localScale.z);
	}

	void OnCollisionEnter2d(Collision2D coll){
		if (coll.gameObject.tag == "player"){
			inverted = -1.0f;
		}
	}
	
	void FixedUpdate ()
	{
		if (Input.GetKey (KeyCode.Q)) {
			rb.angularVelocity = speedRotation * inverted;
		} else if (Input.GetKey (KeyCode.D)) {
			rb.angularVelocity = -speedRotation * inverted;
		} else {
			rb.angularVelocity = 0f;
		}
		
		Vector2 forceVect = transform.up * Mathf.Pow (force, pushForce);
		
		if (Input.GetKey (KeyCode.Z)) {
			rb.AddForce (forceVect * inverted);
			animator.SetBool ("swim", true);
		} else if (Input.GetKey (KeyCode.S)) {
			rb.AddForce (-forceVect * inverted);
			animator.SetBool ("swim", true);
		} else {
			rb.velocity = rb.velocity * slowDown;
			animator.SetBool ("swim", false);
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if(other.gameObject.tag == "Zone1"){
			StartCoroutine (Volumeup(musicSource));
		}
	}

	void OnTriggerEnter2D(Collider2D other) {

		if(other.gameObject.tag == "Zone1"){
			StartCoroutine (VolumeDown(musicSource));
		}
	}

	public void Eat(){
		animator.SetTrigger("eat");
	}

	public void setScale(float adj){
		scaleRatio = Mathf.MoveTowards(scaleRatio,adj,0.01f);
		transform.localScale = startScale * scaleRatio;
	}

	private IEnumerator Volumeup (AudioSource source) {
		while(source.volume <1){
			yield return new WaitForSeconds(1/100);
			source.volume = source.volume + 1/(timeVolume*100);
		}
	}

	private IEnumerator VolumeDown (AudioSource source) {
		while(source.volume > 0){
			yield return new WaitForSeconds(1/100);
			source.volume = source.volume - 1/(timeVolume*100);
		}
	}

}