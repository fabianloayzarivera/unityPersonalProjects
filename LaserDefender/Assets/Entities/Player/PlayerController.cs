using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public GameObject laserShot;
	public float speed = 15.0f;
	public float padding = 1f;
	public float laserSpeed = 0f;
	public float fireRate = 0.2f;
	public float health = 100f;
	public string levelToLoad = "Win Screen";
	private float xmin;
	private float xmax;
	private LevelManager levelManager;

	public AudioClip fireSound;
	public AudioClip deathSound;

	// Use this for initialization
	void Start () {
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftMost = Camera.main.ViewportToWorldPoint (new Vector3 (0,0,distance));
		Vector3 rightMost = Camera.main.ViewportToWorldPoint (new Vector3 (1,0,distance));
		xmin = leftMost.x + padding;
		xmax = rightMost.x - padding;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.LeftArrow)) {
			//transform.position += new Vector3 (-speed * Time.deltaTime, 0f, 0f);
			transform.position += Vector3.left * speed * Time.deltaTime;
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			//transform.position += new Vector3 (speed * Time.deltaTime, 0f, 0f);
			transform.position += Vector3.right * speed * Time.deltaTime;
		} 
		if (Input.GetKeyDown (KeyCode.Space)) {
			InvokeRepeating ("fire", 0.00001f, fireRate);

		}
		if (Input.GetKeyUp (KeyCode.Space)) {
			CancelInvoke ("fire");
		}
		float newX = Mathf.Clamp (transform.position.x, xmin, xmax);
		transform.position = new Vector3 (newX, transform.position.y, transform.position.z);
	}

	void fire(){
		Vector3 offset = new Vector3 (0f, 1f, 0f);
		GameObject shot = Instantiate (laserShot, transform.position + offset, Quaternion.identity) as GameObject;
		shot.GetComponent<Rigidbody2D>().velocity = new Vector3 (0f, laserSpeed, 0f);
		AudioSource.PlayClipAtPoint (fireSound, transform.position);
	}

	void OnTriggerEnter2D(Collider2D col){

		EnemyShotScript shot = col.gameObject.GetComponent<EnemyShotScript> ();
		if (shot != null) {
			print ("Player Hit by EnemyShot!");
			health -= shot.getDamage ();
			shot.hit ();
			if (health <= 0) {
				die ();
			}
		}

	}

	void die(){
		Destroy (gameObject);
		AudioSource.PlayClipAtPoint (deathSound, transform.position);
		levelManager = GameObject.Find ("LevelManager").GetComponent<LevelManager> ();
		levelManager.LoadLevel (levelToLoad);
	}
}
