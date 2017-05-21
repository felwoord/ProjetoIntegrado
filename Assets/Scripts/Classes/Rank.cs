using System;
using System.Collections.Generic;
using UnityEngine;

namespace AssemblyCSharp
{
	public class Rank
	{
		private List<string> name = new List<string>();
		private List<float> score = new List<float>();

		public Rank ()
		{
			for (int i = 0; i <= 9; i++) {
				this.name.Add("AAA");
				this.score.Add(0);
				this.name [i] = PlayerPrefs.GetString ("rankingName " + i);						//get ranking nick on position i+1 in PlayerPrefs
				this.score [i] = PlayerPrefs.GetFloat ("rankingScore " + i);					//get ranking Score on position i+1 in playerPref
			}
		}


		public void addNewScore(string auxName, float auxScore){
			for (int i = 0; i <= 9; i++) {
				if (auxScore >= this.score [i]) {
					this.name.Insert (i, auxName);
					this.score.Insert (i, auxScore);
					this.score.RemoveAt (10);
					this.name.RemoveAt (10);				
					break;
				}
			}
		}

		public void saveRank(){
			for (int i = 0; i <= 9; i++) {
				PlayerPrefs.SetString ("rankingName " + i, this.name[i]);
				PlayerPrefs.SetFloat ("rankingScore " + i, this.score[i]);		
			}
			PlayerPrefs.Save();
		}

		public float getScore(int i){
			return this.score [i];
		}

		public string getName(int i){
			return this.name [i];
		}

		public int madeToRanking(float scoreP0, float scoreP1){						//function to check if players made to rank (0: neither, 1:both, 2:P1, 
			
			if (scoreP0 < this.score [9] && scoreP1 < this.score [9]) {				//if neither made to rank
				return 0;															//return 0;
			}
			if (scoreP0 >= scoreP1) {												//if P1 score > P2 score
				if (scoreP0 >= this.score [9]) {										//if P1 score made to rank
					if (scoreP1 >= this.score [8]) {										//if P2 score makes to rank after P1 score added
						return 1;																//both made to rank, return 1
					} else {																//if P2 score did not make after P1 score added									
						return 2;																//only P1 score makes, return 2
					}
				}
			} else{																	//if P1 score < P2 score
				if (scoreP1 >= this.score [9]) {										//if P2 score made to rank
					if (scoreP0 >= this.score [8]) {										//if P1 score makes to rank after P2 score added
						return 1;																//both made to rank, return 1
					} else {																//if P1 score did not make after P2 score added
						return 3;																//if only P2 score made to rank, return 3
					}			
				}
			}
			return 4;
		}




	}
}

