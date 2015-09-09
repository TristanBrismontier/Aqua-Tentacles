using UnityEngine;
using System.Collections;

public class HitPlayer : MonoBehaviour {
	public int damage;
	
	void OnCollisionEnter2D(Collision2D other) {
		checkCollision(other);
	}
	
	void OnCollisionStay2D(Collision2D other) {
		checkCollision(other);
	}
	void checkCollision(Collision2D other){
		if (other.gameObject.tag == "Player") {
			GameManager.instance.looseLife(damage);
		}
	}
}
