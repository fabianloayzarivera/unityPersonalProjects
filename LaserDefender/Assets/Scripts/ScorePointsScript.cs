using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class ScorePointsScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Text myText = GetComponent<Text> ();
		myText.text = ScoreScript.score.ToString();
		ScoreScript.reset ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
