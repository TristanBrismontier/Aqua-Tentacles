using UnityEngine;
using System.Collections;

[System.Serializable]
public class Limits
{
	public float xMin, xMax, yMin, yMax;
}

public class Player : MonoBehaviour
{
	public float speed = 10;
	public Limits lim;

	void Start()
	{
	}
	
	void FixedUpdate ()
	{
		float moveHor = Input.GetAxis ("Horizontal");
		float moveVert = Input.GetAxis ("Vertical");
		
		Vector3 move = new Vector3 (moveHor, moveVert,0.0f );
		GetComponent<Rigidbody>().velocity = move * speed;
		
		GetComponent<Rigidbody>().position = new Vector3 
			(
				Mathf.Clamp (GetComponent<Rigidbody>().position.x, lim.xMin, lim.xMax), 
				Mathf.Clamp (GetComponent<Rigidbody>().position.y, lim.yMin, lim.yMax),
				0.0f
				);

	}
}