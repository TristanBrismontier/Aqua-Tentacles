using UnityEngine;
using System.Collections;

public class Enemys : MonoBehaviour {

	public Transform Target;
	//private GameObject Enemy;
	private GameObject Player;
	private float range;
	public float speed;

	void Start () {
	}

	void Update () {
		transform.LookAt (Target.position);
		transform.Rotate (new Vector3 (0, -90, 0), Space.Self);

		if (Vector3.Distance (transform.position, Target.position) > 1f) {
			transform.Translate (new Vector3 (speed * Time.deltaTime, 0, 0));
		}
	}
}
