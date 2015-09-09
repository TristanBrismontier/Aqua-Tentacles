using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class LabyrinthManager : MonoBehaviour {
	
	public static LabyrinthManager instance = null;
	private List<LabyTiles> tiles = new List<LabyTiles>();
	private LabyTiles lastTiles;
	private PlayerExit playerExitTaken;
	private PlayerExit lastPlayerExit;
	private LabyTiles lastVisitedTile;
	private LabyTiles anteVisited;

	private int correctExitNeed;
	private int correctExitTakes;
	private PlayerExit next;

	void Awake () {
		if (instance == null){
			Debug.Log ("New Instance Laby");
			instance = this;
			LabyConfigTiles labyConfig = new LabyConfigTiles();
			tiles.AddRange(labyConfig.getPreConfigTiles());
			correctExitNeed = labyConfig.getCorrectExitNeeded();
			lastTiles = labyConfig.getLastTile();
			correctExitTakes = 0;
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
		if(playerExitTaken == next)
			correctExitTakes++;
		else
			correctExitTakes = 0;

		LabyTiles nextTile;
		if(correctExitTakes == correctExitNeed)
			nextTile = lastTiles;
		else
			nextTile = getRandomFittingTile(tiles);

		try{
			next = nextTile.exitList
				.OrderBy(e => Guid.NewGuid())
					.Where(e => e != playerExitTaken)
					.First();
		}catch(InvalidOperationException e){
			Debug.LogError("### Have to Catch");
			next= playerExitTaken;
		}


		Debug.Log ("#### NEXT "+ next);
		lastVisitedTile = anteVisited;
		anteVisited = nextTile;
		GameManager.instance.loadLevel(nextTile.sceneName);

	}

	public PlayerExit getplayerExit(){
		return playerExitTaken;
	}

	private LabyTiles getRandomFittingTile(List<LabyTiles> tilesParam){
		if(lastVisitedTile != null && lastPlayerExit.Equals(lastVisitedTile.getOppositeOf(playerExitTaken))){
			return lastVisitedTile;
		}
		List<LabyTiles> tilestmp = tilesParam.Where( x => x.canFitWithThisExit(playerExitTaken)).ToList();
		if(tilestmp.Count>1){
			tilestmp.Remove(anteVisited);
			Debug.Log("Remove last " +anteVisited);
		}
		LabyTiles tile = tilestmp[UnityEngine.Random.Range(0,tilestmp.Count)] ;



		return tile;
	}
}
