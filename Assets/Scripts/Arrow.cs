using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
			healthBar.fillAmount -= 0.05f;
			Destroy (gameObject);
		}
		if (infoCollider.tag == "Left Hand") {
			healthBar.fillAmount -= 0.05f;
			Destroy (gameObject);
		}
	}
}
