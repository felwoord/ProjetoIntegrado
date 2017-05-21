using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AutoDestroy : MonoBehaviour {
	float timer;											//timer that colors stay "alive"
	public Image healthBar;									//health bar image on screen
	int x,y;												//position x and y of color

	void Start () {
		timer = 0;																//set timer at zero
		healthBar = GameObject.Find ("HealthBar").GetComponent<Image>();		//get health bar image on scene
	}

	void Update () {
		timer += Time.deltaTime;																			//increment timer 

		if (timer >= 5) {																					//if 5 seconds has passed
			GameObject.Find ("Main Camera").GetComponent<SequencesGenerator> ().setBoolBoardFalse (x, y);	//set position on BoolBoard on x,y position to false
			Destroy (gameObject);																			//destroy color
			//healthBar.fillAmount -= 0.05f;																	//remove 5% from health
			GameObject.Find("Main Camera").GetComponent<SequencesGenerator>().selfDestoyed += 1;			//increment 1 to self destroyed on SequencesGenerator
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
