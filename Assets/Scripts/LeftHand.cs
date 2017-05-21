using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArcadePUCCampinas;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LeftHand : MonoBehaviour {

	Vector2 input;											//controller
	private int jogador = 0;								//set wich player is ( 0 = Player 1)
	public float velocity = 5000.0f; 						//set player mov. speed
	private float scoreP0, missesP0,  multiplierP0;			//score, misses and multiplier counter
	private float rightsP0, maxMultiplierP0;				//number of right colors and max multiplier reached
	private Text scoreP0Text;								//score text on screen
	private Text multiplierP0Text;							//current multiplier text on screen
	private Image healthBar;								//health bar image on screen

	void Start () {
		scoreP0Text = GameObject.Find ("ScoreP0Text").GetComponent<Text> ();			//get Player 1 score Text Component	
		multiplierP0Text = GameObject.Find ("MultiplierP0Text").GetComponent<Text> ();	//get Player 1 multiplier Text Component
		healthBar = GameObject.Find ("HealthBar").GetComponent<Image> ();				//get Health Bar Image
		missesP0 = 0;																	//initiate Player 1 misses at zero
		scoreP0 = 0;																	//initiate Player 1 score at zero
		multiplierP0 = 1;																//initiate Player 1 multiplier at one
		rightsP0 = 0;																	//initiate Player 1 rights at zero
		maxMultiplierP0 = 0;															//initiate Player 1 max mult. at zero

		PlayerPrefs.SetFloat ("scoreP0", scoreP0);						//set score in playerpref
		PlayerPrefs.SetFloat ("missesP0", missesP0);					//set misses in playerpref
		PlayerPrefs.SetFloat ("multiplierP0", multiplierP0);			//set multiplier in playerpref
		PlayerPrefs.SetFloat ("rightsP0", rightsP0);					//set rights in playerpref
		PlayerPrefs.SetFloat ("maxMultiplierP0", maxMultiplierP0);		//set max multiplier in playerpref

		SetScoreText ();												//set text score at zero on screen
		SetMultiplierText();											//set text multiplier at zero on screen
	}
	

	void Update () {

		//set movement (physics) horizontal and vertical
		input.x = InputArcade.Eixo(jogador, EEixo.HORIZONTAL);
		input.y = InputArcade.Eixo(jogador, EEixo.VERTICAL);
		GetComponent<Rigidbody2D> ().AddForce (new Vector2 (1, 0) * input.x * velocity * Time.deltaTime);
		GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, 1) * input.y * velocity * Time.deltaTime);

		scoreP0 += Time.deltaTime * multiplierP0;						//scoreP0 increase over time and based on multiplier
		SetScoreText ();												//set text score on screen
		SetMultiplierText ();											//set text multiplier on screen

		if (multiplierP0 > maxMultiplierP0) {							//if current mult. > max mult.								
			maxMultiplierP0 = multiplierP0;								//max mult. = current mult.
		}

	}
		
	void OnTriggerStay2D(Collider2D infoCollider){																//function for when hand is on spot, check if right button is pressed
		if (InputArcade.Apertou(jogador, EControle.VERDE)){														//check if player pressed VERDE button
			if ( infoCollider.gameObject.tag == "Green"){														//if object is green
				RightColor (infoCollider.gameObject);															//call RightCollor function
			}else{																								//if not
				WrongColor (infoCollider.gameObject);															//call WrongCollor function
				if (healthBar.fillAmount == 0) {																//and check if players health is zero now
					GameObject.Find("Right Hand").GetComponent<RightHand>().SaveStatsP1();						//Call function SaveStatsP1, from RightHand Script
					GameObject.Find("Main Camera").GetComponent<SequencesGenerator>().SaveStatsSequenceGen();	//Call function SaveStatsSequenceGen, from SequenceGenerator Script
					SaveStatsP0	();																				//Call SaveStatsP0 function
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
					GameObject.Find("Right Hand").GetComponent<RightHand>().SaveStatsP1();						//Call function SaveStatsP1, from RightHand Script
					GameObject.Find("Main Camera").GetComponent<SequencesGenerator>().SaveStatsSequenceGen();	//Call function SaveStatsSequenceGen, from SequenceGenerator Script
					SaveStatsP0	();																				//Call SaveStatsP0 function
					HealthBarZero ();																			//call HealthBarZero function
				}
			}
		}
		if (InputArcade.Apertou(jogador, EControle.PRETO)){														//PRETO
			if ( infoCollider.gameObject.tag == "Black"){														//Black
				RightColor (infoCollider.gameObject);
			}else{
				WrongColor (infoCollider.gameObject);
				if (healthBar.fillAmount == 0) {
					GameObject.Find("Right Hand").GetComponent<RightHand>().SaveStatsP1();						
					GameObject.Find("Main Camera").GetComponent<SequencesGenerator>().SaveStatsSequenceGen();	
					SaveStatsP0	();																				
					HealthBarZero ();
				}
			}
		}
		if (InputArcade.Apertou(jogador, EControle.BRANCO)){													//BRANCO
			if ( infoCollider.gameObject.tag == "White"){														//White
				RightColor (infoCollider.gameObject);
			}else{
				WrongColor (infoCollider.gameObject);
				if (healthBar.fillAmount == 0) {
					GameObject.Find("Right Hand").GetComponent<RightHand>().SaveStatsP1();						
					GameObject.Find("Main Camera").GetComponent<SequencesGenerator>().SaveStatsSequenceGen();	
					SaveStatsP0	();																				
					HealthBarZero ();
				}
			}
		}
		if (InputArcade.Apertou(jogador, EControle.AMARELO)){													//Amarelo
			if ( infoCollider.gameObject.tag == "Yellow"){														//Yellow
				RightColor (infoCollider.gameObject);
			}else{
				WrongColor (infoCollider.gameObject);
				if (healthBar.fillAmount == 0) {
					GameObject.Find("Right Hand").GetComponent<RightHand>().SaveStatsP1();						
					GameObject.Find("Main Camera").GetComponent<SequencesGenerator>().SaveStatsSequenceGen();	
					SaveStatsP0	();																				
					HealthBarZero ();
				}
			}
		}
		if (InputArcade.Apertou(jogador, EControle.AZUL)){														//Azul
			if ( infoCollider.gameObject.tag == "Blue"){														//Blue
				RightColor (infoCollider.gameObject);
			}else{
				WrongColor (infoCollider.gameObject);
				if (healthBar.fillAmount == 0) {
					GameObject.Find("Right Hand").GetComponent<RightHand>().SaveStatsP1();						
					GameObject.Find("Main Camera").GetComponent<SequencesGenerator>().SaveStatsSequenceGen();	
					SaveStatsP0	();																				
					HealthBarZero ();
				}
			}
		}
	}

	void SetScoreText (){							//function to update score on screen
		scoreP0Text.text = scoreP0.ToString ();
	}

	void SetMultiplierText (){						//function to update current multiplier on sceen
		multiplierP0Text.text = multiplierP0.ToString () + "x";
	}

	void HealthBarZero(){   						//function for when health bar comes to zero
		Debug.Log ("Game Over");					//Display Game Over on Log
		SceneManager.LoadScene ("GetNickScene");	//call get nick scene when player dies
	}

	void WrongColor(GameObject infoCollider){																	//function for when player hits wrong color
		int auxX = infoCollider.gameObject.GetComponent<AutoDestroy>().getX();									//get color's position X 
		int auxY = infoCollider.gameObject.GetComponent<AutoDestroy>().getY();								//get color's position Y
		GameObject.Find ("Main Camera").GetComponent<SequencesGenerator> ().setBoolBoardFalse (auxX, auxY);		//set position on bool board to false
		Destroy (infoCollider);																					//Destroy the wrong color
		Debug.Log("Not ok");						
		healthBar.fillAmount -= 0.1f;																			//remove 10% from health
		missesP0++;																								//add 1 to wrong clicks
		multiplierP0 = 1;																						//set multiplier to one
	}

	void RightColor(GameObject infoCollider){																	//function for when player hits right color
		int auxX = infoCollider.gameObject.GetComponent<AutoDestroy>().getX();									//get color's position X 
		int auxY = infoCollider.gameObject.GetComponent<AutoDestroy>().getY();									//get color's position Y
		GameObject.Find ("Main Camera").GetComponent<SequencesGenerator> ().setBoolBoardFalse (auxX, auxY);		//set position on bool board to false
		Destroy (infoCollider);																					//Destroy the right color
		multiplierP0++;																							//Add 1 to multiplier
		rightsP0 ++;																							//Add 1 to rights 
	}

	public void SaveStatsP0(){											//Function to save P1 current stats
		PlayerPrefs.SetFloat ("scoreP0", scoreP0);						//set score in playerpref
		PlayerPrefs.SetFloat ("missesP0", missesP0);					//set misses in playerpref
		PlayerPrefs.SetFloat ("multiplierP0", multiplierP0);			//set multiplier in playerpref
		PlayerPrefs.SetFloat ("rightsP0", rightsP0);					//set rights in playerpref
		PlayerPrefs.SetFloat ("maxMultiplierP0", maxMultiplierP0);		//set max multiplier in playerpref
		PlayerPrefs.Save ();											//and saves it
	}

	public void SetMultiplierP0(float aux){								//Set Player 1 multiplier function
		multiplierP0 = aux;												//to aux
	}
}
