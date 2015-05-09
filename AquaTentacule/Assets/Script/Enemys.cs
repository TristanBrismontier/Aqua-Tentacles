using UnityEngine;
using System.Collections;

public class Enemys : MonoBehaviour {

	public float range;
	public float speed;

	void Start () {
	}

	void Update () {
	

		GameObject player = GameObject.FindGameObjectWithTag("Player");
		transform.LookAt (player.transform.position);
		transform.Rotate (new Vector3 (0, -90, 0), Space.Self);
		
		if (Vector3.Distance (transform.position, player.transform.position) < range) {
			transform.Translate (new Vector3 (speed * Time.deltaTime, 0, 0));
		}
	}

	public void OnDrawGizmos(){
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position,range);
	}
}
