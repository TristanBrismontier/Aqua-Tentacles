﻿using UnityEngine;
using System.Collections;

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

	public int frameCount = 0;
	public AudioClip[] eatSounds;
	public GameObject[] foods;
	public GameObject Octopus;
	public GameObject FishEye;
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

	public void RespawnOctopus(){
		float x = (float)(Random.Range(-maxRange,maxRange));
		float y = (float)(Random.Range(-25,maxRange));
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		float deltaX = x  - player.transform.position.x;
		float deltaY = y  - player.transform.position.y;
		if( Mathf.Abs(deltaX) < 3.1f ){
			x=x+((deltaX<=0)?-3:3);
		}
		if( Mathf.Abs(deltaY) < 3.1f ){
			y=y+((deltaY<=0)?-3:3);
		}
		Instantiate(Octopus, new Vector3( x, y, 0), Quaternion.identity);
	}

	public void RespawnFishEye(){
		float x = (float)(Random.Range(-maxRange,maxRange));
		float y = (float)(Random.Range(10,maxRange));
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		float deltaX = x  - player.transform.position.x;
		float deltaY = y  - player.transform.position.y;
		if( Mathf.Abs(deltaX) < 3.1f ){
			x=x+((deltaX<=0)?-3:3);
		}
		if( Mathf.Abs(deltaY) < 3.1f ){
			y=y+((deltaY<=0)?-3:3);
		}
		Instantiate(FishEye, new Vector3( x, y, 0), Quaternion.identity);
	}

	public void eatFood(int nutritionFact){
		AddFood ();
		eat (nutritionFact);
	}
	public void eatOctoPus(int nutritionFact){
		RespawnOctopus();
		eat (nutritionFact);
	}
	public void eatFishEye(int nutritionFact){
		RespawnFishEye();
		eat (nutritionFact);
	}

	public void looseLife(int damage){
		eat (damage*-1);
	}

	private void eat (int nutritionFact){
		Debug.Log(nutritionFact);
		life = life + nutritionFact;
		if(nutritionFact <0)
			return;
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
			death();
		}

	}
	public void death(){
		life = 100;
		Player.instance.transform.position = startPosition.position;
	}

	private  float Remap (this float value, float from1, float to1, float from2, float to2) {
		return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
	}

}
