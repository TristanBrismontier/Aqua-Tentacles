using UnityEngine;
using System.Collections;

public class Oursins : MonoBehaviour {

	public double damage;

	void OnCollisionEnter2D() {
		Debug.Log("PLOP");
		GameManager.instance.looseLife ((int)damage);
	}
}