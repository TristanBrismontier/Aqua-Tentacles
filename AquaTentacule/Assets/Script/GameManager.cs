using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
	
	public static GameManager instance = null;
	public float life;
	public float starving = 2.0f;
	public Transform startPosition;
	public int minFood;
	public int maxFood;
	public int octopus;
	public int FishCount;
	public int maxRange;
	public int limiteVieHeartbeat;
	public bool debug;
	public PlayerInfo playerInfo = new PlayerInfo ();
	public Bounds cameraBound;
	public GameObject[] foods;
	public GameObject Octopus;
	public GameObject FishEye;
	public GameObject bubbleExplosionPlop;
	private Player player;
	private List<GameObject> instanciatesGameObjects = new List<GameObject> ();
	private bool canDie;
	private bool isPlayingSound;

	void Awake ()
	{
		if (instance == null) {
			Debug.Log ("New Instance GM");
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}
		DontDestroyOnLoad (gameObject);
	}

	public void Start ()
	{
		isPlayingSound = false;
		initGame ();
	}

	public void setPlayer (Player _player)
	{
		player = _player;
		playerInfo.restorePlayerInfo (player);
		startPosition.position = new Vector3 (player.transform.position.x, player.transform.position.y, player.transform.position.z);
	}

	public void initPlayerPosition (Transform position)
	{
		playerInfo.startDeltaPosition = position.position;
	}

	public void initGame ()
	{
		playerInfo.playerHasTenta = false;
		canDie = true;
		playerInfo.tentaCount = 0;
		initAntagonist ();

	}

	public void initAntagonist ()
	{
		if (!debug) {
			Debug.Log ("Deb " + debug);
			int foodNumber = Random.Range (minFood, maxFood);
			for (int i = 0; i<foodNumber; i++) {
				AddFood ();
			}
		
			for (int i = 0; i<octopus; i++) {
				RespawnOctopus ();
			}
		
			for (int i = 0; i<FishCount; i++) {
				RespawnFishEye ();
			}
		}
	}

	public void AddFood ()
	{
		GameObject foodChoice = foods [Random.Range (0, foods.Length)];
		GameObject go = Instantiate (foodChoice, getLimitedRandomPos (), Quaternion.identity)as GameObject;
		instanciatesGameObjects.Add (go);
	}
	
	public void RespawnOctopus ()
	{
		GameObject go = Instantiate (Octopus, getLimitedRandomPos (), Quaternion.identity) as GameObject;
		instanciatesGameObjects.Add (go);
	}

	public void RespawnFishEye ()
	{
		GameObject go = Instantiate (FishEye, getLimitedRandomPos (), Quaternion.identity) as GameObject;
		instanciatesGameObjects.Add (go);
	}

	private Vector3 getLimitedRandomPos ()
	{
		float x = (float)(Random.Range (-maxRange, maxRange));
		float y = (float)(Random.Range (-maxRange, maxRange));
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		float deltaX = x - player.transform.position.x;
		float deltaY = y - player.transform.position.y;
		if (Mathf.Abs (deltaX) < 3.1f) {
			x = x + ((deltaX <= 0) ? -3 : 3);
		}
		if (Mathf.Abs (deltaY) < 3.1f) {
			y = y + ((deltaY <= 0) ? -3 : 3);
		}
		return new Vector3 (x, y, 0);
	}

	public void eatFood (int nutritionFact)
	{
		AddFood ();
		eat (nutritionFact);
		SoundManager.instance.RandomizeSfx (SoundManager.instance.efxSource, SoundManager.instance.eatSounds);
	}

	public void eatOctoPus (int nutritionFact)
	{
		RespawnOctopus ();
		player.spawnTenta ();
		playerInfo.playerHasTenta = true;
		eat (nutritionFact);
	}

	public void eatFishEye (int nutritionFact)
	{
		RespawnFishEye ();
		player.spawnEye ();
		playerInfo.playerHasEye = true;
		eat (nutritionFact);
	}

	public void looseLife (int damage)
	{
		eat (damage * -1);
		player.Hurt ();
	}

	private void eat (int nutritionFact)
	{
		life = life + nutritionFact;
		if (nutritionFact > 0) {
			player.Eat ();
		}
	}

	void Update ()
	{
		//Life max = 150 pv
		if (life > 150) {
			//life = 150;
		}
		//Each seconds = -2 pv
		life -= starving * Time.deltaTime;
		if (life <= 0) {
			death ();
		}

		if (life <= limiteVieHeartbeat && !isPlayingSound) {
			isPlayingSound = true;
			SoundManager.instance.PlaySingleLoop (SoundManager.instance.heartbeatSound, SoundManager.instance.interfaceSource, 0.3f);
			StartCoroutine (SoundManager.instance.FadeIn (SoundManager.instance.interfaceSource, 0.3f));
		} else if (life > limiteVieHeartbeat && isPlayingSound) {
			isPlayingSound = false;
			StartCoroutine (SoundManager.instance.FadeOut (SoundManager.instance.interfaceSource, 0.3f));
		}
	}
	
	public void loadLevel (string name)
	{
		playerInfo.saveInfo (player);
		Application.LoadLevel (name);
	}

	public void death ()
	{
		if (canDie) {
			canDie = false;
			life = 100000;
			SoundManager.instance.RandomizeSfx (SoundManager.instance.efxSource, SoundManager.instance.deadSounds);
			GameObject playerGO = GameObject.FindGameObjectWithTag ("Player");
			playerGO.transform.localScale = new Vector3 (0f, 0f, 0f);
			GameObject buble = Instantiate (bubbleExplosionPlop, new Vector3 (playerGO.transform.position.x, playerGO.transform.position.y, playerGO.transform.position.z - 1), playerGO.transform.rotation) as GameObject;
			StartCoroutine (deadPlayer (buble));
		}
	}

	public IEnumerator deadPlayer (GameObject buble)
	{
		yield return new WaitForSeconds (2);
	
		Destroy (buble);
		GameObject playerGO = GameObject.FindGameObjectWithTag ("Player");
		playerGO.SetActive (true);
		playerGO.transform.localScale = new Vector3 (2.3f, 2.3f, 2.3f);
		player.transform.position = startPosition.position;
		yield return new WaitForSeconds (0.5f);
		player.resetPlayer ();
		foreach (GameObject gobj in instanciatesGameObjects) {
			Destroy (gobj);
		}
		initGame ();
	}
}
