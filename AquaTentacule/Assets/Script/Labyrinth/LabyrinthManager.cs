using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class LabyrinthManager : MonoBehaviour {


	public static LabyrinthManager instance = null;
	private List<LabyTiles> tiles = new List<LabyTiles>();
	private PlayerExit playerExitTaken;

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
		playerExitTaken = (PlayerExit)Enum.Parse(typeof(PlayerExit), exitTag);
		LabyTiles nextTile = getRandomFittingTile(tiles);
		GameManager.instance.loadLevel(nextTile.sceneName,adjustPlayerEntrancePostion());
	}

	private LabyTiles getRandomFittingTile(List<LabyTiles> tilesParam){
		List<LabyTiles> tilestmp = new List<LabyTiles>();
		tilestmp.AddRange(tilesParam);
		LabyTiles tile = tilestmp[UnityEngine.Random.Range(0,tilestmp.Count)] ;
		if(tile.canFitWithThisExit(playerExitTaken)){
			return tile;
		}else{
			Debug.Log (tile.sceneName + " => Exit " + playerExitTaken);
			tilestmp.Remove(tile);
			return getRandomFittingTile(tilestmp);
		}
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
