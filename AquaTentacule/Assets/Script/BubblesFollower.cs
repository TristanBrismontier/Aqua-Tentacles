using UnityEngine;
using System.Collections;

public class BubblesFollower : MonoBehaviour {


	public Transform target;
	
	// Update is called once per frame
	void Update () {
		transform.position = target.position;
	}
}
