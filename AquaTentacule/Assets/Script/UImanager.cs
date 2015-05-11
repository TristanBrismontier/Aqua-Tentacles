using UnityEngine;
using System.Collections;

public class UImanager : MonoBehaviour {
	
	public GameObject StartMenu;
	
	public void NewGame() {
		Application.LoadLevel("Main");
	}
	
	public void Exit(){
		Application.Quit();
	}
	
}