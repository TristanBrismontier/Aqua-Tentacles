using UnityEngine;
using System.Collections;

public class LabyTileExitCheck : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log (other.gameObject.tag);
		if(other.gameObject.tag == "Player"){
			LabyrinthManager.instance.loadNextTile(tag);
		}
	}
}
