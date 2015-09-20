using UnityEngine;
using System.Collections;

public class Weak : MonoBehaviour {
	public GameObject parent;
	public GameObject bubbleExplosion;
	public AudioClip[] deaths;
	public int nutritionFact;

	
	void OnCollisionEnter2D(Collision2D other) {
		checkCollision(other);
	}
	
	void OnCollisionStay2D(Collision2D other) {
		checkCollision(other);
	}

	void checkCollision(Collision2D other){
		if (other.gameObject.tag == "Player") {
			Instantiate (bubbleExplosion, transform.position, transform.rotation);
			GameManager.instance.eatArmorFish(nutritionFact);
			SoundManager.instance.RandomizeSfx(SoundManager.instance.efxSource ,deaths);

			Destroy (parent);
		}
	}
}
