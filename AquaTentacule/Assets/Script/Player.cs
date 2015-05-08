using UnityEngine;
using System.Collections;


public class Player : MonoBehaviour
{
	public float speedRotation = 100.0f;
	public float force = 10.0f;
	private Rigidbody2D rb;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate ()
	{
		if (Input.GetKey (KeyCode.Q)) {
			rb.angularVelocity = speedRotation;
		} else if (Input.GetKey (KeyCode.D)) {
			rb.angularVelocity = -speedRotation;
		} else {
			rb.angularVelocity = 0f;
		}

		if (Input.GetKey (KeyCode.Z)) {
			rb.AddForce(transform.up * (force * force * force * force * force));
		}

	}
}