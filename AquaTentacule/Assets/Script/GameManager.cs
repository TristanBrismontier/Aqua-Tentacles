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
	public bool playerHasTenta;
	public int tentaCount;

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
		player.setEvolution(tentaCount);
		startPosition.position = new Vector3(player.transform.position.x,player.transform.position.y,player.transform.position.z);
	}

	public void initGame(){
		playerHasTenta = false;
		canDie = true;
		tentaCount = 0;
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
		player.spawnTenta();
		playerHasTenta = true;
		eat (nutritionFact);
	}
	public void eatFishEye(int nutritionFact){
		RespawnFishEye();
		eat (nutritionFact);
	}

	public void looseLife(int damage){
		eat (damage*-1);
		player.Hurt();
	}

	private void eat(int nutritionFact){
		life = life + nutritionFact;
		Debug.Log(life);
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

	public void loadNELevel(){
		tentaCount = player.countTenta;
		Application.LoadLevel("FourDir");
	}

	public void death(){
		if(canDie){
			canDie = false;
			life = 100;
			SoundManager.instance.RandomizeSfx(deadSounds);
			GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
			playerGO.transform.localScale = new Vector3(0f,0f,0f);
			GameObject buble = Instantiate(bubbleExplosionPlop,new Vector3( playerGO.transform.position.x,playerGO.transform.position.y,playerGO.transform.position.z-1), playerGO.transform.rotation) as GameObject;
			StartCoroutine(deadPlayer(buble));
		}
	}

	public IEnumerator deadPlayer(GameObject buble){
		yield return new WaitForSeconds(2);
	
		Destroy(buble);
		GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
		playerGO.SetActive(true);
		playerGO.transform.localScale = new Vector3(2.3f,2.3f,2.3f);
		MusicManager.instance.resetM();
		player.transform.position = startPosition.position;
		yield return new WaitForSeconds(0.5f);
		player.resetPlayer();
		foreach (GameObject gobj in instanciatesGameObjects)
		{
			Destroy(gobj);
		}
		initGame();
	}

}
