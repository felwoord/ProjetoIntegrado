using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArcadePUCCampinas;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RightHand : MonoBehaviour {

	Vector2 input;											//controller
	private int jogador = 1;								//set wich player is ( 1 = Player 2)
	private float velocity = 5000.0f; 						//set player mov. speed
	private float scoreP1, missesP1,  multiplierP1;			//score, misses and multiplier counter
	private float rightsP1, maxMultiplierP1;				//number of right colors and max multiplier reached
	public Text scoreP1Text;								//score text on screen
	public Text multiplierP1Text;							//current multiplier text on screen
	public Image healthBar;									//health bar image on screen

	void Start () {
		missesP1 = 0;										//initiate Player 2 misses at zero
		scoreP1 = 0;										//initiate Player 2 score at zero
		multiplierP1 = 1;									//initiate Player 2 multiplier at one
		rightsP1 = 0;										//initiate Player 2 rights at zero
		maxMultiplierP1 = 0;								//initiate Player 2 max mult. at zero

		PlayerPrefs.SetFloat ("scoreP1", scoreP1);						//set score in playerpref
		PlayerPrefs.SetFloat ("missesP1", missesP1);					//set misses in playerpref
		PlayerPrefs.SetFloat ("multiplierP1", multiplierP1);			//set multiplier in playerpref
		PlayerPrefs.SetFloat ("rightsP1", rightsP1);					//set rights in playerpref
		PlayerPrefs.SetFloat ("maxMultiplierP1", maxMultiplierP1);		//set max multiplier in playerpref

		SetScoreText ();												//set text score at zero on screen
		SetMultiplierText();											//set text multiplier at zero on screen
	}


	void Update () {

		//set movement (physics) horizontal and vertical
		input.x = InputArcade.Eixo(jogador, EEixo.HORIZONTAL);
		input.y = InputArcade.Eixo(jogador, EEixo.VERTICAL);
		GetComponent<Rigidbody2D> ().AddForce (new Vector2 (1, 0) * input.x * velocity * Time.deltaTime);
		GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, 1) * input.y * velocity * Time.deltaTime);

		scoreP1 += Time.deltaTime * multiplierP1;						//scoreP1 increase over time and based on multiplier
		SetScoreText ();												//set text score on screen
		SetMultiplierText ();											//set text multiplier on screen

		if (multiplierP1 > maxMultiplierP1) {							//if current mult. > max mult.								
			maxMultiplierP1 = multiplierP1;								//max mult. = current mult.
		}

	}

	void OnTriggerStay2D(Collider2D infoCollider){																//function for when hand is on spot, check if right button is pressed
		if (InputArcade.Apertou(jogador, EControle.VERDE)){														//check if player pressed VERDE button
			if ( infoCollider.gameObject.tag == "Green"){														//if object is green
				RightColor (infoCollider.gameObject);															//call RightCollor function
			}else{																								//if not
				WrongColor (infoCollider.gameObject);															//call WrongCollor function
				if (healthBar.fillAmount == 0) {																//and check if players health is zero now
					GameObject.Find("Left Hand").GetComponent<LeftHand>().SaveStatsP0();						//Call function SaveStatsP0, from LeftHand Script
					GameObject.Find("Main Camera").GetComponent<SequencesGenerator>().SaveStatsSequenceGen();	//Call function SaveStatsSequenceGen, from SequenceGenerator Script
					SaveStatsP1();																				//Call SaveStatsP1 function
					HealthBarZero ();																			//call HealthBarZero function
				}
			}
		}
		if (InputArcade.Apertou(jogador, EControle.VERMELHO)){													//check if player pressed VERMELHO button
			if ( infoCollider.gameObject.tag == "Red"){															//if object is Red
				RightColor (infoCollider.gameObject);															//call RightCollor function
			}else{																								//if not
				WrongColor (infoCollider.gameObject);															//call WrongCollor function
				if (healthBar.fillAmount == 0) {																//and check if players health is zero now
					GameObject.Find("Left Hand").GetComponent<LeftHand>().SaveStatsP0();						//Call function SaveStatsP0, from LeftHand Script
					GameObject.Find("Main Camera").GetComponent<SequencesGenerator>().SaveStatsSequenceGen();	//Call function SaveStatsSequenceGen, from SequenceGenerator Script
					SaveStatsP1();																				//Call SaveStatsP1 function
					HealthBarZero ();																			//call HealthBarZero function
				}
			}
		}
		if (InputArcade.Apertou(jogador, EControle.PRETO)){														//PRETO button
			if ( infoCollider.gameObject.tag == "Black"){														//Black
				RightColor (infoCollider.gameObject);
			}else{
				WrongColor (infoCollider.gameObject);
				if (healthBar.fillAmount == 0) {															
					GameObject.Find("Left Hand").GetComponent<LeftHand>().SaveStatsP0();				
					GameObject.Find("Main Camera").GetComponent<SequencesGenerator>().SaveStatsSequenceGen();	
					SaveStatsP1();																				
					HealthBarZero ();																			
				}
			}
		}
		if (InputArcade.Apertou(jogador, EControle.BRANCO)){													//BRANCO button
			if ( infoCollider.gameObject.tag == "White"){														//White
				RightColor (infoCollider.gameObject);
			}else{
				WrongColor (infoCollider.gameObject);
				if (healthBar.fillAmount == 0) {													
					GameObject.Find("Left Hand").GetComponent<LeftHand>().SaveStatsP0();			
					GameObject.Find("Main Camera").GetComponent<SequencesGenerator>().SaveStatsSequenceGen();	
					SaveStatsP1();																		
					HealthBarZero ();
				}
			}
		}
		if (InputArcade.Apertou(jogador, EControle.AMARELO)){													//AMARELO	
			if ( infoCollider.gameObject.tag == "Yellow"){														//Yellow
				RightColor (infoCollider.gameObject);
			}else{
				WrongColor (infoCollider.gameObject);
				if (healthBar.fillAmount == 0) {
					GameObject.Find("Left Hand").GetComponent<LeftHand>().SaveStatsP0();			
					GameObject.Find("Main Camera").GetComponent<SequencesGenerator>().SaveStatsSequenceGen();	
					SaveStatsP1();																		
					HealthBarZero ();
				}
			}
		}
		if (InputArcade.Apertou(jogador, EControle.AZUL)){														//AZUL
			if ( infoCollider.gameObject.tag == "Blue"){														//Blue
				RightColor (infoCollider.gameObject);
			}else{
				WrongColor (infoCollider.gameObject);
				if (healthBar.fillAmount == 0) {
					GameObject.Find("Left Hand").GetComponent<LeftHand>().SaveStatsP0();			
					GameObject.Find("Main Camera").GetComponent<SequencesGenerator>().SaveStatsSequenceGen();	
					SaveStatsP1();																		
					HealthBarZero ();;
				}
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
		Destroy (infoCollider);																					//Destroy the right color
		multiplierP1++;																							//Add 1 to multiplier
		rightsP1 ++;																							//Add 1 to rights 
	}

	public void SaveStatsP1(){											//Function to save P2 current stats
		PlayerPrefs.SetFloat ("scoreP1", scoreP1);						//set score in playerpref
		PlayerPrefs.SetFloat ("missesP1", missesP1);					//set misses in playerpref
		PlayerPrefs.SetFloat ("multiplierP1", multiplierP1);			//set multiplier in playerpref
		PlayerPrefs.SetFloat ("rightsP1", rightsP1);					//set rights in playerpref
		PlayerPrefs.SetFloat ("maxMultiplierP1", maxMultiplierP1);		//set max multiplier in playerpref
		PlayerPrefs.Save ();											//and saves it
	}

	public void SetMultiplierP1(float aux){								//Set Player 2 multiplier function
		multiplierP1 = aux;												//to aux
	}
}
