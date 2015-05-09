using UnityEngine;
using System.Collections;

public class FishEye : MonoBehaviour {
	
	public float rangeDash;
	public float speed;
	public SpriteRenderer debugSprite;
	public int damage;
	public int nutritionFact;
	public int life = 3;
	public GameObject bubbleExplosion;


	public Animator animator;
	
	void Start () {
		debugSprite.enabled = false;
	}
	
	void Update () {
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		
		if (Vector3.Distance (transform.position, player.transform.position) < rangeDash) {
			animator.SetBool ("Dash", true);
			speed = 4;
			transform.LookAt (player.transform.position);
			transform.Rotate (new Vector3 (0, -90, 0), Space.Self);
			transform.Translate (new Vector3 (speed * Time.deltaTime, 0, 0));
		} else {
			animator.SetBool ("Dash", false);
		}
	}

	void OnCollisionEnter2D(Collision2D other) {
		Debug.Log("COLLLLOOOWLWLJVDS " + other.gameObject.tag);
		if (other.gameObject.tag == "Player") {
			Debug.Log("OOOOOO");
			print ("Ennemi : " + life);
			animator.SetBool ("Attack", true);
			life -= 1;
			if (life <= 0) {
				Instantiate (bubbleExplosion, transform.position, transform.rotation);
				GameManager.instance.eatFishEye(nutritionFact);
				//PlayerController.score++;
				Destroy (gameObject);
			}
			GameManager.instance.looseLife(damage);
		} else {
			animator.SetBool ("Attack", false);
		}
	}

	void OnCollisionStay2D(Collision2D other) {
		Debug.Log("COLLLLOOOWLWLJVDS " + other.gameObject.tag);
		if (other.gameObject.tag == "Player") {
			Debug.Log("OOOOOO");
			print ("Ennemi : " + life);
			animator.SetBool ("Attack", true);
			life -= 1;
			if (life <= 0) {
				Instantiate (bubbleExplosion, transform.position, transform.rotation);
				GameManager.instance.eatFishEye(nutritionFact);
				//PlayerController.score++;
				Destroy (gameObject);
			}
			GameManager.instance.looseLife(damage);
		} else {
			animator.SetBool ("Attack", false);
		}
	}

	public void OnDrawGizmos(){
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position,rangeDash);
	}
}
