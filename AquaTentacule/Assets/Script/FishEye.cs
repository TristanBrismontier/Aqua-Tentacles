using UnityEngine;
using System.Collections;

public class FishEye : MonoBehaviour {
	
	public float rangeDash;
	public float rangeAttack;
	public float speed;
	public SpriteRenderer debugSprite;

	public Animator animator;
	
	void Start () {
		debugSprite.enabled = false;
	}
	
	void Update () {
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		
		if (Vector3.Distance (transform.position, player.transform.position) < rangeDash) {
			animator.SetBool ("Dash", true);
			speed = 4;
			transform.LookAt (player.transform.position);
			transform.Rotate (new Vector3 (0, -90, 0), Space.Self);
			transform.Translate (new Vector3 (speed * Time.deltaTime, 0, 0));
		} else {
			animator.SetBool ("Dash", false);
		}

		if (Vector3.Distance (transform.position, player.transform.position) < rangeAttack) {
			animator.SetBool ("Attack", true);
			print("TOUCHE");
		} else {
			animator.SetBool("Attack",false);
		}
	}

	public void OnDrawGizmos(){
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position,rangeDash);
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position,rangeAttack);
	}
}
