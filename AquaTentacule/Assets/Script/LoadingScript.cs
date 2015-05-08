using UnityEngine;
using System.Collections;

public class LoadingScript : MonoBehaviour {

	public GameObject gameManager;	
	void Awake () {
		if (GameManager.instance == null)
			Instantiate(gameManager);
	}
}
