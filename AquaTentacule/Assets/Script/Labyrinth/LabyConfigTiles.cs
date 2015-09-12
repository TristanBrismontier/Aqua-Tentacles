using System.Collections;
using System.Collections.Generic;


public class LabyConfigTiles{
	public List<LabyTiles> getPreConfigTiles(){
		List<LabyTiles> tiles = new List<LabyTiles>();
		tiles.Add( new LabyTiles("EWS",ExitEnum.East,ExitEnum.South,ExitEnum.West));
		tiles.Add( new LabyTiles("FourDir2",ExitEnum.North,ExitEnum.East,ExitEnum.South,ExitEnum.West));
		tiles.Add( new LabyTiles("EWN",ExitEnum.East,ExitEnum.West,ExitEnum.North));
		tiles.Add( new LabyTiles("NSE",ExitEnum.North,ExitEnum.East,ExitEnum.South));
		tiles.Add( new LabyTiles("NE",ExitEnum.North,ExitEnum.East));
		tiles.Add( new LabyTiles("WMachinesLumBleues",ExitEnum.West));
		tiles.Add( new LabyTiles("NSE2",ExitEnum.North,ExitEnum.East,ExitEnum.South));
		tiles.Add( new LabyTiles("NWEfondMachines",ExitEnum.North,ExitEnum.East,ExitEnum.West));
		return tiles;
	}

	public LabyTiles getLastTile(){
		// Todo : Configure the right tile.
		return new LabyTiles("NSE2");
	} 

	public int getCorrectExitNeeded(){
		return 5;
	}
}
