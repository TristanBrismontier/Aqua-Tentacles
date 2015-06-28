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
		player.transform.position = player.transform.position +startDeltaPosition;
		player.setVelocity(velocity);
	}

	public void saveInfo(Player player,Vector3 startPos){
		playerRotation = player.transform.rotation;
		tentaCount = player.countTenta;
		startDeltaPosition = startPos;
		velocity = player.getVelocity();
	}
}
