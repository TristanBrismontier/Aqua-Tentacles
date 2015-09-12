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
		Collider2D cod = GetComponent<Collider2D>();
		GameManager.instance.initAntagonist();
		if(cod !=null)
			GameManager.instance.cameraBound = cod.bounds;
	}
		
	private Transform adjustPlayerEntrancePostion() {
		ExitEnum playerExit = LabyrinthManager.instance.getplayerExit() ; 
		switch(playerExit){
		case ExitEnum.North: 
			return southSpawn;
		case ExitEnum.East:
			return westSpawn;
		case ExitEnum.South:
			return northSpawn;
		case ExitEnum.West:
			return eastSpawn;
		}
		return southSpawn;
	}

	public void OnDrawGizmos(){
		Gizmos.color = Color.red;
		Collider2D cod = GetComponent<Collider2D>();
		if(cod != null)
		Gizmos.DrawWireCube(cod.bounds.center,cod.bounds.size);
	}

}
