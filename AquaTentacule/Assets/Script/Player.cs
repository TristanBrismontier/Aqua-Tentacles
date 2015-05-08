using UnityEngine;
using System.Collections;


public class Player : MonoBehaviour
{
	public float speed = 10;
	private Rigidbody2D rb;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate ()
	{
		float moveHor = Input.GetAxis ("Horizontal");
		float moveVert = Input.GetAxis ("Vertical");
		
		Vector3 move = new Vector3 (moveHor, moveVert,0.0f );
		rb.velocity = move * speed;
		
		rb.position = new Vector3 (rb.position.x, rb.position.y,0.0f);

	}
}