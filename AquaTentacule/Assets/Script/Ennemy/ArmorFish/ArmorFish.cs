using UnityEngine;
using System.Collections;

public class ArmorFish : MonoBehaviour {

	
	public float rangeDash;
	public float speed;


	private Rigidbody2D rb;
	private Vector3 velocity = Vector3.zero;
	public float dampTime ;


	void Start () {
		rb = GetComponent<Rigidbody2D>();

	}
	
	void Update () {
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		
		if (Vector3.Distance (transform.position, player.transform.position) < rangeDash) {
			int eatable = 1;
			//speed = 4;

			transform.LookAt (Vector3.SmoothDamp (transform.position, player.transform.position, ref velocity, dampTime));

			transform.Rotate (new Vector3 (0, -90, 0), Space.Self);
			transform.Translate (new Vector3 (speed * Time.deltaTime, 0, 0));
		} else {
			rb.velocity = Vector2.zero;
			rb.angularVelocity = 0.0f;
		}
	}

	public void eat(){
		GameManager.instance.death();
	}
	
	public void OnDrawGizmos(){
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position,rangeDash);
	}
}
