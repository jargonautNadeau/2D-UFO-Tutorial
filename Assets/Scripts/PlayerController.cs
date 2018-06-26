using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
	public float speed = 5.0f;
	public Text countText;
	public Text scoreText;
	public Text winText;
	public float interactionRange = 300.0f;
	public int winTotal = 300;

	private bool wonGame;
	private int count;
	private int score;
	private float initialSpeed;
	private Rigidbody2D rb2d;

	void Start() {
		rb2d = GetComponent<Rigidbody2D> ();
		initialSpeed = speed;
		count = 0;
		score = 0;
		wonGame = false;
		winText.text = "";
		UpdateUI ();
	}

	void FixedUpdate(){
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");
		Vector2 movement = new Vector2 (h, v);
		rb2d.AddForce (movement * speed);
		if(speed > initialSpeed){
			speed -= 1.0f;
		}
		if (score >= winTotal) {
			wonGame = true;
		}
		if (Input.GetKeyUp(KeyCode.F)) {
			Debug.Log ("Pressed F");
			GameObject closestInteractable = FindClosestInteractable ();
			if (closestInteractable != null) {
				if (closestInteractable.CompareTag ("Portal")) {
					closestInteractable.GetComponent<Portal> ().buyPortal();
				}
			}
		}
		UpdateUI ();
	}
	void OnTriggerStay2D(Collider2D other) {
		Debug.Log ("In Collider for: "+other.gameObject.name);
		if (Input.GetKeyUp (KeyCode.E)) {
			Debug.Log ("Pressed E");
			if (other.gameObject.CompareTag ("Portal")) {
				Portal p = other.gameObject.GetComponent<Portal> ();
				Debug.Log ("Portal "+other.gameObject.name+" is locked? "+p.locked);
				if (!p.locked) {
					Debug.Log ("Pressed E");
					transform.position = p.targetPortal.transform.position;
				}
			}
		}
	}
	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log ("Tag: " + other.gameObject.tag);
		if (other.gameObject.CompareTag ("PickUp")) {
			other.gameObject.SetActive (false);
			addCount (1);
			score += 15;
		} else if (other.gameObject.CompareTag ("PowerUp")) {
			//Powerup
			Debug.Log ("POWERUP");
			other.gameObject.SetActive (false);
			speed += 100.0f;
		} else if (other.gameObject.CompareTag ("Portal")) {
			
		}
	}

	void UpdateUI(){
		countText.text = "Count: " + count.ToString();
		scoreText.text = "Score: " + score.ToString();
		if (wonGame) {
			winText.text = "You Win!!!";
		}
	}

	public int getCount() {
		return count;
	}

	public void addCount(int deltaValue) {
		if ((count + deltaValue) < 0) {
			count = 0;	
		} else {
			count += deltaValue;
		}
	}

	public GameObject FindClosestInteractable() {
		GameObject[] gos = GameObject.FindGameObjectsWithTag("Portal");
		GameObject closest = null;
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;
		foreach (GameObject go in gos) {
			Vector3 diff = go.transform.position - position;
			float curDistance = diff.sqrMagnitude;
			Debug.Log (go.name+" is "+curDistance+" away");
			if (curDistance < distance) {
				closest = go;
				distance = curDistance;
			}
		}

		if (distance < interactionRange) {
			return closest;
		} else {
			return null;
		}
	}
}
