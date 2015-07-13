using UnityEngine;
using System.Collections;

public class LabyTileExitCheck : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.tag == "Player"){
			LabyrinthManager.instance.loadNextTile(tag);
		}
	}
}
