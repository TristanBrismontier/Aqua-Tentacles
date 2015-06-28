using UnityEngine;
using System.Collections;

public class LoadingScript : MonoBehaviour {

	public float dampTime = 0.2f;
	private Vector3 velocity = Vector3.zero;
	public Transform target;
	public float smoothTime = 0.3F;
	private float yVelocity = 0.0F;
	public float paralaxRatio;

	public GameObject background;
	public GameObject gameManager;	

	public float scaleRatio;
	private float startScale; 

	public Camera camera;

	private Vector3 startPosition;
	void Awake () {
		if (GameManager.instance == null)
			Instantiate(gameManager);
	}

	void Start () {
		scaleRatio = 1;
		startScale = camera.orthographicSize;
		startPosition = new Vector3(0,0,20);
	}

	void Update () 
	{
		if (target)
		{
			Vector3 point = GetComponent<UnityEngine.Camera>().WorldToViewportPoint(target.position);
			Vector3 delta = target.position - GetComponent<UnityEngine.Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
			Vector3 destination = transform.position + delta;
			destination = new Vector3(Mathf.Clamp(destination.x,-61f,61),Mathf.Clamp(destination.y,-52,52),destination.z);
			//transform.position = new Vector3(destination.x, destination.y,transform.position.z);
			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);



			background.transform.position = new Vector3(
				startPosition.x - (float)(transform.position.x/paralaxRatio),
				startPosition.y - (float)(transform.position.y/paralaxRatio),
				startPosition.z
				);
		}
	}

	public void setScale(float adj){
		scaleRatio = Mathf.SmoothDamp(scaleRatio,adj,ref yVelocity, smoothTime);
		camera.orthographicSize = startScale * scaleRatio;
	}


}
