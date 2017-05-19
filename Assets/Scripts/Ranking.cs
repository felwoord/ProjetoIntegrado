using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using ArcadePUCCampinas;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ranking : MonoBehaviour {
	private float[] ranking = new float[10];								//ranking array
	private float auxScore;													//aux
	public Text[] rankingText = new Text[10];								//ranking text on screen

	// Use this for initialization
	void Start () {
		ranking [0] = PlayerPrefs.GetFloat ("ranking 0");					//get ranking position 1 in playerpref
		ranking [1] = PlayerPrefs.GetFloat ("ranking 1");					//get ranking position 2 in playerpref
		ranking [2] = PlayerPrefs.GetFloat ("ranking 2");					//get ranking position 3 in playerpref
		ranking [3] = PlayerPrefs.GetFloat ("ranking 3");					//get ranking position 4 in playerpref
		ranking [4] = PlayerPrefs.GetFloat ("ranking 4");					//get ranking position 5 in playerpref
		ranking [5] = PlayerPrefs.GetFloat ("ranking 5");					//get ranking position 6 in playerpref
		ranking [6] = PlayerPrefs.GetFloat ("ranking 6");					//get ranking position 7 in playerpref
		ranking [7] = PlayerPrefs.GetFloat ("ranking 7");					//get ranking position 8 in playerpref
		ranking [8] = PlayerPrefs.GetFloat ("ranking 8");					//get ranking position 9 in playerpref
		ranking [9] = PlayerPrefs.GetFloat ("ranking 9");					//get ranking position 10 in playerpref


		auxScore = PlayerPrefs.GetFloat ("scoreP0");						//Get last Player 1 score from Player Prefs

		if(auxScore > ranking[9]){											//Check if it's better than the last in rank						
			ranking [9] = auxScore;											//get the position 
			Array.Sort	(ranking);											//sort the ranking (crescent)
			Array.Reverse (ranking);										//reverse the ranking (to be decrescent)
		}
			

		auxScore = PlayerPrefs.GetFloat ("scoreP1");						//Get last Player 2 score from Player Prefs

		if(auxScore > ranking[9]){											//Check if it's better than the last in rank	
			ranking [9] = auxScore;											//get the position 
			Array.Sort	(ranking);											//sort the ranking (crescent)
			Array.Reverse (ranking);										//reverse the ranking (to be decrescent)
		}

		foreach (float aux in ranking) {
			Debug.Log (aux);												//show ranking in Log
		}
			
		for (int i = 0; i <= 9; i++) {
			rankingText [i].text = ranking[i].ToString();
		}
		PlayerPrefs.SetFloat ("ranking 0", ranking[0]);						//set ranking position 1 in playerpref
		PlayerPrefs.SetFloat ("ranking 1", ranking[1]);						//set ranking position 2 in playerpref
		PlayerPrefs.SetFloat ("ranking 2", ranking[2]);						//set ranking position 3 in playerpref
		PlayerPrefs.SetFloat ("ranking 3", ranking[3]);						//set ranking position 4 in playerpref
		PlayerPrefs.SetFloat ("ranking 4", ranking[4]);						//set ranking position 5 in playerpref
		PlayerPrefs.SetFloat ("ranking 5", ranking[5]);						//set ranking position 6 in playerpref
		PlayerPrefs.SetFloat ("ranking 6", ranking[6]);						//set ranking position 7 in playerpref
		PlayerPrefs.SetFloat ("ranking 7", ranking[7]);						//set ranking position 8 in playerpref
		PlayerPrefs.SetFloat ("ranking 8", ranking[8]);						//set ranking position 8 in playerpref
		PlayerPrefs.SetFloat ("ranking 9", ranking[9]);						//set ranking position 10 in playerpref

		PlayerPrefs.Save();													//save player prefs
		//PlayerPrefs.DeleteAll ();											//delete all in player prefs

	
	}
	
	// Update is called once per frame
	void Update () {
		if (InputArcade.Apertou (0, EControle.AZUL)) {		//If Player 1 press VERDE button
			SceneManager.LoadScene ("MenuScene");			//Load Menu Scene
		}
	}
}
