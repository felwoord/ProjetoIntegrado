using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {
	float t;
	private Transform rightHandTransform;
	private Vector3 turretPosition, rightHandPosition, arrowDirection, arrowDirectionVersor;
	private GameObject arrow;
	private float arrowDirectionMagnitude;
	public float arrowForce;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (t >= 2) {
			rightHandTransform = GameObject.Find("Right Hand").GetComponent<Transform>();
			rightHandPosition = new Vector3 (rightHandTransform.position.x, rightHandTransform.position.y, rightHandTransform.position.z);
			turretPosition = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
			arrow = Instantiate (Resources.Load ("Arrow")) as GameObject;
			arrow.transform.position = new Vector2 (turretPosition.x, turretPosition.y);
			arrowDirection = rightHandPosition - turretPosition;
			arrow.transform.LookAt (arrowDirection);
			if (arrow.transform.rotation.y > 1) {
				arrow.transform.Rotate (arrow.transform.rotation.x, -90, transform.rotation.z);
			} else {
				arrow.transform.Rotate (arrow.transform.rotation.x, -90, transform.rotation.z);
			}
			arrowDirectionMagnitude = arrowDirection.magnitude;
			arrowDirectionVersor = new Vector3 (arrowDirection.x / arrowDirectionMagnitude, arrowDirection.y / arrowDirectionMagnitude, arrowDirection.z / arrowDirectionMagnitude);
			arrow.GetComponent<Rigidbody2D> ().AddForce (arrowDirectionVersor * arrowForce);
			t = 0;
		}

		t += Time.deltaTime;
	}
}
