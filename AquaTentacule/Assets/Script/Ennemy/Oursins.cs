using UnityEngine;
using System.Collections;

public class Oursins : MonoBehaviour {

	public double damage;

	void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "Player")
		{
			GameManager.instance.looseLife ((int)damage);
			SoundManager.instance.RandomizeSfx(SoundManager.instance.efxSource, GameManager.instance.deadSounds);
		}
	}
}