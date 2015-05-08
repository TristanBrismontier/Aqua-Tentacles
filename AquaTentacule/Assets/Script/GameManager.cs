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



	public GameObject food;

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

	public void initGame(){
		int foodNumber = Random.Range(minFood,maxFood);
		for(int i = 0 ; i<foodNumber; i++){
			AddFood();
		}
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
		Instantiate(food, new Vector3( x, y, 0), Quaternion.identity);
	}

	public void eatFood(int nutritionFact){
		life = life + nutritionFact;
		Debug.Log(life);
		AddFood ();
	}

	void Update () {
		//Life max = 150 pv
		if (life > 150) {
			life = 150;
		}
		//Each seconds = -2 pv
		life -= starving * Time.deltaTime;
		if(life<=0){
			GameObject player = GameObject.FindGameObjectWithTag("Player");
			player.transform.position = startPosition.position;
		}

	}

}
