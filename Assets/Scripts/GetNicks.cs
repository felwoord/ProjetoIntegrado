using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AssemblyCSharp;
using ArcadePUCCampinas;
using UnityEngine.SceneManagement;

public class GetNicks : MonoBehaviour {
	private Text[] lettersP0Text = new Text [3];
	private Text[] lettersP1Text = new Text [3];
	private Text scoreP0Text, congratsP0Text;
	private Text scoreP1Text, congratsP1Text;
	private float scoreP0;
	private float scoreP1;
	private Rank ranking;
	private int aux;
	Vector2 inputP0, inputP1;
	float timerP0;														  	//timer to make selecting for Player 1 better
	bool delayP0 = true;													//delay to be able to Player 1 to change letter	
	float timerP1;														  	//timer to make selecting for Player 2 better
	bool delayP1 = true;													//delay to be able to Player 2 to change letter	
	private int i = 0;
	private int j = 0;
	private int k = 0;
	private int l = 0;
	private int currentLetterP0 = 0;
	private int currentLetterP1 = 0;
	private char[] alphabet = new char[] {'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R',  //26 letters + 1 space + 10 numbers => size = 37
		'S','T','U','V','W','X','Y','Z',' ','0','1','2','3','4','5','6','7','8','9'};
	private bool sceneDelay;
	private float sceneDelayTimer;

	private float counter, flashSpeed;
	private bool toggle;
	private Text press1Text;



	void Start () {
		sceneDelay = true;
		sceneDelayTimer = 0;

		lettersP0Text[0] = GameObject.Find ("Letter1P0").GetComponent<Text> ();
		lettersP0Text[1] = GameObject.Find ("Letter2P0").GetComponent<Text> ();
		lettersP0Text[2] = GameObject.Find ("Letter3P0").GetComponent<Text> ();
		lettersP1Text[0] = GameObject.Find ("Letter1P1").GetComponent<Text> ();
		lettersP1Text[1] = GameObject.Find ("Letter2P1").GetComponent<Text> ();
		lettersP1Text[2] = GameObject.Find ("Letter3P1").GetComponent<Text> ();
		scoreP0Text = GameObject.Find ("ScoreP0").GetComponent<Text> ();
		scoreP1Text = GameObject.Find ("ScoreP1").GetComponent<Text> ();
		congratsP0Text = GameObject.Find ("CongratzP0").GetComponent<Text> ();
		congratsP1Text = GameObject.Find ("CongratzP1").GetComponent<Text> ();
		ranking = new Rank ();
		scoreP0 = PlayerPrefs.GetFloat ("scoreP0");
		scoreP1 = PlayerPrefs.GetFloat ("scoreP1");
		aux = ranking.madeToRanking (scoreP0, scoreP1);
		scoreP0Text.text = scoreP0.ToString ();
		scoreP1Text.text = scoreP1.ToString ();
		lettersP0Text[0].text = "A";
		lettersP0Text[1].text = "A";
		lettersP0Text[2].text = "A";
		lettersP1Text[0].text = "A";
		lettersP1Text[1].text = "A";
		lettersP1Text[2].text = "A";

		switch (aux) 
		{
		case 0:												//neither players made to rank
			SceneManager.LoadScene ("NewRankingScene");	
			break;

		case 1:												//Player 1 and Player 2 made to rank, active both players texts
			scoreP0Text.enabled = true;
			congratsP0Text.enabled = true;
			lettersP0Text [0].enabled = true;
			lettersP0Text [1].enabled = true;
			lettersP0Text [2].enabled = true;
			scoreP1Text.enabled = true;
			congratsP1Text.enabled = true;
			lettersP1Text [0].enabled = true;
			lettersP1Text [1].enabled = true;
			lettersP1Text [2].enabled = true;
			break;

			case 2:												//Player 1 made to rank, active Player 1 texts
			scoreP0Text.enabled = true;
			congratsP0Text.enabled = true;
			lettersP0Text [0].enabled = true;
			lettersP0Text [1].enabled = true;
			lettersP0Text [2].enabled = true;
			scoreP1Text.enabled = false;
			congratsP1Text.enabled = false;
			lettersP1Text [0].enabled = false;
			lettersP1Text [1].enabled = false;
			lettersP1Text [2].enabled = false;
			break;

			case 3:												//Player 2 made to rank, active Player 2 texts
			scoreP0Text.enabled = false;
			congratsP0Text.enabled = false;
			lettersP0Text [0].enabled = false;
			lettersP0Text [1].enabled = false;
			lettersP0Text [2].enabled = false;
			scoreP1Text.enabled = true;
			congratsP1Text.enabled = true;
			lettersP1Text [0].enabled = true;
			lettersP1Text [1].enabled = true;
			lettersP1Text [2].enabled = true;
			break;
			
		}

		counter = 0;
		flashSpeed = 10;
		press1Text = GameObject.Find ("Press1Text").GetComponent<Text> ();

	}


