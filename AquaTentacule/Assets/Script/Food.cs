using UnityEngine;
using System.Collections;

public class Food : MonoBehaviour {

	public int nutritionFact;
	public float speedMax;

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
	}
	
	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player"){
			GameManager.instance.eatFood(nutritionFact);
			Destroy(this.gameObject);
		}
		if(coll.gameObject.tag == "InhertElement"){
			Vector2 randomVector = Random.insideUnitCircle;
			rb.velocity = new Vector2(randomVector.x*speed,randomVector.y*speed);
		}
	
	}
}
