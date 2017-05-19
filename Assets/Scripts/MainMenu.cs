using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ArcadePUCCampinas;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
	private Vector2 position1, position2, position3, currentPosition;  	//positions mark and current position of the Selector Arrow
	Vector2 input;														//controller
	float timer;													  	//timer to make selecting better
	bool delay = true;													//delay to be able to move selector	arrow
	private SpriteRenderer selector;										//SpriteRenderer from selector image
	private int flashSpeed = 10;										//how fast image blinks
	private int counter;												//counter to blink image
	private bool toggle = false;										//toggle to active and deactive image


	void Start () {
		selector = GetComponent<SpriteRenderer> ();
		position1 = new Vector2 (0, -0.60f);							//position 1 = start game
		position2 = new Vector2 (0, -1.55f);							//position 2 = rules
		position3 = new Vector2 (0, -2.50f);							//position 3 = Ranking

		
	}
	

	void FixedUpdate () {
		if (counter >= flashSpeed) {									//if counter >= flashSpeed
			counter = 0;												//reset counter
			toggle = !toggle;											//change toggle

			if (toggle) {												//if toggle = true
				selector.enabled = true;								//active image
			} else {													//if toggle = false
				selector.enabled = false;								//deactive image
			}
		} else {														//if counter < flashSpeed
			counter++;													//add to counter
		}

		if (delay == false) {											//if delay == false
			timer += Time.deltaTime;									//increment timer
			if (timer >= 0.15f) {										//if timer >= 0.15 secs
				delay = true;											//active delay
				timer = 0;												//reset timer
			}	
		}

		if (currentPosition == position1) {								//if current position = position 1
			if (InputArcade.Apertou (0, EControle.AZUL)) {				//if player 1 press VERDE button
				SceneManager.LoadScene ("GameScene");					//load Game Scene
			}
		}
		if (currentPosition == position2) {								//if current position = position 2
			if (InputArcade.Apertou (0, EControle.AZUL)) {				//if player 1 press VERDE button
				SceneManager.LoadScene ("RulesScene");					//load RulesScene
			}
		}
		if (currentPosition == position3) {								//if current position = position 3
			if (InputArcade.Apertou (0, EControle.AZUL)) {				//if player 1 press VERDE button
				SceneManager.LoadScene ("RankingScene");				//load RankScene
			}
		}


		if (delay == true) {											//if delay = true
			input.y = InputArcade.Eixo (0, EEixo.VERTICAL);				//get vertical controller

			currentPosition = transform.position;						//get current position of the Selector Arrow
			if (currentPosition == position1) {							//if current position = position 1
				if (input.y < 0) {										//if player 1 press down
					transform.position = position2;						//change position of the Selector Arrow to position 2
					delay = false;										//set delay to false
				}
			}
			if (currentPosition == position2) {							//if current position = position 2
				if (input.y > 0) {										//if player 1 press up
					transform.position = position1;						//change position to position 1
					delay = false;										//set delay to false
				}
				if (input.y < 0) {										//if player 1 press down
					transform.position = position3;						//change position to position 3
					delay = false;										//set delay to false
				}
			}
			if (currentPosition == position3) {							//if current position = position 3
				if (input.y > 0) {										//if player 1 press up
					transform.position = position2;						//change position to position 2
					delay = false;										//set delay to false
				}
			}
				
		}
	}

}
