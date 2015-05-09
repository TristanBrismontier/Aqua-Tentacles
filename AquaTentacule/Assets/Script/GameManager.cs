using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	public static GameManager instance = null;


	public float life;
	public float starving = 2.0f;
	public Transform startPosition;
	public int minFood;
	public int maxFood;

	public int maxRange;

	public int frameCount = 0;
	public AudioClip[] eatSounds;
	public GameObject[] foods;
	private Player player;

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
		int foodNumber = Random.Range(minFood,maxFood);
		for(int i = 0 ; i<foodNumber; i++){
			AddFood();
		}
		Vector3 playerPos = Player.instance.transform.position;
		startPosition.position = new Vector3(playerPos.x,playerPos.y,playerPos.z);
			
	}


	public void AddFood(){
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
		GameObject foodChoice = foods[Random.Range (0, foods.Length)];
		Instantiate(foodChoice, new Vector3( x, y, 0), Quaternion.identity);

	}

	public void eatFood(int nutritionFact){
		life = life + nutritionFact;
		AddFood ();
		player.Eat();
		SoundManager.instance.RandomizeSfx(eatSounds);

	}

	void Update () {
		//Life max = 150 pv
		if (life > 150) {
			life = 150;
		}
		frameCount++;
		if(frameCount %10 == 0 && life > 100){
			float scale = Remap(life, 100 , 150, 1,3);
			Player.instance.setScale(scale);
		}

		//Each seconds = -2 pv
		life -= starving * Time.deltaTime;
		if(life<=0){
			life = 100;
			Player.instance.transform.position = startPosition.position;
		}

	}

	private  float Remap (this float value, float from1, float to1, float from2, float to2) {
		return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
	}

}
