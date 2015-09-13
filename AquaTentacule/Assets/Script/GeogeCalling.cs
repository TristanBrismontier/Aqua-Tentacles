using UnityEngine;
using System.Collections;

public class GeogeCalling : MonoBehaviour {
		
		public Transform target;
		public SpriteRenderer bubble; 
		
		void Start(){
			bubble.enabled = false;
		}

		// Update is called once per frame
		void Update () {
			transform.position = target.position;
			if (Input.GetKeyDown("space"))
				StartCoroutine(showGeorgeBubble());
		}
		
		public IEnumerator showGeorgeBubble(){
			bubble.enabled = true;
			yield return new WaitForSeconds(2);
			bubble.enabled = false;
		}
}
