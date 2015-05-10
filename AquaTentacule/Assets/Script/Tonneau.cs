using UnityEngine;
using System.Collections;

public class Tonneau : MonoBehaviour {
	
	public float speedMax;
	public float speedRotationMax;
	public float range;
	public double dommage;
	public AudioSource SoundTonneau;
	
	private Transform target;
	private Rigidbody2D rb;
	private float speed;
	// Use this for initialization
	void Start () {
		speed  = (float)(Random.Range(0,speedMax*10)/10);
		rb = GetComponent<Rigidbody2D>();
		GameObject go = GameObject.FindGameObjectWithTag("Player");
		target = go.transform;
		Vector2 randomVector = Random.insideUnitCircle;
		rb.velocity = new Vector2(randomVector.x*speed,randomVector.y*speed);
		transform.Rotate(0.0f, 0.0f, Random.Range(0.0f, 360.0f));
		rb.angularVelocity = (float)(Random.Range(10,speedRotationMax*10)/10);
		
	}

	void Update() {

		GameObject player = GameObject.FindGameObjectWithTag("Player");
		
		if (Vector3.Distance (transform.position, player.transform.position) < range) {
			GameManager.instance.looseLife ((int)dommage);
			Player.instance.Hurt ();
			SoundTonneau.volume = 1;
		} else {
			SoundTonneau.volume = 0;
		}
	}
	
	void OnCollisionEnter2D(Collision2D coll) {
		
		if(coll.gameObject.tag == "InhertElement"){
			Vector2 randomVector = Random.insideUnitCircle;
			rb.velocity = new Vector2(randomVector.x*speed,randomVector.y*speed);
		}
		
	}

	public void OnDrawGizmos(){
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position,range);
	}
}
