using UnityEngine;
using System.Collections;

public class Ennemy : MonoBehaviour {

	public float nuttritionFact;
	public float speed;

	private Transform target;
	private Rigidbody2D rb;
	
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		GameObject go = GameObject.FindGameObjectWithTag("Player");
		target = go.transform;
		Vector2 randomVector = Random.insideUnitCircle.Normalize();
		Debug.Log (randomVector);
		rb.velocity = new Vector2(randomVector.x*speed,randomVector.y*speed);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
