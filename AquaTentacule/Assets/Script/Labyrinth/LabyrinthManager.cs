using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class LabyrinthManager : MonoBehaviour
{
	public static LabyrinthManager instance = null;
	private List<LabyTiles> tiles = new List<LabyTiles> ();
	private LabyTiles lastTiles;
	private ExitEnum playerExitTaken;
	private ExitEnum lastPlayerExit;
	private LabyTiles lastVisitedTile;
	private LabyTiles anteVisited;
	private int correctExitNeed;
	private int correctExitTakes;
	private ExitEnum next;

	void Awake ()
	{
		if (instance == null) {
			instance = this;
			LabyConfigTiles labyConfig = new LabyConfigTiles ();
			tiles.AddRange (labyConfig.getPreConfigTiles ());
			correctExitNeed = labyConfig.getCorrectExitNeeded ();
			lastTiles = labyConfig.getLastTile ();
			correctExitTakes = 0;
		} else if (instance != this) {
			Destroy (gameObject);
		}
		DontDestroyOnLoad (gameObject);
	}

	public void loadNextTile (string exitTag)
	{
		lastPlayerExit = playerExitTaken;
		playerExitTaken = (ExitEnum)Enum.Parse (typeof(ExitEnum), exitTag);
		if (playerExitTaken == next)
			correctExitTakes++;
		else
			correctExitTakes = 0;

		LabyTiles nextTile = (correctExitTakes == correctExitNeed)?lastTiles:getRandomFittingTile (tiles);
		if (correctExitTakes == correctExitNeed) 
			Debug.Log ("FINISH HIM");
	
		try {
			next = nextTile.exitList
				.OrderBy (e => Guid.NewGuid ())
					.Where (e => e != LabyTiles.getOppositeOf (playerExitTaken))
					.First ();
		} catch (InvalidOperationException e) {
			next = LabyTiles.getOppositeOf (playerExitTaken);
		}

		Debug.Log ("#### NEXT " + next + " comeFrom " + LabyTiles.getOppositeOf (playerExitTaken) + " CorrectHit : " + correctExitTakes);
		lastVisitedTile = anteVisited;
		anteVisited = nextTile;
		GameManager.instance.loadLevel (nextTile.sceneName);
	}

	public ExitEnum getplayerExit ()
	{
		return playerExitTaken;
	}

	private LabyTiles getRandomFittingTile (List<LabyTiles> tilesParam)
	{
		if (lastVisitedTile != null && lastPlayerExit.Equals (LabyTiles.getOppositeOf (playerExitTaken))) {
			return lastVisitedTile;
		}
		List<LabyTiles> tilestmp = tilesParam.Where (x => x.canFitWithThisExit (playerExitTaken)).ToList ();
		if (tilestmp.Count > 1) {
			tilestmp.Remove (anteVisited);
			Debug.Log ("Remove last " + anteVisited);
		}
		LabyTiles tile = tilestmp [UnityEngine.Random.Range (0, tilestmp.Count)];
		return tile;
	}

	void Update(){
		GameObject nextExit = GameObject.FindGameObjectWithTag(next+"");
		Vector2 nex = new Vector2(nextExit.transform.position.x,nextExit.transform.position.y);
		Vector2 player = GameManager.instance.getPlayerPosition();
	//	Debug.Log(next+"Spawn : "
	//	          +nex 
	//	          +" Distance with Player : "
	//	          + Vector2.Distance(player,nex));
	}

	public void OnDrawGizmos(){
		GameObject nextExit = GameObject.FindGameObjectWithTag(next+"");
		Gizmos.color = Color.blue;
		Gizmos.DrawLine(nextExit.transform.position, GameManager.instance.getPlayerPosition());
	}
}
