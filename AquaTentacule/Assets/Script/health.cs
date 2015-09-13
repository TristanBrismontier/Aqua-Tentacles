using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class health : MonoBehaviour {
	public Slider Health;

	void Start () {
		Health.value = GameManager.instance.life;
	}

	void Update ()
	{
		Health.value = Mathf.MoveTowards(Health.value,  GameManager.instance.life, 1.0f);
	}
}
