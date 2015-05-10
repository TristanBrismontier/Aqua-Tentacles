using UnityEngine;
using System.Collections;

public class LoadingScript : MonoBehaviour {

	public float dampTime = 0.2f;
	private Vector3 velocity = Vector3.zero;
	public Transform target;
	public float paralaxRatio;

	public GameObject background;
	public GameObject gameManager;	

	private Vector3 startPosition;
	void Awake () {
		if (GameManager.instance == null)
			Instantiate(gameManager);
	}

	void Start () {
		startPosition = new Vector3(0,0,20);
	}

	void Update () 
	{
		if (target)
		{
			Vector3 point = GetComponent<UnityEngine.Camera>().WorldToViewportPoint(target.position);
			Vector3 delta = target.position - GetComponent<UnityEngine.Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
			Vector3 destination = transform.position + delta;
			destination = new Vector3(Mathf.Clamp(destination.x,-61f,61),Mathf.Clamp(destination.y,-47,52),destination.z);
			//transform.position = new Vector3(destination.x, destination.y,transform.position.z);
			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);



			background.transform.position = new Vector3(
				startPosition.x - (float)(transform.position.x/paralaxRatio),
				startPosition.y - (float)(transform.position.y/paralaxRatio),
				startPosition.z
				);
		}
	}
}
