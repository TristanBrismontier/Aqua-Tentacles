using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class LabyTiles {
	public string sceneName;
	public List<ExitEnum> exitList = new List<ExitEnum>();

	public LabyTiles(string name,params ExitEnum[] exitParamList){
		this.sceneName = name;
		for (int i = 0; i < exitParamList.Length; i++)
		{
			this.exitList.Add(exitParamList[i]);
		}
	}

	public bool canFitWithThisExit(ExitEnum exit){
		ExitEnum entrance = getOppositeOf(exit);
		return exitList.Contains(entrance);
	}
	public static ExitEnum getOppositeOf(ExitEnum exit){
		switch(exit){
			case ExitEnum.North: 
				return ExitEnum.South;
			case ExitEnum.East:
				return ExitEnum.West;
			case ExitEnum.South: 
				return ExitEnum.North;
			case ExitEnum.West: 
				return ExitEnum.East;
		}
		Debug.LogError("WTF! Enum Reverse ERROR " + exit);
		return ExitEnum.East;

	}
}
