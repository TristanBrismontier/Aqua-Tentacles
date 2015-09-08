using UnityEngine;
using System.Collections;

public class FishEye : MonoBehaviour {
	
	public float rangeDash;
	public float speed;
	public SpriteRenderer debugSprite;
	public int damage;
	public int nutritionFact;
	public GameObject bubbleExplosion;
	private Rigidbody2D rb;

	public AudioClip[] deaths;
	public Animator animator;
	
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		debugSprite.enabled = false;
	}
	
	void Update () {
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		
		if (Vector3.Distance (transform.position, player.transform.position) < rangeDash) {
			int eatable = (GameManager.instance.playerInfo.playerHasTenta)?-1:1;
			animator.SetBool ("Dash", true);
			speed = 4;
			transform.LookAt (player.transform.position);
			transform.Rotate (new Vector3 (0, -90*eatable, 0), Space.Self);
			transform.Translate (new Vector3 (speed * Time.deltaTime, 0, 0));
		} else {
			rb.velocity = Vector2.zero;
			rb.angularVelocity = 0.0f;
			animator.SetBool ("Dash", false);
		}
	}

	void OnCollisionEnter2D(Collision2D other) {
		checkCollision(other);
	}
	
	void OnCollisionStay2D(Collision2D other) {
		checkCollision(other);
	}

	void checkCollision(Collision2D other){
		if (other.gameObject.tag == "Player") {
			animator.SetBool ("Attack", true);
			if (GameManager.instance.playerInfo.playerHasTenta) {
				Instantiate (bubbleExplosion, transform.position, transform.rotation);
				GameManager.instance.eatFishEye(nutritionFact);
				SoundManager.instance.RandomizeSfx(SoundManager.instance.efxSource ,deaths);
				//PlayerController.score++;
				Destroy (gameObject);
			}else{
				GameManager.instance.looseLife(damage);
			}
		} else {
			animator.SetBool ("Attack", false);
		}
	}

	public void OnDrawGizmos(){
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position,rangeDash);
	}
}
