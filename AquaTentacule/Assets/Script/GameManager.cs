using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	public static GameManager instance = null;
	public int life;

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

	public void AddFood(){
		float x = (float)(Random.Range(-5,5)/10);
		float y = (float)(Random.Range(-5,5)/10);
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		float deltaX = x  - player.transform.position.x;
		float deltaY = y  - player.transform.position.y;
		if( Mathf.Abs(deltaX) < 2.1f ){
			x=x+((deltaX<=0)?-2:2);
		}
		if( Mathf.Abs(deltaY) < 2.1f ){
			y=y+((deltaY<=0)?-2:2);
		}
		Instantiate(food, new Vector3( x, y, 0), Quaternion.identity);
	}

	public void eatFood(int nutritionFact){
		life = life + nutritionFact;
		Debug.Log(life);
		AddFood ();
	}
}
