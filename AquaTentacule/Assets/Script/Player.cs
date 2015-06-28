using UnityEngine;
using System.Collections;


public class Player : MonoBehaviour
{
	public float speedRotation;
	public float force;
	public float pushForce;
	public float slowDown;	

	private Animator animator;
	public float dampTime = 0.2f;
	private Vector3 velocity = Vector3.zero;


	public int timetoInverteBack = 10;

	public SpriteRenderer debugSprite;
	private int currentZone;
		
	public GameObject tentacool1;
	public GameObject tentacool2;
	public GameObject tentacool3;

	private Rigidbody2D rb;
	public float scaleRatio;
	private Vector3 startScale; 
	
	private int countTenta;



	private float inverted = 1.0f;

	void Start()
	{
		animator = GetComponent<Animator> ();	

		rb = GetComponent<Rigidbody2D>();
		resetPlayer();
		debugSprite.enabled = false;
		GameManager.instance.setPlayer(this);
		scaleRatio = 1;
		startScale = new Vector3(transform.localScale.x,transform.localScale.y,transform.localScale.z);
	}
	public void resetPlayer(){
		currentZone = 1 ;
		MusicManager.instance.resetMusique();

		countTenta = 1;
		tentacool1.transform.localScale = Vector3.zero;
		tentacool2.transform.localScale = Vector3.zero;
		tentacool3.transform.localScale = Vector3.zero;
	}
		
	void FixedUpdate ()
	{
		float rotation = Input.GetAxis("Horizontal") * -1 ;
		if(Mathf.Abs(rotation)>= 0.1f){
			rb.angularVelocity = rotation * inverted * speedRotation;
		}else{
			rb.angularVelocity = 0;
		}
				
		Vector2 forceVect =Input.GetAxis("Vertical")* transform.up * Mathf.Pow (force, pushForce);
		if(Mathf.Abs(Input.GetAxis("Vertical"))>= 0.5f){
			rb.AddForce (forceVect * inverted);
			animator.SetBool ("swim", true);
		}else{
			rb.velocity = rb.velocity * slowDown;
			animator.SetBool ("swim", false);
		}

	}
	public IEnumerator timer(){
		yield return new WaitForSeconds(timetoInverteBack);
		inverted = 1.0f;
	}

	void OnTriggerExit2D(Collider2D other) {
		MusicManager.instance.exitZone(currentZone);
		if(other.gameObject.tag == "Zone1"){
			currentZone = 2;
		}
		if(other.gameObject.tag == "Zone2"){
			currentZone = 3;
		}
		if(other.gameObject.tag == "Zone3"){
			currentZone = 4;
		}
		if(other.gameObject.tag == "Zone4"){
			currentZone = 5;
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.tag == "NE"){
			Debug.Log("Trigger Dude");
			GameManager.instance.loadNELevel();
			return;
		}
		if(other.gameObject.tag == "Zone1" && currentZone == 2){
			currentZone = 1;
		}
		if(other.gameObject.tag == "Zone2" && currentZone == 3){
			currentZone = 2;
		}
		if(other.gameObject.tag == "Zone3" && currentZone == 4){
			currentZone = 3;
		}
		if(other.gameObject.tag == "Zone4" && currentZone == 5){
			currentZone = 4;
		}
		MusicManager.instance.enterZone(currentZone);
		if (other.gameObject.tag == "Meduse") {
			inverted = -1.0f;
			StartCoroutine(timer());
		}
	}

	public void Eat(){
		animator.SetTrigger("eat");
	}

	public void  Hurt(){
		Debug.Log ("hurt");
		animator.SetTrigger("hurt");
	}

	public void spawnTenta(){
		if(countTenta == 1){
			Debug.Log("Spawn" + countTenta);
			StartCoroutine(growTenta(tentacool1));
			countTenta++;
		}else if (countTenta == 2){
			Debug.Log("Spawn" + countTenta);
			StartCoroutine(growTenta(tentacool2));
			countTenta++;
		}else if( countTenta == 3){
			Debug.Log("Spawn" + countTenta);
			StartCoroutine(growTenta(tentacool3));
			countTenta++;
		}
	}

	private IEnumerator growTenta (GameObject tenta) {
		float scale = 0;
		while(scale < 1){
			yield return new WaitForSeconds(1/100);
			scale += 0.01f;
			Debug.Log(scale);
			tenta.transform.localScale = new Vector3(scale,scale,1);
		}
	
	}
}