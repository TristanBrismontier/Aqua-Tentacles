using UnityEngine;
using System.Collections;

public class Meduse : MonoBehaviour {
	
	public float slowDown;
	public int nutritionFact;
	public GameObject bubbleExplosion;
	private Rigidbody2D rb;
	public SpriteRenderer debugSprite;
	private BoxCollider2D hitBox;
	
	
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		hitBox = GetComponent<BoxCollider2D>();
		debugSprite.enabled = false;
		float scale = (float) (Random.Range(50,130)/100);
		//transform.localScale = new Vector3(scale,scale,scale);
	}
	
	void OnCollisionEnter2D(Collision2D coll) {

		if(coll.gameObject.tag == "InhertElement"){
			Vector2 randomVector = Random.insideUnitCircle;
			//rb.velocity = new Vector2(randomVector.x*speed,randomVector.y*speed);
		}
		
	}
	
	public IEnumerator DestroyLater(GameObject particules){
		yield return new WaitForSeconds(2);
		hitBox.isTrigger = false;
		yield return new WaitForSeconds(28);
		Destroy(particules);
	}

}