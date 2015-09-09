using UnityEngine;
using System.Collections;

public class PlayerInfo  {
	public int tentaCount;
	public int eyeCount;
	public Vector3 startDeltaPosition;
	public Quaternion playerRotation;
	public Vector2 velocity;
	public bool playerHasTenta;
	public bool playerHasEye;

	public void restorePlayerInfo(Player player){
		player.restoreEvolution(tentaCount);
		player.restoreEvolutionEye(eyeCount);
		player.transform.rotation = playerRotation;
		Debug.Log("## StartPosition : " + startDeltaPosition);
		player.transform.position = new Vector3(startDeltaPosition.x,startDeltaPosition.y,player.transform.position.z);
		player.setVelocity(velocity);
		Debug.Log ("## Player "+player.transform.position);
	}

	public void saveInfo(Player player){
		playerRotation = player.transform.rotation;
		tentaCount = player.countTenta;
		eyeCount = player.countEyes;
		startDeltaPosition = Vector3.zero;
		velocity = player.getVelocity();
	}
}