using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class health : MonoBehaviour {

	public Slider Health;
	private float HP;

	void Start () {
		Health.value = GameManager.instance.life;
	}

	void Update ()
	{
		HP = GameManager.instance.life;
		Health.value = Mathf.MoveTowards(Health.value, HP, 1.0f);
	}
}
