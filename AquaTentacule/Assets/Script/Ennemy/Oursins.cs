using UnityEngine;
using System.Collections;

public class Oursins : MonoBehaviour {

	public double damage;

	void OnCollisionEnter2D() {
		GameManager.instance.looseLife ((int)damage);
	}
}