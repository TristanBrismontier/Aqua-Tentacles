using UnityEngine;
using System.Collections;

public class Octopus : MonoBehaviour {
	
	public float range;
	public float speed;
	public float slowDown;
	public int nutritionFact;
	public GameObject bubbleExplosion;
	private Rigidbody2D rb;
	private float speedDamp;
	public SpriteRenderer debugSprite;
	private BoxCollider2D hitBox;
	public GameObject inkParticule;

	
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		hitBox = GetComponent<BoxCollider2D>();
		debugSprite.enabled = false;
		float scale = (float) (Random.Range(50,130)/100);
		Debug.Log ("Scale : "+ scale);
		speedDamp = 0;
		//transform.localScale = new Vector3(scale,scale,scale);
	}

	void Update () {
		GameObject player = GameObject.FindGameObjectWithTag("Player");

		if (Vector3.Distance (transform.position, player.transform.position) < range) {
			transform.LookAt (player.transform.position);
			transform.Rotate (new Vector3 (0, 90, 0), Space.Self);
			transform.Translate (new Vector3 (speed * Time.deltaTime, 0, 0));
			speedDamp = speed;
		}else{
	
		rb.velocity = Vector2.zero;

		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player"){
<<<<<<< HEAD
			GameManager.instance.eatOctoPus(nutritionFact);
			Instantiate (bubbleExplosion, transform.position, transform.rotation);
			Destroy(this.gameObject);
=======
			if(GameManager.instance.life > 80){
				GameManager.instance.eatOctoPus(nutritionFact);
				Destroy(this.gameObject);
			}else{
				ink();
			}
>>>>>>> origin/master
			
		}
		if(coll.gameObject.tag == "InhertElement"){
			Vector2 randomVector = Random.insideUnitCircle;
			//rb.velocity = new Vector2(randomVector.x*speed,randomVector.y*speed);
		}
		
	}

	public void ink(){
		hitBox.isTrigger = true;
		transform.Translate (new Vector3 (speed * 4 * Time.deltaTime, 0, 0));
		GameObject particules = Instantiate(inkParticule, new Vector3(transform.position.x,transform.position.y,-5f), Quaternion.identity) as GameObject;
		StartCoroutine(DestroyLater(particules));

	}

	public IEnumerator DestroyLater(GameObject particules){
		yield return new WaitForSeconds(2);
		hitBox.isTrigger = false;
		yield return new WaitForSeconds(28);
		Destroy(particules);
	}

	public void OnDrawGizmos(){
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position,range);
	}
}