	void Update ()
	{
		if (!sceneDelay) {

			if (delayP0 == false) {											//if delay == false
				timerP0 += Time.deltaTime;									//increment timer
				if (timerP0 >= 0.15f) {										//if timer >= 0.15 secs
					delayP0 = true;											//active delay
					timerP0 = 0;											//reset timer
				}	
			}


			if (delayP0 == true) {												//if delay = true
				inputP0.y = InputArcade.Eixo (0, EEixo.VERTICAL);				//get vertical controller
				inputP0.x = InputArcade.Eixo (0, EEixo.HORIZONTAL);

				if (inputP0.y > 0) {
					i++;
					if (i > 36) {
						i = 0;
					}
					lettersP0Text [currentLetterP0].text = Convert.ToString (alphabet [i]);
					delayP0 = false;	
				}
				if (inputP0.y < 0) {				
					i--;
					if (i < 0) {
						i = 36;
					}
					lettersP0Text [currentLetterP0].text = Convert.ToString (alphabet [i]);
					delayP0 = false;	
				}


				if (inputP0.x > 0) {
					j++;
					if (j > 2) {
						j = 0;
					}
					for (int a = 0; a <= 36; a++) {
						if (Convert.ToChar (lettersP0Text [j].text) == alphabet [a]) {
							i = a;
						}	
					}
					currentLetterP0 = j;
					delayP0 = false;	
				}
				if (inputP0.x < 0) {				
					j--;
					if (j < 0) {
						j = 2;
					}
					for (int a = 0; a <= 36; a++) {
						if (Convert.ToChar (lettersP0Text [j].text) == alphabet [a]) {
							i = a;
						}
					}
					currentLetterP0 = j;
					delayP0 = false;	
				}

			}


			if (delayP1 == false) {											//if delay == false
				timerP1 += Time.deltaTime;									//increment timer
				if (timerP1 >= 0.15f) {										//if timer >= 0.15 secs
					delayP1 = true;											//active delay
					timerP1 = 0;											//reset timer
				}	
			}


			if (delayP1 == true) {												//if delay = true
				inputP1.y = InputArcade.Eixo (1, EEixo.VERTICAL);				//get vertical controller
				inputP1.x = InputArcade.Eixo (1, EEixo.HORIZONTAL);

				if (inputP1.y > 0) {
					k++;
					if (k > 36) {
						k = 0;
					}
					lettersP1Text [currentLetterP1].text = Convert.ToString (alphabet [k]);
					delayP1 = false;	
				}
				if (inputP1.y < 0) {				
					k--;
					if (k < 0) {
						k = 36;
					}
					lettersP1Text [currentLetterP1].text = Convert.ToString (alphabet [k]);
					delayP1 = false;	
				}


				if (inputP1.x > 0) {
					l++;
					if (l > 2) {
						l = 0;
					}
					for (int a = 0; a <= 36; a++) {
						if (Convert.ToChar (lettersP1Text [l].text) == alphabet [a]) {
							k = a;
						}
					}
					currentLetterP1 = l;
					delayP1 = false;	
				}
				if (inputP1.x < 0) {				
					l--;
					if (l < 0) {
						l = 2;
					}
					for (int a = 0; a <= 36; a++) {
						if (Convert.ToChar (lettersP1Text [l].text) == alphabet [a]) {
							k = a;
						}
					}
					currentLetterP1 = l;
					delayP1 = false;	

				}
			}


			if (InputArcade.Apertou (0, EControle.AZUL)) {				//if player 1 press VERDE button
				ranking.addNewScore (lettersP0Text [0].text + lettersP0Text [1].text + lettersP0Text [2].text, scoreP0);
				ranking.addNewScore (lettersP1Text [0].text + lettersP1Text [1].text + lettersP1Text [2].text, scoreP1);
				ranking.saveRank ();
				SceneManager.LoadScene ("NewRankingScene");					//load new ranking scene
			}

			if (counter >= flashSpeed) {									
				counter = 0;												
				toggle = !toggle;											
				if (toggle) {		
					press1Text.enabled = true;
					lettersP0Text [currentLetterP0].enabled = true;
					lettersP1Text [currentLetterP1].enabled = true;
				} else {													
					press1Text.enabled = false;
					lettersP0Text [currentLetterP0].enabled = false;
					lettersP1Text [currentLetterP1].enabled = false;
				}
			} else {													
				counter++;												
			}

			if (lettersP0Text [0] != lettersP0Text [currentLetterP0]) {
				lettersP0Text [0].enabled = true;
			}
			if (lettersP0Text [1] != lettersP0Text [currentLetterP0]) {
				lettersP0Text [1].enabled = true;
			}
			if (lettersP0Text [2] != lettersP0Text [currentLetterP0]) {
				lettersP0Text [2].enabled = true;
			}
			if (lettersP1Text [0] != lettersP1Text [currentLetterP1]) {
				lettersP1Text [0].enabled = true;
			}
			if (lettersP1Text [1] != lettersP1Text [currentLetterP1]) {
				lettersP1Text [1].enabled = true;
			}
			if (lettersP1Text [2] != lettersP1Text [currentLetterP1]) {
				lettersP1Text [2].enabled = true;
			}

		}
		sceneDelayTimer += Time.deltaTime;
		if (sceneDelayTimer >= 0.25f) {
			sceneDelay = false;
		}
	}
}
