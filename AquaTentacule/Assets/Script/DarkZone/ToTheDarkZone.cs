using UnityEngine;
using System.Collections;


public class ToTheDarkZone : MonoBehaviour
{
	public string destination;
	public string spawnTo;

	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.tag == "Player"){
			LabyrinthManager.instance.SetplayerExitTaken(spawnTo);
			GameManager.instance.loadLevel (destination);
		}
	}
}


//(tag)