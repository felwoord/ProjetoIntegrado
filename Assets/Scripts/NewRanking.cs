using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ArcadePUCCampinas;

public class NewRanking : MonoBehaviour {
	private float[] rankingScore = new float[10];								
	private string[] rankingNick = new string[10];
	public Text[] nicksText = new Text[10];
	public Text[] scoresText = new Text[10];
	private bool sceneDelay;
	private float sceneDelayTimer;

	private float counter, flashSpeed;
	private bool toggle;
	private Text press1Text;


	// Use this for initialization
	void Start () {
		counter = 0;
		flashSpeed = 10;
		press1Text = GameObject.Find ("Press1Text").GetComponent<Text> ();

		sceneDelay = true;
		sceneDelayTimer = 0;

		for(int i = 0; i<=9;i++){
			nicksText [i] = GameObject.Find ("Nick" + i + "Text").GetComponent<Text> ();
			scoresText [i] = GameObject.Find ("Score" + i + "Text").GetComponent<Text> ();
			rankingNick [i] = PlayerPrefs.GetString ("rankingName " + i);						//get ranking nick on position i+1 in PlayerPrefs
			rankingScore [i] = PlayerPrefs.GetFloat ("rankingScore " + i);
			nicksText [i].text = rankingNick [i].ToString ();
			scoresText [i].text = rankingScore [i].ToString ();
		}
	}
	
	// Update is called once per frame
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
