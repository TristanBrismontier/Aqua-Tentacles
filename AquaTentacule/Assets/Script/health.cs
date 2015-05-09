using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class health : MonoBehaviour {

	public Slider Health;
	public float HP;

	void Update ()
	{
		HP = GameManager.instance.life;
		Health.value = Mathf.MoveTowards(Health.value, HP, 1.0f);
	}
}
