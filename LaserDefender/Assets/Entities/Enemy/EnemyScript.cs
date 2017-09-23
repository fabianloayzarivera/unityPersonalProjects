using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {
	public float health = 100f;
	void OnTriggerEnter2D(Collider2D col){
		
		LaserShotScript shot = col.gameObject.GetComponent<LaserShotScript> ();
		if (shot != null) {
			print ("Enemy Hit by LaserShot!");
			health -= shot.getDamage ();
			shot.hit ();
			if (health <= 0) {
				Destroy (gameObject);
			}
		}
		
	}
}
