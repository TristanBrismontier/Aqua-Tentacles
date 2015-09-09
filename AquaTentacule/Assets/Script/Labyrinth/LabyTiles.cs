using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class LabyTiles {
	public string sceneName;
	public List<PlayerExit> exitList = new List<PlayerExit>();

	public LabyTiles(string name,params PlayerExit[] exitParamList){
		this.sceneName = name;
		for (int i = 0; i < exitParamList.Length; i++)
		{
			this.exitList.Add(exitParamList[i]);
		}
	}

	public bool canFitWithThisExit(PlayerExit exit){
		PlayerExit entrance = getOppositeOf(exit);
		return exitList.Contains(entrance);
	}
	public static PlayerExit getOppositeOf(PlayerExit exit){
		switch(exit){
			case PlayerExit.North: 
				return PlayerExit.South;
			case PlayerExit.East:
				return PlayerExit.West;
			case PlayerExit.South: 
				return PlayerExit.North;
			case PlayerExit.West: 
				return PlayerExit.East;
		}
		Debug.LogError("WTF! Enum Reverse ERROR " + exit);
		return PlayerExit.East;

	}
}
