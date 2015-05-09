using UnityEngine;
using System.Collections;

public class Enemys : MonoBehaviour {

	private float range;
	public float speed;

	void Start () {
	}

	void Update () {
		Gizmos.DrawSphere(transform.position,3f);

		GameObject player = GameObject.FindGameObjectWithTag("Player");
		if (Vector3.Distance (transform.position, player.transform.position) < 3f) {
				
				transform.LookAt (player.transform.position);
				transform.Rotate (new Vector3 (0, -90, 0), Space.Self);
				transform.Translate (new Vector3 (speed * Time.deltaTime, 0, 0));
		}
	}
}
