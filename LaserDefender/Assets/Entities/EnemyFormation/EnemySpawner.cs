using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
	public GameObject enemyPrefab;
	public float width = 10f;
	public float height = 5f;
	public float speed = 5f;
	public float spawnDelay = 0.5f;
	private float xmin;
	private float xmax;
	private bool movingRight= false;

	void Start () {		
		
		spawnEnemies ();

		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftMost = Camera.main.ViewportToWorldPoint (new Vector3 (0,0,distance));
		Vector3 rightMost = Camera.main.ViewportToWorldPoint (new Vector3 (1,0,distance));
		xmin = leftMost.x;
		xmax = rightMost.x;
	}
	
	// Update is called once per frame
	void Update () {
		if (movingRight) {			
			transform.position += Vector3.right * speed * Time.deltaTime;
		} else {
			transform.position += Vector3.left * speed * Time.deltaTime;
		}
		float lefEdgeFormation = transform.position.x - (width / 2);
		float rightEdgeFormation = transform.position.x + (width / 2);
		//float newX = Mathf.Clamp (transform.position.x, xmin, xmax);
		//transform.position = new Vector3 (newX, transform.position.y, transform.position.z);

		if (lefEdgeFormation < xmin) {
			movingRight = true;
		} else if (rightEdgeFormation > xmax) {
			movingRight = false;
		}

		if (allEnemiesDead ()) {
			Debug.Log ("All enemies are dead");
			spawnUntilFull ();
		}
	}

	public void OnDrawGizmos(){
		Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0f));
	}

	private bool allEnemiesDead(){
		
		foreach (Transform childPositionGameObject in transform) {
			if (childPositionGameObject.childCount > 0) {
				return false;
			}
		}
		return true;
	}

	private void spawnEnemies(){
		foreach (Transform child in transform) {
			GameObject enemy = Instantiate (enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = child;
		}
	}

	private void spawnUntilFull(){
		Transform freePosition = nextFreePosition ();
		if (freePosition) {
			GameObject enemy = Instantiate (enemyPrefab, freePosition.transform.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = freePosition;

		} 

		if (nextFreePosition ()) {
			Invoke ("spawnUntilFull", spawnDelay);
		}
	}

	private Transform nextFreePosition(){

		foreach (Transform childPositionGameObject in transform) {
			if (childPositionGameObject.childCount <1) {
				return childPositionGameObject;
			}
		}
		return null;
	}
}
