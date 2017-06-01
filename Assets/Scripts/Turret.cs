﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {
	float t;
	private Transform handTransform;
	private Vector3 turretPosition, handPosition, arrowDirection, arrowDirectionVersor;
	private GameObject arrow;
	private float arrowDirectionMagnitude;
	private float arrowForce;
	private float[] positionsY = new float[2]; 
	private float[] positionsX = new float[2];
	private float inicialPositionX;
	private float inicialPositionY;
	private float amplitude = 1.75f;
	private float period;
	private float attackDellay;
	private int globalX, globalY;

	// Use this for initialization
	void Start () {
		arrowForce = Random.Range (150, 200);
		period = Random.Range (1, 5);
		attackDellay = Random.Range (3, 10);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (inicialPositionX, inicialPositionY + amplitude * Mathf.Sin (period * Time.time), 0);
		if (t >= attackDellay) {
			int i = Random.Range (0, 2);
			if (i == 0) {
				handTransform = GameObject.Find("Left Hand").GetComponent<Transform>();
			}
			if (i == 1) {
				handTransform = GameObject.Find("Right Hand").GetComponent<Transform>();
			}
			handPosition = new Vector3 (handTransform.position.x, handTransform.position.y, handTransform.position.z);
			turretPosition = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
			arrow = Instantiate (Resources.Load ("Arrow")) as GameObject;
			arrow.transform.position = new Vector2 (turretPosition.x, turretPosition.y);
			arrowDirection = handPosition - turretPosition;
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

	public void setPosition(int x, int y){
		positionsX [0] = -8.0f;
		positionsX [1] = 8.0f;
		positionsY [0] = 1.75f;
		positionsY [1] = -1.75f;
		globalX = x;
		globalY = y;
		switch (x) {
		case 0:
			inicialPositionX = positionsX [0];
			gameObject.GetComponent<SpriteRenderer> ().flipX = true;
			break;
		case 1:
			inicialPositionX = positionsX [1];
			break;
		}
		switch (y) {
		case 0:
			inicialPositionY = positionsY [0];
			break;
		case 1:
			inicialPositionY = positionsY [1];
			break;
		}
	}

	public int getPositionX(){
		return globalX;
	}
	public int getPositionY(){
		return globalY;
	}

}
