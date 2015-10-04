using UnityEngine;
using System.Collections;

public class georges : MonoBehaviour {

	private bool final = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log("finish");
		GameManager.instance.player.canMove = false;
		if(final){
			StartCoroutine(showGeorgeBudbble());
			final=false;
		}

	}
	public IEnumerator showGeorgeBudbble(){
		StartCoroutine(alphabet.instance.showGeorgeBubble());
		yield return new WaitForSeconds(2.5f);
		StartCoroutine(alphabet.instance.showGeorgeBubble());
		yield return new WaitForSeconds(2.5f);
		StartCoroutine(alphabet.instance.showGeorgeBubble());
		yield return new WaitForSeconds(2.5f);
		Application.LoadLevel("Menu");
	}

}
