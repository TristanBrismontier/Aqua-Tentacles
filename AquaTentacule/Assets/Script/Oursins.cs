using UnityEngine;
using System.Collections;

public class Oursins : MonoBehaviour {

	void checkCollision(Collision2D other) {

		if (other.gameObject.tag == "Player") {
			GameManager.instance.looseLife(10);
		}
	}

}