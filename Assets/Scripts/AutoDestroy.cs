﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AutoDestroy : MonoBehaviour {
	float timer;											//timer that colors stay "alive"
	private Image healthBar;								//health bar image on screen
	int x,y;												//position x and y of color

	void Start () {
		timer = 0;																//set timer at zero
		healthBar = GameObject.Find ("HealthBar").GetComponent<Image>();		//get health bar image on scene
	}

	void Update () {
		transform.Rotate (Vector3.forward * -0.5f);

		timer += Time.deltaTime;																			//increment timer 

		if (timer >= 5) {																					//if 5 seconds has passed
			GameObject.Find ("Main Camera").GetComponent<SequencesGenerator> ().setBoolBoardFalse (x, y);	//set position on BoolBoard on x,y position to false

			GameObject spriteAfterDestroy = Instantiate (Resources.Load ("Animation/WrongColorAnimation")) as GameObject;	
			spriteAfterDestroy.transform.position = transform.position;
			spriteAfterDestroy.transform.rotation = transform.rotation;
			if (gameObject.tag == "Black") {
				spriteAfterDestroy.tag = "Black";
				spriteAfterDestroy.GetComponent<SpriteRenderer>().color = Color.black;
			}
			if (gameObject.tag == "Blue") {
				spriteAfterDestroy.tag = "Blue";
				spriteAfterDestroy.GetComponent<SpriteRenderer>().color = Color.blue;
			}
			if (gameObject.tag == "Yellow") {
				spriteAfterDestroy.tag = "Yellow";
				spriteAfterDestroy.GetComponent<SpriteRenderer>().color = Color.yellow;
			}
			if (gameObject.tag == "Green") {
				spriteAfterDestroy.tag = "Green";
				spriteAfterDestroy.GetComponent<SpriteRenderer>().color = Color.green;
			}
			if (gameObject.tag == "Red") {
				spriteAfterDestroy.tag = "Red";
				spriteAfterDestroy.GetComponent<SpriteRenderer>().color = Color.red;
			}
			if (gameObject.tag == "White") {
				spriteAfterDestroy.tag = "White";
				spriteAfterDestroy.GetComponent<SpriteRenderer>().color = Color.white;
			}

			Destroy (gameObject);																			//destroy color
			healthBar.fillAmount -= 0.05f;																//remove 5% from health
			GameObject.Find("Main Camera").GetComponent<SequencesGenerator>().AddSelfDestroyed();			//increment 1 to self destroyed on SequencesGenerator
			GameObject.Find("Left Hand").GetComponent<LeftHand>().SetMultiplierP0(1);						//set P1 multiplier to one
			GameObject.Find("Right Hand").GetComponent<RightHand>().SetMultiplierP1(1);						//set P2 multiplier to one

			if (healthBar.fillAmount == 0) {																//if health bar is zero
				GameObject.Find("Left Hand").GetComponent<LeftHand>().SaveStatsP0();						//Call function SaveStatsP0, from LeftHand Script
				GameObject.Find("Right Hand").GetComponent<RightHand>().SaveStatsP1();						//Call function SaveStatsP1, from RightHand Script
				GameObject.Find("Main Camera").GetComponent<SequencesGenerator>().SaveStatsSequenceGen();	//Call function SaveStatsSequenceGen, from SequenceGenerator Script
				HealthBarZero ();																			//call healthBarZero function
			}
		}
	}

	void HealthBarZero(){   						//function for when health bar comes to zero
		Debug.Log ("Game Over");					
		SceneManager.LoadScene ("PostGameScene");	//call post game scene when player dies
	}
		
	public void SetX(int aux){						//function to set x position
		x = aux;									
	}
	public int getX(){								//function to get x position
		return x;
	}
	public void SetY(int aux){						//function to set y position
		y = aux;
	}
	public int getY(){								//function to get y position
		return y;
	}
}
