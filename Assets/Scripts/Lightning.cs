using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour {
	private GameObject leftHand;
	private GameObject rightHand;
	private Vector3 leftHandPosition;
	private Vector3 rightHandPosition;
	private Vector3 lightningPosition;
	private float angleRotationZ;
	private Vector3 handsDistance;
	private float handsDistanceMagnitude;
	private float timerX, timerY, timerActive;
	private SpriteRenderer sp;

	// Use this for initialization
	void Start () {
		sp = GetComponent<SpriteRenderer> ();
		leftHand = GameObject.Find ("Left Hand");
		rightHand = GameObject.Find ("Right Hand");
		
	}
	
	// Update is called once per frame
	void Update () {
		leftHandPosition = leftHand.transform.position;
		leftHandPosition = new Vector3 (leftHandPosition.x, leftHandPosition.y, 0);
		rightHandPosition = rightHand.transform.position;
		rightHandPosition = new Vector3 (rightHandPosition.x, rightHandPosition.y,0);

		lightningPosition = new Vector3 ((leftHandPosition.x + rightHandPosition.x) / 2, (leftHandPosition.y + rightHandPosition.y) / 2);
		transform.position = lightningPosition;

		if (rightHandPosition.x > leftHandPosition.x) {
			angleRotationZ = Mathf.Atan2 (rightHandPosition.y - leftHandPosition.y, rightHandPosition.x - leftHandPosition.x);
			transform.eulerAngles = new Vector3 (transform.rotation.x, transform.rotation.y, (angleRotationZ * 90) + 90);
		} else {
			angleRotationZ = Mathf.Atan2 (-rightHandPosition.y + leftHandPosition.y, - rightHandPosition.x + leftHandPosition.x);
			transform.eulerAngles = new Vector3 (transform.rotation.x, transform.rotation.y, (angleRotationZ * 90) + 90);
		}

		//transform.eulerAngles = new Vector3 (transform.rotation.x * Time.time, transform.rotation.y, transform.rotation.z);
	
		handsDistance = rightHandPosition - leftHandPosition;
		handsDistanceMagnitude = handsDistance.magnitude;
		transform.localScale = new Vector3 (transform.localScale.x, handsDistanceMagnitude /10, transform.localScale.z);

		if (timerX >= 0.5) {
			sp.flipX = !sp.flipX;
			timerX = 0;
		}
		timerX++;
		if (timerY >= 0.25) {
			sp.flipY = !sp.flipY;
			timerY = 0;
		}
		timerY++;
		if (timerActive >= 8.33) {
			sp.enabled = !sp.enabled;
			timerActive = 0;
		}
		timerActive++;
	}
}
