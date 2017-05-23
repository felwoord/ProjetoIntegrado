using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour {

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
			Destroy (infoCollider.gameObject);
		}
	}
}
