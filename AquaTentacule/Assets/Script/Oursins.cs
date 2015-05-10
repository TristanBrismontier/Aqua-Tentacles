using UnityEngine;
using System.Collections;

public class Oursins : MonoBehaviour {

	public double damage;
	private bool colision;


	void OnCollisionEnter2D() {
		colision = true;
	}

	void OnCollisionExit2D(){
		colision = false;
	}

	void Update () {
		if (colision) {
			print ("Aie");
			GameManager.instance.looseLife ((int)damage);
		}
	}



}