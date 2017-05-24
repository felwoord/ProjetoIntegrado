using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SequencesGenerator : MonoBehaviour {
	
	private GameObject[,] board;		//board
	private int p,q,r;					//random numbers
	private float period, difficult, t;	//timer, difficult (how often colors respawn)
	private float g;
	private int boardCounter;			//BoolBoard counter
	private GameObject childObject;		//children objects
	private int selfDestroyed;			//number of self destroyed colors
	private bool[,] boolBoard;			//bool boar to check if position is taken
	private Vector3 localScaleSize = new Vector3 (0.3f, 0.3f, 0.3f); //local scale size of color
	private bool difficultBool = true;	//bool to make difficult stop decrement on difficult
	public float minDifficult;			//minimun value to difficult
	private bool[,] boolTurret;
	private GameObject turret;

	void Start () {
		boolTurret = new bool[2, 2];
		boolTurret [0, 0] = false;
		boolTurret [0, 1] = false;
		boolTurret [1, 0] = false;
		boolTurret [1, 1] = false;
		selfDestroyed = 0;										//start self destroyed at zero
		PlayerPrefs.SetInt ("selfDestroyed", selfDestroyed);	//set it on player prefs
		board = new GameObject[7,14];							//set board size
		boolBoard = new bool[7,14];								//set bool board size
		difficult = 5.0f;										//initiate difficult at 5 seconds



		//Get board and set bool board to false
		for (int i = 0; i <= 6; i++) {
			for (int j = 0; j <= 13; j++) {
				board [i, j] = GameObject.Find (i + "-" + j);
				boolBoard [i, j] = false;
			}
		}

//		i = 9;										//do not remember why i = 9 and j = 9 ZzZzZzZzZzZzZzZzZzZzZzZzZzZzZzZzZzZzZzZz 
//		j = 9;
	}


	void Update () {
		//count how many bools on boolBoard are true(filled)
		boardCounter = 0;						
		for (int i = 0; i <= 6; i++) {
			for (int j = 0; j <= 13; j++) {
				if (boolBoard [i, j] == true) {
					boardCounter += 1;
				}
			}
		}

		//Do every "difficult" seconds
		if (period > difficult ) {

			if (boardCounter < 98) {								//if there is at least one false(empty) on boolBoard
				
				for (int i = 0; i <= 1; i++) {						//spawn 2 colors
					//random position and color generator
					p = Random.Range (0, 7);						//x
					q = Random.Range (0, 14);						//y
					r = Random.Range (0, 6);						//color

					while (boolBoard [p, q] == true) {				//while position on board is full, get new position and color
						//random position and color generator
						p = Random.Range (0, 7);					//x
						q = Random.Range (0, 14);					//y
						r = Random.Range (0, 6);					//color
					}

					GlowBoard (p, q, r);								//Call GlowBoard function

				}
			}
			period = 0;											//reset timer
		}


		//every 3 seconds colors respawn 0.1 seconds faster
		if (t > 3) {
			if (difficultBool) {
				difficult -= 0.1f;	
			}
			//Debug.Log (difficult);

			t = 0;
			if (difficult <= minDifficult) {
				difficultBool = false;
			}
		}





		if (g > 8) {
			for (int i = 0; i <= 1; i++) {
				for (int j = 0; j <= 1; j++) {
					if (boolTurret [i, j] == false) {
						turret = Instantiate (Resources.Load ("Turret")) as GameObject;
						turret.GetComponent<Turret> ().setPosition (i, j);
						boolTurret [i, j] = true;
					}
				}
			}
			g = 0;
		}

		period += UnityEngine.Time.deltaTime;	//increment timer 
		t += UnityEngine.Time.deltaTime;		//increment counter
		g += Time.deltaTime;

	}
		



	void GlowBoard(int a,int b, int c){																			//function to create a color
		switch (c) {																							//switch the random generated number c

		case 0:																									//case c = 0
			childObject = Instantiate (Resources.Load ("Black")) as GameObject;									//instantiate Black as object
			childObject.transform.parent = board [a, b].transform;												//set as child object from position generated from a and b
			childObject.transform.localPosition = new Vector2 (0, 0);											//set position to (0,0) (center of the parent object
			childObject.transform.localScale = localScaleSize;													//set size to localScaleSize
			BoxCollider2D boxCollider0 = childObject.AddComponent<BoxCollider2D> () as BoxCollider2D;			//add box collier to object
			boxCollider0.isTrigger = true;																		//active isTrigger
			boolBoard [a, b] = true;																			//set bool board at position a,b to true
			childObject.GetComponent<AutoDestroy> ().SetX (a);													//set position X in AutoDestroy script
			childObject.GetComponent<AutoDestroy> ().SetY (b);													//set position Y in AutoDestroy script
			break;
				
		case 1:																									//case c = 1
			childObject = Instantiate (Resources.Load ("Blue")) as GameObject;									//instantiate Blue as object
			childObject.transform.parent = board [a, b].transform;												//set as child object from position generated from a and b
			childObject.transform.localPosition = new Vector2 (0, 0);											//set position to (0,0) (center of the parent object
			childObject.transform.localScale = localScaleSize;													//set size to localScaleSize
			BoxCollider2D boxCollider1 = childObject.AddComponent<BoxCollider2D> () as BoxCollider2D;			//add box collier to object
			boxCollider1.isTrigger = true;																		//active isTrigger
			boolBoard [a, b] = true;																			//set bool board at position a,b to true
			childObject.GetComponent<AutoDestroy> ().SetX (a);													//set position X in AutoDestroy script
			childObject.GetComponent<AutoDestroy> ().SetY (b);													//set position Y in AutoDestroy script
			break;
		
		case 2:																									//c = 2
			childObject = Instantiate (Resources.Load ("Green")) as GameObject;									//Green
			childObject.transform.parent = board [a, b].transform;
			childObject.transform.localPosition = new Vector2 (0, 0);
			childObject.transform.localScale = localScaleSize;
			BoxCollider2D boxCollider2 = childObject.AddComponent<BoxCollider2D> () as BoxCollider2D;
			boxCollider2.isTrigger = true;
			boolBoard [a, b] = true;
			childObject.GetComponent<AutoDestroy> ().SetX (a);
			childObject.GetComponent<AutoDestroy> ().SetY (b);
			break;

		case 3:																									//c = 3
			childObject = Instantiate (Resources.Load ("Red")) as GameObject;									//Red
			childObject.transform.parent = board [a, b].transform;
			childObject.transform.localPosition = new Vector2 (0, 0);
			childObject.transform.localScale = localScaleSize;
			BoxCollider2D boxCollider3 = childObject.AddComponent<BoxCollider2D> () as BoxCollider2D;
			boxCollider3.isTrigger = true;
			boolBoard [a, b] = true;
			childObject.GetComponent<AutoDestroy> ().SetX (a);
			childObject.GetComponent<AutoDestroy> ().SetY (b);
			break;

		case 4:																									//c = 4
			childObject = Instantiate (Resources.Load ("White")) as GameObject;									//White
			childObject.transform.parent = board [a, b].transform;
			childObject.transform.localPosition = new Vector2 (0, 0);
			childObject.transform.localScale = localScaleSize;
			BoxCollider2D boxCollider4 = childObject.AddComponent<BoxCollider2D> () as BoxCollider2D;
			boxCollider4.isTrigger = true;
			boolBoard [a, b] = true;
			childObject.GetComponent<AutoDestroy> ().SetX (a);
			childObject.GetComponent<AutoDestroy> ().SetY (b);
			break;

		case 5:																									//c = 5
			childObject = Instantiate (Resources.Load ("Yellow")) as GameObject;								//Yellow
			childObject.transform.parent = board [a, b].transform;
			childObject.transform.localPosition = new Vector2 (0, 0);
			childObject.transform.localScale = localScaleSize;
			BoxCollider2D boxCollider5 = childObject.AddComponent<BoxCollider2D> () as BoxCollider2D;
			boxCollider5.isTrigger = true;
			boolBoard [a, b] = true;
			childObject.GetComponent<AutoDestroy> ().SetX (a);
			childObject.GetComponent<AutoDestroy> ().SetY (b);
			break;
		}
	}

	public void SaveStatsSequenceGen(){							//Function to save SequenceGenerator current stats
		PlayerPrefs.SetInt ("selfDestroyed", selfDestroyed);		//set self destroyed on player prefs
		PlayerPrefs.Save ();
	}

	public void setBoolBoardFalse(int a, int b){				//function to set position on bool board to false
		boolBoard [a, b] = false;
	}

	public void AddSelfDestroyed(){
		selfDestroyed++;
	}

	public void setBoolTurretFalse(int a, int b){
		boolTurret [a, b] = false;
	}
}
