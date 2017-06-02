using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret2 : MonoBehaviour {
	float t;
	private Transform leftHandTransform;
	private Vector3 turretPosition, leftHandPosition, arrow0Direction, arrow0DirectionVersor;
	private GameObject arrow0;
	private float arrow0DirectionMagnitude;
	private float arrow0Force;
	private Transform rightHandTransform;
	private Vector3 rightHandPosition, arrow1Direction, arrow1DirectionVersor;
	private GameObject arrow1;
	private float arrow1DirectionMagnitude;
	private float arrow1Force;
	private float amplitude = 8;
	private float period;
	private float attackDellay;

	// Use this for initialization
	void Start () {
		arrow0Force = Random.Range (150, 200);
		arrow1Force = Random.Range (150, 200);
		period = Random.Range (1, 3);
		attackDellay = Random.Range (3, 10);
	}

	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (amplitude * Mathf.Sin (period * Time.time), 4.6f, 0);
		if (t >= attackDellay) {
			
			leftHandTransform = GameObject.Find("Left Hand").GetComponent<Transform>();
			leftHandPosition = new Vector3 (leftHandTransform.position.x, leftHandTransform.position.y, leftHandTransform.position.z);
			turretPosition = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
			arrow0 = Instantiate (Resources.Load ("Arrow")) as GameObject;
			arrow0.transform.position = new Vector2 (turretPosition.x, turretPosition.y);
			arrow0Direction = leftHandPosition - turretPosition;
			arrow0.transform.LookAt (arrow0Direction);
			arrow0.transform.Rotate (arrow0.transform.rotation.x, -90, transform.rotation.z);
			arrow0DirectionMagnitude = arrow0Direction.magnitude;
			arrow0DirectionVersor = new Vector3 (arrow0Direction.x / arrow0DirectionMagnitude, arrow0Direction.y / arrow0DirectionMagnitude, arrow0Direction.z / arrow0DirectionMagnitude);
			arrow0.GetComponent<Rigidbody2D> ().AddForce (arrow0DirectionVersor * arrow0Force);

			rightHandTransform = GameObject.Find("Right Hand").GetComponent<Transform>();
			rightHandPosition = new Vector3 (rightHandTransform.position.x, rightHandTransform.position.y, rightHandTransform.position.z);
			turretPosition = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
			arrow1 = Instantiate (Resources.Load ("Arrow")) as GameObject;
			arrow1.transform.position = new Vector2 (turretPosition.x, turretPosition.y);
			arrow1Direction = rightHandPosition - turretPosition;
			arrow1.transform.LookAt (arrow1Direction);
			arrow1.transform.Rotate (arrow1.transform.rotation.x, -90, transform.rotation.z);
			arrow1DirectionMagnitude = arrow1Direction.magnitude;
			arrow1DirectionVersor = new Vector3 (arrow1Direction.x / arrow1DirectionMagnitude, arrow1Direction.y / arrow1DirectionMagnitude, arrow1Direction.z / arrow1DirectionMagnitude);
			arrow1.GetComponent<Rigidbody2D> ().AddForce (arrow1DirectionVersor * arrow1Force);


			t = 0;
		}




		t += Time.deltaTime;
	}
		
}
