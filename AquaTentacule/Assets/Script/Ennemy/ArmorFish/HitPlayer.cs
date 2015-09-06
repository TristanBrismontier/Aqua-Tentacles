using UnityEngine;
using System.Collections;

public class HitPlayer : MonoBehaviour {

	
	void OnCollisionEnter2D(Collision2D other) {
		checkCollision(other);
	}
	
	void OnCollisionStay2D(Collision2D other) {
		checkCollision(other);
	}
	void checkCollision(Collision2D other){
		Debug.Log("COLLISION");
		if (other.gameObject.tag == "Player") {
			GameManager.instance.death();
		}
	}
}
