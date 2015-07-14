using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class LabyrinthManager : MonoBehaviour {


	public static LabyrinthManager instance = null;
	private List<LabyTiles> tiles = new List<LabyTiles>();
	private PlayerExit playerExitTaken;
	private PlayerExit lastPlayerExit;
	private LabyTiles lastVisitedTile;

	void Awake () {
		if (instance == null){
			instance = this;
			tiles.AddRange(new LabyConfigTiles().getPreConfigTiles());
		}
		else if(instance != this)
		{
			Destroy(gameObject);
		}
		
		DontDestroyOnLoad(gameObject);
	}

	public void loadNextTile(string exitTag){
		lastPlayerExit = playerExitTaken;
		playerExitTaken = (PlayerExit)Enum.Parse(typeof(PlayerExit), exitTag);
		LabyTiles nextTile = getRandomFittingTile(tiles);
		lastVisitedTile = nextTile;
		Debug.Log(nextTile.sceneName);
		GameManager.instance.loadLevel(nextTile.sceneName,adjustPlayerEntrancePostion());
	}

	private LabyTiles getRandomFittingTile(List<LabyTiles> tilesParam){
		if(lastVisitedTile != null && lastPlayerExit.Equals(lastVisitedTile.getOppositeOf(playerExitTaken))){
			Debug.Log ("Yes Man LastTiles FTW " + lastVisitedTile.sceneName);
			return lastVisitedTile;
		}
		List<LabyTiles> tilestmp = new List<LabyTiles>();
		tilestmp.AddRange(tilesParam);
		LabyTiles tile = tilestmp[UnityEngine.Random.Range(0,tilestmp.Count)] ;
		if(tile.canFitWithThisExit(playerExitTaken)){
			return tile;
		}else{
			tilestmp.Remove(tile);
			return getRandomFittingTile(tilestmp);
		}
	}



	//Not so good FixMe Please
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
