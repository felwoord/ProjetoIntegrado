using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArcadePUCCampinas;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PostGame : MonoBehaviour {
	private float scoreP0;								//Player 1 score
	private Text scoreP0Text;					//Player 1 score text
	private float scoreP1;								//Player 2 score
	private Text scoreP1Text;					//Player 2 score text
	private float missesP0;								//Player 1 misses
	private Text missesP0Text;					//Player 1 misses text
	private float missesP1;								//Player 2 misses
	private Text missesP1Text;					//Player 2 misses text
	private float rightsP0;								//Player 1 rights
	private Text rightsP0Text;					//Player 1 rights text
	private float rightsP1;								//Player 2 rights
	private Text rightsP1Text;					//Player 2 tights text
	private float maxMultiplierP0;						//Player 1 max mult.
	private Text maxMultiplierP0Text;			//Player 1 max mult. text
	private float maxMultiplierP1;						//Player 2 max mult.
	private Text maxMultiplierP1Text;			//Player 2 max mult. text
	private float scoreTotal;							//Total Score
	private Text scoreTotalText;					//Total Score Text
	private int selfDestroyed;							//number of self destroyed colors
	private Text selfDestroyedText;				//self destroyed colors text
	private int turretsP0;
	private Text turretsP0Text;
	private int turretsP1;
	private Text turretsP1Text;
	private int hitsP0;									//Player 1 hits taken by turrets
	private Text hitsP0Text;
	private int hitsP1;
	private Text hitsP1Text;

	void Start () {
		maxMultiplierP0Text = GameObject.Find ("MaxMultP0Text").GetComponent<Text> ();
		missesP0Text = GameObject.Find ("MissesP0Text").GetComponent<Text> ();
		turretsP0Text = GameObject.Find ("TurretsP0Text").GetComponent<Text> ();
		hitsP0Text = GameObject.Find ("HitsP0Text").GetComponent<Text> ();
		rightsP0Text = GameObject.Find ("RightsP0Text").GetComponent<Text> ();
		scoreP0Text = GameObject.Find ("ScoreP0Text").GetComponent<Text> ();

		maxMultiplierP1Text = GameObject.Find ("MaxMultP1Text").GetComponent<Text> ();
		missesP1Text = GameObject.Find ("MissesP1Text").GetComponent<Text> ();
		turretsP1Text = GameObject.Find ("TurretsP1Text").GetComponent<Text> ();
		hitsP1Text = GameObject.Find ("HitsP1Text").GetComponent<Text> ();
		rightsP1Text = GameObject.Find ("RightsP1Text").GetComponent<Text> ();
		scoreP1Text = GameObject.Find ("ScoreP1Text").GetComponent<Text> ();

		selfDestroyedText = GameObject.Find ("SelfDestroyedText").GetComponent<Text> ();

		turretsP0 = PlayerPrefs.GetInt ("turretsDestroyedP0");
		turretsP1 = PlayerPrefs.GetInt ("turretsDestroyedP1");
		hitsP0 = PlayerPrefs.GetInt ("hitsP0");
		hitsP1 = PlayerPrefs.GetInt ("hitsP1");
		scoreP0 = PlayerPrefs.GetFloat	("scoreP0");					//Get Player 1 score from Player Prefs
		scoreP1 = PlayerPrefs.GetFloat ("scoreP1");						//Get Player 2 score from Player Prefs
		missesP0 = PlayerPrefs.GetFloat ("missesP0");					//Get Player 1 misses from Player Prefs
		missesP1 = PlayerPrefs.GetFloat ("missesP1");					//Get Player 2 misses from Player Prefs
		rightsP0 = PlayerPrefs.GetFloat ("rightsP0");					//Get Player 1 rights from Player Prefs
		rightsP1 = PlayerPrefs.GetFloat ("rightsP1");					//Get Player 2 rights from Player Prefs
		maxMultiplierP0 = PlayerPrefs.GetFloat ("maxMultiplierP0");		//Get Player 1 max mult. from Player Prefs
		maxMultiplierP1 = PlayerPrefs.GetFloat ("maxMultiplierP1");		//Get Player 2 max mult. from Player Prefs
		selfDestroyed = PlayerPrefs.GetInt ("selfDestroyed");		 	//Get number of self destroyed colors from Player Prefs
				
		//scoreTotal = scoreP0 + scoreP1;							//Score total = score P1 + Score Player 2

		scoreP0Text.text = scoreP0.ToString ();  				//Set Player 1 score on screen
		scoreP1Text.text = scoreP1.ToString ();					//Set Player 2 score on screen
		missesP0Text.text = missesP0.ToString();				//Set Player 1 misses on screen
		missesP1Text.text = missesP1.ToString();				//Set Player 2 misses on screen
		rightsP0Text.text = rightsP0.ToString();				//Set Player 1 rights on screen
		rightsP1Text.text = rightsP1.ToString();				//Set Player 2 rights on screen
		maxMultiplierP0Text.text = maxMultiplierP0.ToString();	//Set Player 1 Max Mult. on screen
		maxMultiplierP1Text.text = maxMultiplierP1.ToString();	//Set Player 2 Max Mult. on screen
		turretsP0Text.text = turretsP0.ToString();
		turretsP1Text.text = turretsP1.ToString();
		hitsP0Text.text = hitsP0.ToString ();
		hitsP1Text.text = hitsP1.ToString ();
		//scoreTotalText.text = scoreTotal.ToString ();			//Set Total score on screen
		selfDestroyedText.text = selfDestroyed.ToString();		//Set selfDestroyed on screen

		
	}

	void Update () {

		if (InputArcade.Apertou (0, EControle.AZUL)) {		//If Player 1 press VERDE button
			SceneManager.LoadScene ("GetNickScene");		//Load Ranking Scene
		}
	}
}
