using System.Collections;
using System.Collections.Generic;


public class LabyConfigTiles{
	public List<LabyTiles> getPreConfigTiles(){
		List<LabyTiles> tiles = new List<LabyTiles>();

		//tiles.Add( new LabyTiles("FourDir",PlayerExit.North,PlayerExit.East,PlayerExit.South,PlayerExit.West));
		//tiles.Add( new LabyTiles("EW",PlayerExit.East,PlayerExit.West));
		tiles.Add( new LabyTiles("EWS",PlayerExit.East,PlayerExit.South,PlayerExit.West));
		tiles.Add( new LabyTiles("FourDir2",PlayerExit.North,PlayerExit.East,PlayerExit.South,PlayerExit.West));
		tiles.Add( new LabyTiles("EWN",PlayerExit.East,PlayerExit.West));
		tiles.Add( new LabyTiles("NSE",PlayerExit.North,PlayerExit.East,PlayerExit.South));
		tiles.Add( new LabyTiles("NE",PlayerExit.North,PlayerExit.East));
		//tiles.Add( new LabyTiles("NE",PlayerExit.North,PlayerExit.East));

		
		
		return tiles;
		
	}
}
