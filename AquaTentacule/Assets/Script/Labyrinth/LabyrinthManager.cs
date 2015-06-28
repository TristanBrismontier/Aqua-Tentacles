using UnityEngine;
using System;

public class LabyrinthManager : MonoBehaviour {


	public static LabyrinthManager instance = null;
	private enum PlayerExit {North, East, West, South};
	private PlayerExit playerExitTaken;

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

	public void loadNextTile(string exitTag){
		playerExitTaken = (PlayerExit)Enum.Parse(typeof(PlayerExit), exitTag);
		Debug.Log(playerExitTaken);
		GameManager.instance.loadLevel("FourDir",adjustPlayerEntrancePostion());
	}

	private Vector3 adjustPlayerEntrancePostion() {
		switch(playerExitTaken){
			case PlayerExit.North: 
				return new Vector3(0,-25,0);
			case PlayerExit.East:
				return new Vector3(-30,0,0);
			case PlayerExit.South: 
				return new Vector3(0,25,0);
			case PlayerExit.West: 
				return new Vector3(30,0,0);
		}
		return Vector3.zero;
	}

}
