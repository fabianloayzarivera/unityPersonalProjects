using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {
	public float health = 100f;
	public GameObject enemyShot;
	public float enemyShotSpeed = 5f;
	public float shotsPerSeconds = 0.5f;

	void OnTriggerEnter2D(Collider2D col){
		
		LaserShotScript shot = col.gameObject.GetComponent<LaserShotScript> ();
		if (shot != null) {
			//print ("Enemy Hit by LaserShot!");
			health -= shot.getDamage ();
			shot.hit ();
			if (health <= 0) {
				Destroy (gameObject);

			}
		}
		
	}

	void Start(){
		
	}
	void Update(){
		float probability = Time.deltaTime * shotsPerSeconds;
		if(Random.value < probability){
			fire ();
		}
	}

	void fire(){
		Vector3 startPosition = transform.position + new Vector3 (0f, -1f, 0f);
		GameObject shot = Instantiate (enemyShot, startPosition, Quaternion.identity) as GameObject;
		shot.GetComponent<Rigidbody2D>().velocity = new Vector3 (0f, -enemyShotSpeed, 0f);
	}
}
