using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour {
	private int shooter; //0 = p1, 1 = p2
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnBecameInvisible(){
		Destroy (gameObject);
	}

	void OnTriggerEnter2D(Collider2D infoCollider){
		if (infoCollider.tag == "Turret") {
			int x = infoCollider.GetComponent<Turret> ().getPositionX ();
			int y = infoCollider.GetComponent<Turret> ().getPositionY ();
			GameObject.Find ("Main Camera").GetComponent<SequencesGenerator> ().setBoolTurretFalse (x, y);
			if (shooter == 0) {
				GameObject.Find ("Left Hand").GetComponent<LeftHand> ().addToTurretsDestroyedP0 ();
			}
			if (shooter == 1) {
				GameObject.Find ("Right Hand").GetComponent<RightHand> ().addToTurretsDestroyedP1 ();
			}
			Destroy (infoCollider.gameObject);
		}

		if (infoCollider.tag == "Turret2") {
			GameObject.Find ("Main Camera").GetComponent<SequencesGenerator> ().setBoolTurret2False();
			if (shooter == 0) {
				GameObject.Find ("Left Hand").GetComponent<LeftHand> ().addToTurretsDestroyedP0 ();
			}
			if (shooter == 1) {
				GameObject.Find ("Right Hand").GetComponent<RightHand> ().addToTurretsDestroyedP1 ();
			}
			Destroy (infoCollider.gameObject);
		}
	}

	public void setShooter(int a){
		shooter = a;
	}
}
