using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour {

	public static int breakableCount = 0;

	private int maxHits;
	private int timesHit;
	public Sprite[] hitSprites;
	private LevelManager levelmanager;
	private bool isBreakable;
	// Use this for initialization
	void Start () {
		isBreakable = (this.tag == "Breakable");
		if (isBreakable) {
			breakableCount++;
		}
		timesHit = 0;
		levelmanager = GameObject.FindObjectOfType<LevelManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D (Collision2D col){
		
		if (isBreakable) {
			handleHits ();
		}
	
	}

	void handleHits(){
		timesHit++;
		maxHits = hitSprites.Length + 1;
		//simulateWin ();
		if (timesHit >= maxHits) {
			breakableCount--;
			levelmanager.brickDestroyed ();
			Destroy (gameObject);
			//print (breakableCount);
		} else {
			LoadSprites ();
		}
	}

	void simulateWin(){
		levelmanager.LoadNextLevel ();
	}

	void LoadSprites(){
		int spriteIndex = timesHit - 1;
		if (hitSprites [spriteIndex]) {
			this.GetComponent<SpriteRenderer> ().sprite = hitSprites [spriteIndex];
		}
	}
}
