﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArcadePUCCampinas;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RightHand : MonoBehaviour {

	Vector2 input;											//controller
	private int jogador = 1;								//set wich player is ( 1 = Player 2)
	public float velocity = 5000.0f; 						//set player mov. speed
	private float scoreP1, missesP1,  multiplierP1;			//score, misses and multiplier counter
	private float rightsP1, maxMultiplierP1;				//number of right colors and max multiplier reached
	private Text scoreP1Text;								//score text on screen
	private Text multiplierP1Text;							//current multiplier text on screen
	private Image healthBar;								//health bar image on screen
	private Image energyBar;

	private Transform leftHandTransform;
	private Vector3 leftHandPosition, rightHandPosition, bulletDirection, bulletDirectionVersor;
	private GameObject bullet;
	private float bulletDirectionMagnitude;
	public float bulletForce;

	private int turretsDestroyedP1;
	private int hitsP1;

	private bool sceneDelay;
	private float sceneDelayTimer;


	void Start () {
		sceneDelay = true;
		sceneDelayTimer = 0;

		scoreP1Text = GameObject.Find ("ScoreP1Text").GetComponent<Text> ();			//get Player 2 score Text Component	
		multiplierP1Text = GameObject.Find ("MultiplierP1Text").GetComponent<Text> ();	//get Player 2 multiplier Text Component
		healthBar = GameObject.Find ("HealthBar").GetComponent<Image> ();				//get Health Bar Image
		energyBar = GameObject.Find("EnergyBar").GetComponent<Image>();
		missesP1 = 0;																	//initiate Player 2 misses at zero
		scoreP1 = 0;																	//initiate Player 2 score at zero
		multiplierP1 = 1;																//initiate Player 2 multiplier at one
		rightsP1 = 0;																	//initiate Player 2 rights at zero
		maxMultiplierP1 = 0;															//initiate Player 2 max mult. at zero
		turretsDestroyedP1 = 0;
		hitsP1 = 0;

		PlayerPrefs.SetFloat ("scoreP1", scoreP1);						//set score in playerpref
		PlayerPrefs.SetFloat ("missesP1", missesP1);					//set misses in playerpref
		PlayerPrefs.SetFloat ("multiplierP1", multiplierP1);			//set multiplier in playerpref
		PlayerPrefs.SetFloat ("rightsP1", rightsP1);					//set rights in playerpref
		PlayerPrefs.SetFloat ("maxMultiplierP1", maxMultiplierP1);		//set max multiplier in playerpref
		PlayerPrefs.SetInt ("turretsDestroyedP1", turretsDestroyedP1);
		PlayerPrefs.SetInt ("hitsP1", hitsP1);

		SetScoreText ();												//set text score at zero on screen
		SetMultiplierText();											//set text multiplier at zero on screen
	}


	void Update () {
		if (!sceneDelay) {

			//set movement (physics) horizontal and vertical
			input.x = InputArcade.Eixo (jogador, EEixo.HORIZONTAL);
			input.y = InputArcade.Eixo (jogador, EEixo.VERTICAL);
			GetComponent<Rigidbody2D> ().AddForce (new Vector2 (1, 0) * input.x * velocity * Time.deltaTime);
			GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, 1) * input.y * velocity * Time.deltaTime);

			scoreP1 += Time.deltaTime * multiplierP1;						//scoreP1 increase over time and based on multiplier
			SetScoreText ();												//set text score on screen
			SetMultiplierText ();											//set text multiplier on screen

			if (multiplierP1 > maxMultiplierP1) {							//if current mult. > max mult.								
				maxMultiplierP1 = multiplierP1;								//max mult. = current mult.
			}

			if (energyBar.fillAmount > 0) {
				if (InputArcade.Apertou (jogador, EControle.AZUL) /*&& InputArcade.Apertado (jogador, EControle.AMARELO)*/) {																											
					rightHandPosition = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
					leftHandTransform = GameObject.Find ("Left Hand").GetComponent<Transform> ();
					leftHandPosition = new Vector3 (leftHandTransform.position.x, leftHandTransform.position.y, leftHandTransform.position.z);
					bullet = Instantiate (Resources.Load ("Bullet")) as GameObject;	
					bullet.GetComponent<Bullet> ().setShooter (jogador);
					bullet.transform.position = new Vector2 (leftHandPosition.x, leftHandPosition.y);
					bulletDirection = leftHandPosition - rightHandPosition;
					bulletDirectionMagnitude = bulletDirection.magnitude;
					bulletDirectionVersor = new Vector3 (bulletDirection.x / bulletDirectionMagnitude, bulletDirection.y / bulletDirectionMagnitude, bulletDirection.z / bulletDirectionMagnitude);
					bullet.GetComponent<Rigidbody2D> ().AddForce (bulletDirectionVersor * bulletForce /*/ bulletDirectionMagnitude*/);
					//bullet.GetComponent<Rigidbody2D>().AddForce (bulletDirection);
					energyBar.fillAmount -= 0.2f;
				}
			}

		}
		sceneDelayTimer += Time.deltaTime;
		if (sceneDelayTimer >= 0.25f) {
			sceneDelay = false;
		}
	}

	void OnTriggerStay2D(Collider2D infoCollider){																//function for when hand is on spot, check if right button is pressed
		if (!InputArcade.Apertou (jogador, EControle.AZUL)) {
			if (infoCollider.tag != "Bullet") {
				if (InputArcade.Apertou (jogador, EControle.VERDE)) {														//check if player pressed VERDE button
					if (infoCollider.gameObject.tag == "Green") {														//if object is green
						RightColor (infoCollider.gameObject);															//call RightCollor function
					} else {																								//if not
						WrongColor (infoCollider.gameObject);															//call WrongCollor function
						if (healthBar.fillAmount == 0) {																//and check if players health is zero now
							GameObject.Find ("Left Hand").GetComponent<LeftHand> ().SaveStatsP0 ();						//Call function SaveStatsP0, from LeftHand Script
							GameObject.Find ("Main Camera").GetComponent<SequencesGenerator> ().SaveStatsSequenceGen ();	//Call function SaveStatsSequenceGen, from SequenceGenerator Script
							SaveStatsP1 ();																				//Call SaveStatsP1 function
							HealthBarZero ();																			//call HealthBarZero function
						}
					}
				}
				if (InputArcade.Apertou (jogador, EControle.VERMELHO)) {													//check if player pressed VERMELHO button
					if (infoCollider.gameObject.tag == "Red") {															//if object is Red
						RightColor (infoCollider.gameObject);															//call RightCollor function
					} else {																								//if not
						WrongColor (infoCollider.gameObject);															//call WrongCollor function
						if (healthBar.fillAmount == 0) {																//and check if players health is zero now
							GameObject.Find ("Left Hand").GetComponent<LeftHand> ().SaveStatsP0 ();						//Call function SaveStatsP0, from LeftHand Script
							GameObject.Find ("Main Camera").GetComponent<SequencesGenerator> ().SaveStatsSequenceGen ();	//Call function SaveStatsSequenceGen, from SequenceGenerator Script
							SaveStatsP1 ();																				//Call SaveStatsP1 function
							HealthBarZero ();																			//call HealthBarZero function
						}
					}
				}
				if (InputArcade.Apertou (jogador, EControle.PRETO)) {														//PRETO button
					if (infoCollider.gameObject.tag == "Black") {														//Black
						RightColor (infoCollider.gameObject);
					} else {
						WrongColor (infoCollider.gameObject);
						if (healthBar.fillAmount == 0) {															
							GameObject.Find ("Left Hand").GetComponent<LeftHand> ().SaveStatsP0 ();				
							GameObject.Find ("Main Camera").GetComponent<SequencesGenerator> ().SaveStatsSequenceGen ();	
							SaveStatsP1 ();																				
							HealthBarZero ();																			
						}
					}
				}
				if (InputArcade.Apertou (jogador, EControle.BRANCO)) {													//BRANCO button
					if (infoCollider.gameObject.tag == "White") {														//White
						RightColor (infoCollider.gameObject);
					} else {
						WrongColor (infoCollider.gameObject);
						if (healthBar.fillAmount == 0) {													
							GameObject.Find ("Left Hand").GetComponent<LeftHand> ().SaveStatsP0 ();			
							GameObject.Find ("Main Camera").GetComponent<SequencesGenerator> ().SaveStatsSequenceGen ();	
							SaveStatsP1 ();																		
							HealthBarZero ();
						}
					}
				}
				if (InputArcade.Apertou (jogador, EControle.AMARELO)) {													//AMARELO	
					if (infoCollider.gameObject.tag == "Yellow") {														//Yellow
						RightColor (infoCollider.gameObject);
					} else {
						WrongColor (infoCollider.gameObject);
						if (healthBar.fillAmount == 0) {
							GameObject.Find ("Left Hand").GetComponent<LeftHand> ().SaveStatsP0 ();			
							GameObject.Find ("Main Camera").GetComponent<SequencesGenerator> ().SaveStatsSequenceGen ();	
							SaveStatsP1 ();																		
							HealthBarZero ();
						}
					}
				}
//			if (InputArcade.Apertou (jogador, EControle.AZUL)) {														//AZUL
//				if (infoCollider.gameObject.tag == "Blue") {														//Blue
//					RightColor (infoCollider.gameObject);
//				} else {
//					WrongColor (infoCollider.gameObject);
//					if (healthBar.fillAmount == 0) {
//						GameObject.Find ("Left Hand").GetComponent<LeftHand> ().SaveStatsP0 ();			
//						GameObject.Find ("Main Camera").GetComponent<SequencesGenerator> ().SaveStatsSequenceGen ();	
//						SaveStatsP1 ();																		
//						HealthBarZero ();
//						;
//					}
//				}
//			}
			}
		}
	}

	void SetScoreText (){							//function to update score on screen
		scoreP1Text.text = scoreP1.ToString ();  
	}

	void SetMultiplierText (){						//function to update current multiplier on sceen
		multiplierP1Text.text = multiplierP1.ToString () + "x";
	}

	void HealthBarZero(){   						//function for when health bar comes to zero
		Debug.Log ("Game Over");					//Display Game Over on Log								
		SceneManager.LoadScene ("PostGameScene");	//call post game scene when player dies
	}
		
	void WrongColor(GameObject infoCollider){																	//function for when player hits wrong color
		int auxX = infoCollider.gameObject.GetComponent<AutoDestroy>().getX();									//get color's position X 
		int auxY = infoCollider.gameObject.GetComponent<AutoDestroy>().getY();									//get color's position Y
		GameObject.Find ("Main Camera").GetComponent<SequencesGenerator> ().setBoolBoardFalse (auxX, auxY);		//set position on bool board to false
		Destroy (infoCollider);																					//Destroy the wrong color
		Debug.Log("Not ok");						
		healthBar.fillAmount -= 0.1f;																			//remove 10% from health
		missesP1++;																								//add 1 to wrong clicks
		multiplierP1 = 1;																						//set multiplier to one
	}

	void RightColor(GameObject infoCollider){																	//function for when player hits right color
		int auxX = infoCollider.gameObject.GetComponent<AutoDestroy>().getX();									//get color's position X 
		int auxY = infoCollider.gameObject.GetComponent<AutoDestroy>().getY();									//get color's position Y
		GameObject.Find ("Main Camera").GetComponent<SequencesGenerator> ().setBoolBoardFalse (auxX, auxY);		//set position on bool board to false
		GameObject spriteAfterDestroy = Instantiate (Resources.Load ("Sprites/SpriteAfterDestroy")) as GameObject;	
		spriteAfterDestroy.transform.position = infoCollider.transform.position;
		spriteAfterDestroy.transform.rotation = infoCollider.transform.rotation;
		if (infoCollider.tag == "Black") {
			spriteAfterDestroy.tag = "Black";
			spriteAfterDestroy.GetComponent<SpriteRenderer>().color = Color.black;
		}
		if (infoCollider.tag == "Blue") {
			spriteAfterDestroy.tag = "Blue";
			spriteAfterDestroy.GetComponent<SpriteRenderer>().color = Color.blue;
		}
		if (infoCollider.tag == "Yellow") {
			spriteAfterDestroy.tag = "Yellow";
			spriteAfterDestroy.GetComponent<SpriteRenderer>().color = Color.yellow;
		}
		if (infoCollider.tag == "Green") {
			spriteAfterDestroy.tag = "Green";
			spriteAfterDestroy.GetComponent<SpriteRenderer>().color = Color.green;
		}
		if (infoCollider.tag == "Red") {
			spriteAfterDestroy.tag = "Red";
			spriteAfterDestroy.GetComponent<SpriteRenderer>().color = Color.red;
		}
		if (infoCollider.tag == "White") {
			spriteAfterDestroy.tag = "White";
			spriteAfterDestroy.GetComponent<SpriteRenderer>().color = Color.white;
		}
		Destroy (infoCollider);																					//Destroy the right color
		multiplierP1++;																							//Add 1 to multiplier
		rightsP1 ++;																							//Add 1 to rights 
		if (multiplierP1 >= 5){
			energyBar.fillAmount += 0.2f;
		}
	}

	public void SaveStatsP1(){											//Function to save P2 current stats
		PlayerPrefs.SetFloat ("scoreP1", scoreP1);						//set score in playerpref
		PlayerPrefs.SetFloat ("missesP1", missesP1);					//set misses in playerpref
		PlayerPrefs.SetFloat ("multiplierP1", multiplierP1);			//set multiplier in playerpref
		PlayerPrefs.SetFloat ("rightsP1", rightsP1);					//set rights in playerpref
		PlayerPrefs.SetFloat ("maxMultiplierP1", maxMultiplierP1);		//set max multiplier in playerpref
		PlayerPrefs.SetInt ("turretsDestroyedP1", turretsDestroyedP1);
		PlayerPrefs.SetInt ("hitsP1", hitsP1);
		PlayerPrefs.Save ();											//and saves it
	}

	public void SetMultiplierP1(float aux){								//Set Player 2 multiplier function
		multiplierP1 = aux;												//to aux
	}
	public void addToTurretsDestroyedP1(){
		turretsDestroyedP1++;
	}

	public void addToHitsP1(){
		hitsP1++;
	}
}
