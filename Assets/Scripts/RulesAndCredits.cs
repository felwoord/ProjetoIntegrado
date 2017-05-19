using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArcadePUCCampinas;
using UnityEngine.SceneManagement;

public class RulesAndCredits : MonoBehaviour {


	void Start () {
		
	}

	void Update () {
		if (InputArcade.Apertou (0, EControle.AZUL)) {		//If Player 1 press VERDE button
			SceneManager.LoadScene ("MenuScene");			//Load Menu Scene
		}	
	}
}
