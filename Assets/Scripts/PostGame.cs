using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArcadePUCCampinas;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PostGame : MonoBehaviour {
	float scoreP0;								//Player 1 score
	public Text scoreP0Text;					//Player 1 score text
	float scoreP1;								//Player 2 score
	public Text scoreP1Text;					//Player 2 score text
	float missesP0;								//Player 1 misses
	public Text missesP0Text;					//Player 1 misses text
	float missesP1;								//Player 2 misses
	public Text missesP1Text;					//Player 2 misses text
	float rightsP0;								//Player 1 rights
	public Text rightsP0Text;					//Player 1 rights text
	float rightsP1;								//Player 2 rights
	public Text rightsP1Text;					//Player 2 tights text
	float maxMultiplierP0;						//Player 1 max mult.
	public Text maxMultiplierP0Text;			//Player 1 max mult. text
	float maxMultiplierP1;						//Player 2 max mult.
	public Text maxMultiplierP1Text;			//Player 2 max mult. text
	float scoreTotal;							//Total Score
	public Text scoreTotalText;					//Total Score Text
	int selfDestroyed;							//number of self destroyed colors
	public Text selfDestroyedText;				//self destroyed colors text

	void Start () {
		scoreP0 = PlayerPrefs.GetFloat ("scoreP0");						//Get Player 1 score from Player Prefs
		scoreP1 = PlayerPrefs.GetFloat ("scoreP1");						//Get Player 2 score from Player Prefs
		missesP0 = PlayerPrefs.GetFloat ("missesP0");					//Get Player 1 misses from Player Prefs
		missesP1 = PlayerPrefs.GetFloat ("missesP1");					//Get Player 2 misses from Player Prefs
		rightsP0 = PlayerPrefs.GetFloat ("rightsP0");					//Get Player 1 rights from Player Prefs
		rightsP1 = PlayerPrefs.GetFloat ("rightsP1");					//Get Player 2 rights from Player Prefs
		maxMultiplierP0 = PlayerPrefs.GetFloat ("maxMultiplierP0");		//Get Player 1 max mult. from Player Prefs
		maxMultiplierP1 = PlayerPrefs.GetFloat ("maxMultiplierP1");		//Get Player 2 max mult. from Player Prefs
		selfDestroyed = PlayerPrefs.GetInt ("selfDestroyed");		 	//Get number of self destroyed colors from Player Prefs
				
		scoreTotal = scoreP0 + scoreP1;							//Score total = score P1 + Score Player 2

		scoreP0Text.text = scoreP0.ToString ();  				//Set Player 1 score on screen
		scoreP1Text.text = scoreP1.ToString ();					//Set Player 2 score on screen
		missesP0Text.text = missesP0.ToString();				//Set Player 1 misses on screen
		missesP1Text.text = missesP1.ToString();				//Set Player 2 misses on screen
		rightsP0Text.text = rightsP0.ToString();				//Set Player 1 rights on screen
		rightsP1Text.text = rightsP1.ToString();				//Set Player 2 rights on screen
		maxMultiplierP0Text.text = maxMultiplierP0.ToString();	//Set Player 1 Max Mult. on screen
		maxMultiplierP1Text.text = maxMultiplierP1.ToString();	//Set Player 2 Max Mult. on screen
		scoreTotalText.text = scoreTotal.ToString ();			//Set Total score on screen
		selfDestroyedText.text = selfDestroyed.ToString();		//Set selfDestroyed on screen

		
	}

	void Update () {

		if (InputArcade.Apertou (0, EControle.AZUL)) {		//If Player 1 press VERDE button
			SceneManager.LoadScene ("RankingScene");		//Load Ranking Scene
		}
	}
}
