using Mono.Xml.Xsl;
using System.Collections;
using UnityEngine;


public class Player : MonoBehaviour
{
	public float speedRotation;
	public float force;
	public float pushForce;
	public float slowDown;	
	public float scaleRatio;
	public int timetoInverteBack;
	public SpriteRenderer debugSprite;

	public GameObject[] tentacools;
	public GameObject[] eyes;
	public int countTenta;
	public int countEyes;

	public float dashRate = 1.0F;
	private float nextDash = 0.0F;

	private int currentZone;
	private int leftZone;
	private Rigidbody2D rb;
	private Animator animator;
	private Vector3 normaleScale; 
	private Vector3 smallScale;
	private float forceRatio;
	private float inverted = 1.0f;

	void Start()
	{
		nextDash = Time.time;
		animator = GetComponent<Animator> ();	
		speedRotation = speedRotation*-1;
		forceRatio = Mathf.Pow (force, pushForce);
		rb = GetComponent<Rigidbody2D>();
		resetPlayer();
		debugSprite.enabled = false;
		GameManager.instance.setPlayer(this);
		float nScal = (1+scaleRatio);
		normaleScale = new Vector3(transform.localScale.x * nScal
		                           ,transform.localScale.y * nScal
		                           ,transform.localScale.z);
		float sScal = (1-scaleRatio);
		smallScale = new Vector3(transform.localScale.x * sScal
		                         ,transform.localScale.y * sScal
		                         ,transform.localScale.z);
	}

	public void resetPlayer(){
		currentZone = 1;
		leftZone = 0;
		countTenta = 0;
		foreach(GameObject tenta in tentacools){
			tenta.transform.localScale = Vector3.zero;
		}

		countEyes = 0;
		foreach(GameObject eye in eyes){
			eye.transform.localScale = Vector3.zero;
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

		if (Input.GetKeyDown (KeyCode.LeftShift) &&(Time.time > nextDash) ){
			nextDash = Time.time + dashRate;
			Debug.Log("Dash");
			rb.AddForce (transform.up *(100 + (100*countTenta)), ForceMode2D.Impulse);
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

	public void  Hurt(bool looseMuta){
		SoundManager.instance.RandomizeSfx (SoundManager.instance.efxSource, SoundManager.instance.deadSounds);
		animator.SetTrigger("hurt");
		if(looseMuta)
			looseMutation();
	}

	public void bigSize(){
		transform.localScale = normaleScale;
	}

	public void smallSize(){
		transform.localScale = smallScale;
	}

	public void setVelocity(Vector2 velo){
		rb.velocity = velo;
	}
	
	public Vector2 getVelocity(){
		return rb.velocity;
	}

	public void spawnTenta(){
		if(countTenta >= tentacools.Length)
			return;
		StartCoroutine(growMutation(tentacools[countTenta]));
		countTenta++;
	}

	public void spawnEye(){
		if(countEyes >= eyes.Length)
			return;
		StartCoroutine(growMutation(eyes[countEyes]));
		countEyes++;
	}

	public void restoreEvolutionEye(int eye){
		countEyes = eye;
		displayMut (eye ,eyes);
	}

	public void restoreEvolutionTenta(int tenta){
		countTenta = tenta;
		displayMut (tenta ,tentacools);
	}

	private void looseMutation(){

		int random = Random.Range(0,100);
		Debug.Log("looseMutation " + random);
		if(countEyes > 0 && (random >= 50 || countTenta == 0)){
			Debug.Log("looseMutation EYES");
			countEyes--;
			StartCoroutine(reduceMutation(eyes[countEyes]));
		} else if (countTenta >0){
			Debug.Log("looseMutation TENTA");
			countTenta--;
			StartCoroutine(reduceMutation(tentacools[countTenta]));
		}
	}

	private void displayMut (int count,GameObject[]  muts)
	{
		for (int i = 0; i < count ; i++) {
			muts [i].transform.localScale = new Vector3 (1, 1, 1);
		}
	}

	private IEnumerator growMutation(GameObject mut) {
		float scale = 0;
		while(scale < 1){
			yield return new WaitForSeconds(1/100);
			scale += 0.01f;
			mut.transform.localScale = new Vector3(scale,scale,1);
		}
	}

	private IEnumerator reduceMutation(GameObject mut) {
		float scale = 1;
		while(scale > 0){
			yield return new WaitForSeconds(1/100);
			scale -= 0.01f;
			mut.transform.localScale = new Vector3(scale,scale,scale);
		}
	}
}
