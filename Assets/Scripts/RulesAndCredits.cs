using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArcadePUCCampinas;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RulesAndCredits : MonoBehaviour {
	private bool sceneDelay;
	private float sceneDelayTimer;

	private float counter, flashSpeed;
	private bool toggle;
	private Text press1Text;


	void Start () {
		counter = 0;
		flashSpeed = 10;
		press1Text = GameObject.Find ("Press1Text").GetComponent<Text> ();
		sceneDelay = true;
		sceneDelayTimer = 0;
	}

	void Update () {
		if (!sceneDelay) {
			if (InputArcade.Apertou (0, EControle.AZUL)) {		//If Player 1 press AZUL button
				SceneManager.LoadScene ("MenuScene");			//Load Menu Scene
			}
			if (counter >= flashSpeed) {									
				counter = 0;												
				toggle = !toggle;											
				if (toggle) {		
					press1Text.enabled = true;
				} else {													
					press1Text.enabled = false;
				}
			} else {													
				counter++;												
			}
			
		}
		sceneDelayTimer += Time.deltaTime;
		if (sceneDelayTimer >= 0.25f) {
			sceneDelay = false;
		}
	}
}
