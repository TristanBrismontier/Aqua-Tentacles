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
	public bool armor;


	public void restorePlayerInfo(Player player){
		player.armor = armor;
		player.restoreEvolutionTenta(tentaCount);
		player.restoreEvolutionEye(eyeCount);
		player.transform.rotation = playerRotation;
		player.transform.position = new Vector3(startDeltaPosition.x,startDeltaPosition.y,player.transform.position.z);
		player.setVelocity(velocity);
	}

	public void saveInfo(Player player){
		playerRotation = player.transform.rotation;
		tentaCount = player.countTenta;
		eyeCount = player.countEyes;
		startDeltaPosition = Vector3.zero;
		velocity = player.getVelocity();
		armor = player.armor;
	}
}