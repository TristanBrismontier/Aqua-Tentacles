using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {

	public Transform westSpawn;
	public Transform northSpawn;
	public Transform eastSpawn;
	public Transform southSpawn;
	// Use this for initialization
	void Start () {
		GameManager.instance.initPlayerPosition(adjustPlayerEntrancePostion());
	}
	
	
	private Transform adjustPlayerEntrancePostion() {
		PlayerExit playerExit = LabyrinthManager.instance.getplayerExit() ; 
		Debug.Log(playerExit);
		switch(playerExit){
		case PlayerExit.North: 
			return southSpawn;
		case PlayerExit.East:
			return westSpawn;
		case PlayerExit.South:
			return northSpawn;
		case PlayerExit.West:
			return eastSpawn;
		}
		return southSpawn;
	}
}
