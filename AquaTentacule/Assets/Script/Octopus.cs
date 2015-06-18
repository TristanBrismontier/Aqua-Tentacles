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
	public GameObject inkParticule;

	public AudioClip[] crySounds;
	public AudioClip[] deadSounds;

	
	void Start () {
		rb = GetComponent<Rigidbody2D>();
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
		rb.angularVelocity = 0.0f;
		rb.velocity = Vector2.zero;

		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player"){
			if(randomStat(10)){
				SoundManager.instance.RandomizeSfx(deadSounds);
				Instantiate (bubbleExplosion, transform.position, transform.rotation);
				GameManager.instance.eatOctoPus(nutritionFact);
				Destroy(this.gameObject);
			}else{
				ink();
			}
		}
		if(coll.gameObject.tag == "InhertElement"){
			Vector2 randomVector = Random.insideUnitCircle;
			//rb.velocity = new Vector2(randomVector.x*speed,randomVector.y*speed);
		}
	}

	private bool randomStat(int percent){
		int random = Random.Range(0,100);
		return random>=percent;
	}
	public void ink(){
		SoundManager.instance.RandomizeSfx(crySounds);
		transform.Translate (new Vector3 (speed * 4 * Time.deltaTime, 0, 0));
		GameObject particules = Instantiate(inkParticule, new Vector3(transform.position.x,transform.position.y,-5f), Quaternion.identity) as GameObject;
		StartCoroutine(DestroyLater(particules));

	}

	public IEnumerator DestroyLater(GameObject particules){
		yield return new WaitForSeconds(28);
		Destroy(particules);
	}

	public void OnDrawGizmos(){
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position,range);
	}
}
