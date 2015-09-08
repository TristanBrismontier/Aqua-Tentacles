﻿using UnityEngine;
using System.Collections;

public class DogSeaMeat : MonoBehaviour {

	public float speedMax;
	public float speedRotationMax;

	public float dogScale=6F;
	private Transform target;
	private Rigidbody2D rb;
	private float speed;
	private bool face=true;
	private float lastX;
	private int jumpCount=0;

	private float fireJump = 3.0F;
	private float nextFire = 0.0F;
	
	
	// Use this for initialization
	void Start () {
		nextFire = Time.time;
		face = Random.Range(0,10) >= 5;
		speed  = 3f;
		rb = GetComponent<Rigidbody2D>();
		rb.freezeRotation =true;
		GameObject go = GameObject.FindGameObjectWithTag("Player");
		transform.localScale = new Vector3(face?dogScale:-dogScale,dogScale, 1);
		rb.velocity = new Vector2((face?-1:1)*speed ,rb.velocity.y);

	}
	
	void FixedUpdate (){
	
		if ( (Time.time > nextFire) && Mathf.Abs(transform.position.x - lastX)<0.003f) {;
			jumpCount++;
			nextFire = Time.time + fireJump;
			//Jump Script      
			rb.AddForce (Vector2.up * 7, ForceMode2D.Impulse);
			Debug.Log("#####Jump : + JumpCount : "+jumpCount+" Time.time ! "+Time.time+" > "+nextFire+" "+fireJump);
			if(jumpCount>4){
				face =!face;
				jumpCount=0;
			}
		}
		transform.localScale = new Vector3(face?dogScale:-dogScale,dogScale, 1);
		rb.velocity = new Vector2((face?-1:1)*speed ,rb.velocity.y);
		lastX = transform.position.x;
	}
	
	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player"){
			GameManager.instance.death();
			
		}
		
		if(coll.gameObject.tag == "Octo"){
			Destroy(this.gameObject);
		}
		
	}
}