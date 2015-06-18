using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	
	public static GameManager instance = null;


	public float life;
	public float starving = 2.0f;
	public Transform startPosition;
	public int minFood;
	public int maxFood;
	public int octopus;
	public int FishCount;

	public int maxRange;

	public AudioClip[] eatSounds;
	public AudioClip[] deadSounds;
	public GameObject[] foods;
	public GameObject Octopus;
	public GameObject FishEye;
	public GameObject bubbleExplosionPlop;
	private Player player;
	private List<GameObject> instanciatesGameObjects = new List<GameObject>();

	private bool canDie;

	void Awake () {
		if (instance == null){
			instance = this;
		}
		else if(instance != this)
		{
			Destroy(gameObject);
		}
		
		DontDestroyOnLoad(gameObject);
	}

	public void Start(){
		initGame();
	}

	public void setPlayer(Player _player){
		player = _player;
	}

	public void initGame(){
		canDie = true;
		int foodNumber = Random.Range(minFood,maxFood);
		for(int i = 0 ; i<foodNumber; i++){
			AddFood();
		}

		for(int i = 0 ; i<octopus; i++){
			RespawnOctopus();
		}

		for(int i = 0 ; i<FishCount; i++){
			RespawnFishEye();
		}

		Vector3 playerPos = Player.instance.transform.position;
		startPosition.position = new Vector3(playerPos.x,playerPos.y,playerPos.z);
			
	}

	public void AddFood(){
		GameObject foodChoice = foods[Random.Range (0, foods.Length)];
		GameObject go = Instantiate(foodChoice,getLimitedRandomPos(), Quaternion.identity)as GameObject;
		instanciatesGameObjects.Add(go);
	}
	
	public void RespawnOctopus(){
		GameObject go = Instantiate(Octopus, getLimitedRandomPos(), Quaternion.identity) as GameObject;
		instanciatesGameObjects.Add(go);
	}

	public void RespawnFishEye(){
		GameObject go = Instantiate(FishEye, getLimitedRandomPos(), Quaternion.identity) as GameObject;
		instanciatesGameObjects.Add(go);
	}

	private Vector3 getLimitedRandomPos(){
		float x = (float)(Random.Range(-maxRange,maxRange));
		float y = (float)(Random.Range(-maxRange,maxRange));
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		float deltaX = x  - player.transform.position.x;
		float deltaY = y  - player.transform.position.y;
		if( Mathf.Abs(deltaX) < 3.1f ){
			x=x+((deltaX<=0)?-3:3);
		}
		if( Mathf.Abs(deltaY) < 3.1f ){
			y=y+((deltaY<=0)?-3:3);
		}
		return new Vector3( x, y, 0);
	}

	public void eatFood(int nutritionFact){
		AddFood ();
		eat (nutritionFact);
		SoundManager.instance.RandomizeSfx(eatSounds);
	}
	public void eatOctoPus(int nutritionFact){
		RespawnOctopus();
		Player.instance.spawnTenta();
		eat (nutritionFact);
	}
	public void eatFishEye(int nutritionFact){
		RespawnFishEye();
		eat (nutritionFact);
	}

	public void looseLife(int damage){
		eat (damage*-1);
	}

	private void eat(int nutritionFact){
		life = life + nutritionFact;
		if(nutritionFact > 0){
			player.Eat();
		}
	}

	void Update () {
		//Life max = 150 pv
		if (life > 150) {
			life = 150;
		}
		//Each seconds = -2 pv
		life -= starving * Time.deltaTime;
		if(life<=0){
			death();
		}
	}
	public void death(){
		if(canDie){
			canDie = false;
			life = 100;
			SoundManager.instance.RandomizeSfx(deadSounds);
			GameObject player = GameObject.FindGameObjectWithTag("Player");
			GameObject buble = Instantiate(bubbleExplosionPlop,new Vector3( player.transform.position.x,player.transform.position.y,player.transform.position.z-1), player.transform.rotation) as GameObject;
			StartCoroutine(deadPlayer(buble));
		}
	}

	public IEnumerator deadPlayer(GameObject buble){
		yield return new WaitForSeconds(2);
		Destroy(buble);
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		player.transform.localScale = new Vector3(2.3f,2.3f,2.3f);
		Player.instance.resetM();
		Player.instance.transform.position = startPosition.position;
		yield return new WaitForSeconds(0.5f);
		Player.instance.resetMusique();
		foreach (GameObject gobj in instanciatesGameObjects)
		{
			Destroy(gobj);
		}
		initGame();
	}

}
