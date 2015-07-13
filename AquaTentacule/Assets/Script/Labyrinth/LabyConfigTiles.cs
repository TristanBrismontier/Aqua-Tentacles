using System.Collections;
using System.Collections.Generic;


public class LabyConfigTiles{
	public List<LabyTiles> getPreConfigTiles(){
		List<LabyTiles> tiles = new List<LabyTiles>();

		tiles.Add( new LabyTiles("FourDir",PlayerExit.North,PlayerExit.East,PlayerExit.South,PlayerExit.East));
		tiles.Add( new LabyTiles("EW",PlayerExit.East,PlayerExit.West));



		return tiles;
		
	}
}
