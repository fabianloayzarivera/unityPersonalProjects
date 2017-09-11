using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour {

	public static int breakableCount = 0;
	public AudioClip crack;
	public GameObject smoke;
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
		AudioSource.PlayClipAtPoint (crack, transform.position);
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
			GameObject smokePuff = Instantiate (smoke, gameObject.transform.position, Quaternion.identity) as GameObject;
			smokePuff.GetComponent<ParticleSystem>().startColor = gameObject.GetComponent<SpriteRenderer> ().color;
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
