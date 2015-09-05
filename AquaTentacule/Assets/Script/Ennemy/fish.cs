using UnityEngine;
using System.Collections;


public class fish : MonoBehaviour {

	
	public int nutritionFact;
	public float speedMax;
	public float speedRotationMax;
	
	private Transform target;
	private Rigidbody2D rb;
	private float speed;
	private bool face=true;


	public float fireRate = 0.5F;
	private float nextFire = 0.0F;


	// Use this for initialization
	void Start () {
		face = Random.Range(0,10) >= 5;
		speed  = (float)(Random.Range(2,speedMax*12)/10);
		rb = GetComponent<Rigidbody2D>();
		rb.freezeRotation =true;
		GameObject go = GameObject.FindGameObjectWithTag("Player");
		transform.localScale = new Vector3(face?3:-3,3, 1);
		rb.velocity = new Vector2((face?-1:1)*speed ,rb.velocity.y);
	}

	void FixedUpdate (){
		transform.localScale = new Vector3(face?3:-3,3, 1);
		rb.velocity = new Vector2((face?-1:1)*speed ,rb.velocity.y);
	}
	
	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player"){
			GameManager.instance.eatFood(nutritionFact);
			Destroy(this.gameObject);
			
		}
		if ( Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			Debug.Log("Colision : " + face);
			speed  = (float)(Random.Range(2,speedMax*12)/10);
			face = !face;
			Debug.Log("Colision : " + face);
		}

		if(coll.gameObject.tag == "Octo"){
			Destroy(this.gameObject);
		}
		
	}
}
