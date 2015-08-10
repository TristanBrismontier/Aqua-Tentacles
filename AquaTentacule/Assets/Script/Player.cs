using UnityEngine;
using System.Collections;


public class Player : MonoBehaviour
{
	public float speedRotation;
	public float force;
	public float pushForce;
	public float slowDown;	
	private float forceRatio;

	private Animator animator;
	public float dampTime = 0.2f;
	private Vector3 velocity = Vector3.zero;


	public int timetoInverteBack = 10;

	public SpriteRenderer debugSprite;
	private int currentZone;
	private int leftZone;
		
	public GameObject tentacool1;
	public GameObject tentacool2;
	public GameObject tentacool3;

	public GameObject eye1;
	public GameObject eye2;
	public GameObject eye3;

	private Rigidbody2D rb;
	public float scaleRatio;
	private Vector3 startScale; 
	
	public int countTenta;
	public int countEyes;

	private float inverted = 1.0f;

	void Start()
	{
		animator = GetComponent<Animator> ();	
		speedRotation = speedRotation*-1;
		forceRatio = Mathf.Pow (force, pushForce);
		rb = GetComponent<Rigidbody2D>();
		resetPlayer();
		debugSprite.enabled = false;
		GameManager.instance.setPlayer(this);
		scaleRatio = 1;
		startScale = new Vector3(transform.localScale.x,transform.localScale.y,transform.localScale.z);
	}
	public void resetPlayer(){
		currentZone = 1;
		leftZone = 0;
		//MusicManager.instance.resetMusique();

		countTenta = 0;
		tentacool1.transform.localScale = Vector3.zero;
		tentacool2.transform.localScale = Vector3.zero;
		tentacool3.transform.localScale = Vector3.zero;

		countEyes = 0;
		eye1.transform.localScale = Vector3.zero;
		eye2.transform.localScale = Vector3.zero;
		eye3.transform.localScale = Vector3.zero;
	}
	public void setEvolution(int tenta){
		countTenta = tenta;
		if(countTenta >= 1){
			tentacool1.transform.localScale = new Vector3(1,1,1);
		}else if (countTenta >= 2){
			tentacool2.transform.localScale = new Vector3(1,1,1);
		}else if( countTenta == 3){
			tentacool3.transform.localScale = new Vector3(1,1,1);
		}
	}

	public void setEvolutionEye(int eye){
		countEyes = eye;
		if(countEyes >= 1){
			eye1.transform.localScale = new Vector3(1,1,1);
			//changer scale hole
		}else if (countEyes >= 2){
			eye2.transform.localScale = new Vector3(1,1,1);
			//changer scale hole
		}else if( countEyes == 3){
			eye3.transform.localScale = new Vector3(1,1,1);
			//changer scale hole
		}
	}
		
	void FixedUpdate ()
	{
		float rotation = Input.GetAxis("Horizontal");
		if(Mathf.Abs(rotation)>= 0.1f){
			rb.angularVelocity = rotation * inverted * speedRotation;
		}else{
			rb.angularVelocity = 0;
		}
				
		Vector2 forceVect =Input.GetAxis("Vertical")* transform.up * forceRatio;
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

	void OnTriggerExit2D(Collider2D other)
	{	
		switch(other.gameObject.tag)
		{
		case "Zone1":
			leftZone = 1;
			break;

		case "Zone2":
			leftZone = 2;
			break;

		case "Zone3":
			leftZone = 3;
			break;

		case "Zone4":
			leftZone = 4;
			break;

		case "Zone5":
			leftZone = 5;
			break;

		default:
			break;
		}


		MusicManager.instance.exitZone(leftZone);
	}

	void OnTriggerEnter2D(Collider2D other)
	{

		switch(other.gameObject.tag)
		{
		case "Zone1":
			currentZone = 1;
			break;
		case "Zone2":
			currentZone = 2;
			break;
		case "Zone3":
			currentZone = 3;
			break;
		case "Zone4":
			currentZone = 4;
			break;
		case "Zone5":
			currentZone = 5;
			break;
		default:
			break;
		}

		MusicManager.instance.enterZone(currentZone);

		if (other.gameObject.tag == "Meduse")
		{
			inverted = -1.0f;
			StartCoroutine(timer());
			SoundManager.instance.RandomizeSfx(SoundManager.instance.efxSource, SoundManager.instance.meduseSounds);
		}
	}

	public void Eat(){
		animator.SetTrigger("eat");
	}

	public void  Hurt(){
		animator.SetTrigger("hurt");
	}

	public void spawnTenta(){
		if(countTenta == 0){
			StartCoroutine(growTenta(tentacool1));
			countTenta++;
		}else if (countTenta == 1){
			StartCoroutine(growTenta(tentacool2));
			countTenta++;
		}else if( countTenta == 2){
			StartCoroutine(growTenta(tentacool3));
			countTenta++;
		}
	}

	public void spawnEye(){
		if(countEyes == 0){
			StartCoroutine(growEye(eye1));
			countEyes++;
		}else if (countEyes == 1){
			StartCoroutine(growEye(eye2));
			countEyes++;
		}else if( countEyes == 2){
			StartCoroutine(growEye(eye3));
			countEyes++;
		}
	}

	private IEnumerator growTenta (GameObject tenta) {
		float scale = 0;
		while(scale < 1){
			yield return new WaitForSeconds(1/100);
			scale += 0.01f;
			tenta.transform.localScale = new Vector3(scale,scale,1);
		}
	}

	private IEnumerator growEye (GameObject eye) {
		float scale = 0;
		while(scale < 1){
			yield return new WaitForSeconds(1/100);
			scale += 0.01f;
			eye.transform.localScale = new Vector3(scale,scale,1);
		}
	}

	public void setVelocity(Vector2 velo){
		rb.velocity = velo;
	}

	public Vector2 getVelocity(){
		return rb.velocity;
	}
}
