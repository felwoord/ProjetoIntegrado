using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Arrow : MonoBehaviour {

	private Image healthBar;
	// Use this for initialization
	void Start () {
		healthBar = GameObject.Find ("HealthBar").GetComponent<Image>();	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnBecameInvisible(){
		Destroy (gameObject);
	}
	void OnTriggerEnter2D(Collider2D infoCollider){
		if (infoCollider.tag == "Right Hand") {
			Collided (infoCollider);
		
		}
		if (infoCollider.tag == "Left Hand") {
			Collided (infoCollider);
		}
	}

	void Collided (Collider2D infoCollider){
		healthBar.fillAmount -= 0.01f;
		if (infoCollider.tag == "Left Hand") {
			infoCollider.GetComponent<LeftHand> ().addToHitsP0 ();
		}
		if (infoCollider.tag == "Right Hand") {
			infoCollider.GetComponent<RightHand> ().addToHitsP1 ();
		}
		Destroy (gameObject);
		if (healthBar.fillAmount == 0) {																//if health bar is zero
			GameObject.Find("Left Hand").GetComponent<LeftHand>().SaveStatsP0();						//Call function SaveStatsP0, from LeftHand Script
			GameObject.Find("Right Hand").GetComponent<RightHand>().SaveStatsP1();						//Call function SaveStatsP1, from RightHand Script
			GameObject.Find("Main Camera").GetComponent<SequencesGenerator>().SaveStatsSequenceGen();	//Call function SaveStatsSequenceGen, from SequenceGenerator Script
			SceneManager.LoadScene ("PostGameScene");
		}
	}
}
