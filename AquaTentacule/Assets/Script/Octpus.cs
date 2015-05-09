using UnityEngine;
using System.Collections;

public class Octpus : MonoBehaviour {
	
	public float range;
	public float speed;
	public SpriteRenderer debugSprite;
	
	void Start () {
		debugSprite.enabled = false;
	}

	void Update () {
		GameObject player = GameObject.FindGameObjectWithTag("Player");

		if (Vector3.Distance (transform.position, player.transform.position) < range) {
			transform.LookAt (player.transform.position);
			transform.Rotate (new Vector3 (0, -90, 0), Space.Self);
			transform.Translate (new Vector3 (speed * Time.deltaTime, 0, 0));
		}
	}
/*
 	void Update () {
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		transform.LookAt (player.transform.position);
		transform.Rotate (new Vector3 (0, -90, 0), Space.Self);
		
		if (Vector3.Distance (transform.position, player.transform.position) < range) {
			transform.Translate (new Vector3 (speed * Time.deltaTime, 0, 0));
		}
	}
//*/	
	public void OnDrawGizmos(){
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position,range);
	}
}
