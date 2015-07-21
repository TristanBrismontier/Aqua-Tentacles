using UnityEngine;
using System.Collections;

public class PlayerInfo  {
	public int tentaCount;
	public Vector3 startDeltaPosition;
	public Quaternion playerRotation;
	public Vector2 velocity;
	public bool playerHasTenta;

	public void restorePlayerInfo(Player player){
		player.setEvolution(tentaCount);
		player.transform.rotation = playerRotation;
		player.transform.position = new Vector3(startDeltaPosition.x,startDeltaPosition.y,player.transform.position.z);
		player.setVelocity(velocity);
	}

	public void saveInfo(Player player){
		playerRotation = player.transform.rotation;
		tentaCount = player.countTenta;
		startDeltaPosition = Vector3.zero;
		velocity = player.getVelocity();
	}
}
