using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class health : MonoBehaviour {

	public Scrollbar HealthBar;

	void Damage (float value)
	{
		GameManager.instance.life -= value;
		HealthBar.size = GameManager.instance.life / 100f;
	}
}
