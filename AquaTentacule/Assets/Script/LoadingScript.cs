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
	public GameObject labyManager;

	public float scaleRatio;
	private float startScale; 

	public Camera camera;

	private Vector3 startPosition;
	void Awake () {
		if (GameManager.instance == null)
			Instantiate(gameManager);
		if(LabyrinthManager.instance == null)
			Instantiate(labyManager);

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


			float height = (2*Camera.main.orthographicSize)/2;
			float width = (height*Camera.main.aspect);

			Vector2 max = new Vector2(transform.position.x+width,transform.position.y+height);
			Vector2 min = new Vector2(transform.position.x-width,transform.position.y-height);

		
			Vector3 delta = target.position - GetComponent<UnityEngine.Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
			Vector3 destination = transform.position + delta;
			if(Mathf.Abs(delta.x) >= width || Mathf.Abs(delta.y) >= width){
				teleportToTarget();
				Debug.Log("TP");
			}

			Bounds bound = GameManager.instance.cameraBound;

			if(bound != null)
				destination = new Vector3(Mathf.Clamp(destination.x,bound.min.x+width,bound.max.x-width),Mathf.Clamp(destination.y,bound.min.y+height,bound.max.y-height),destination.z);
			//transform.position = new Vector3(destination.x, destination.y,transform.position.z);
			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);



if(background != null)
				background.transform.position = new Vector3(
				startPosition.x - (float)(transform.position.x/paralaxRatio),
				startPosition.y - (float)(transform.position.y/paralaxRatio),
				startPosition.z
				);
		}
	}

	private void teleportToTarget(){
		transform.position = new Vector3(target.position.x,target.position.y,transform.position.z);
	}

	public void setScale(float adj){
		scaleRatio = Mathf.SmoothDamp(scaleRatio,adj,ref yVelocity, smoothTime);
		camera.orthographicSize = startScale * scaleRatio;
	}


}
