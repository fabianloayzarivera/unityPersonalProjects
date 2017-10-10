using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {

	public static int score = 0;
	private Text textScore;

	public void Start(){
		textScore = GetComponent<Text> ();
		reset ();
		textScore.text = score.ToString ();

	}

	public void scorePoints(int points){
		score += points;
		textScore.text = score.ToString();
	}
	public static void reset(){
		score = 0;


	}
}
