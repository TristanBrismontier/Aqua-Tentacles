using UnityEngine;
using System.Collections;

public class Octpus : MonoBehaviour {
	
	public float range;
	public float speed;
	public int nutritionFact;
	private Rigidbody2D rb;
	public SpriteRenderer debugSprite;

	
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		debugSprite.enabled = false;
		float scale = (float) (Random.Range(50,130)/100);
		transform.localScale = new Vector3(scale,scale,scale);
	}

	void Update () {
		GameObject player = GameObject.FindGameObjectWithTag("Player");

		if (Vector3.Distance (transform.position, player.transform.position) < range) {
			transform.LookAt (player.transform.position);
			transform.Rotate (new Vector3 (0, 90, 0), Space.Self);
			transform.Translate (new Vector3 (speed * Time.deltaTime, 0, 0));
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player"){
			GameManager.instance.eatFood(nutritionFact);
			Destroy(this.gameObject);
			
		}
		if(coll.gameObject.tag == "InhertElement"){
			Vector2 randomVector = Random.insideUnitCircle;
			//rb.velocity = new Vector2(randomVector.x*speed,randomVector.y*speed);
		}
		if(coll.gameObject.tag == "Octo"){
			GameManager.instance.RespawnOctopus();
			Destroy(this.gameObject);
		}
		
	}

	public void OnDrawGizmos(){
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position,range);
	}
}
