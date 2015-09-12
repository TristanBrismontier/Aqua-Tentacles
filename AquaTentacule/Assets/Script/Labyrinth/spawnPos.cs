using UnityEngine;
using System.Collections;

public class spawnPos : MonoBehaviour {
	//Debug information
	public void OnDrawGizmos(){
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position,2);
	}
}
