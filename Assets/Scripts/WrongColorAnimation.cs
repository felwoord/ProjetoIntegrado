using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrongColorAnimation : MonoBehaviour {
	private float decaySprite = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.position = new Vector3 (transform.position.x, transform.position.y - decaySprite/50, transform.position.z);

		decaySprite += Time.deltaTime;

		if (decaySprite >= 0.82f) {
			Destroy (gameObject);
		}
		
	}
}
