using UnityEngine;
using System.Collections;

public class Scaphandre : MonoBehaviour {

	public float speedMax;
	public float speedRotationMax;
	
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
	
	void OnCollisionEnter2D(Collision2D coll) {

		if(coll.gameObject.tag == "InhertElement"){
			Vector2 randomVector = Random.insideUnitCircle;
			rb.velocity = new Vector2(randomVector.x*speed,randomVector.y*speed);
		}
		
	}
}
