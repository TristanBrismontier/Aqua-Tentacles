using UnityEngine;
using System.Collections;

public class menu : MonoBehaviour {
	public Texture MenuBG1; 

	// Use this for initialization
	void OnGUI () {
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), MenuBG1, ScaleMode.ScaleAndCrop, true, 0.0F);
	}

	void Update (){
		if (Input.GetKeyDown (KeyCode.Escape))
			Application.Quit();
		if (Input.anyKey){
			Application.LoadLevel("second2");
		}
	}
}
