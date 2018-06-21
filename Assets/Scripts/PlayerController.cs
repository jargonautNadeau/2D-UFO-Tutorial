using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
	public float speed = 5.0f;
	public Text countText;
	public Text scoreText;
	public Text winText;

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
		if (count >= 5) {
			wonGame = true;
		}
	}
		
	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log ("Tag: " + other.gameObject.tag);
		if (other.gameObject.CompareTag("PickUp")) {
			other.gameObject.SetActive (false);
			count += 1;
			score += 15;
			UpdateUI ();
		} else if (other.gameObject.CompareTag("PowerUp")){
			//Powerup
			Debug.Log("POWERUP");
			other.gameObject.SetActive (false);
			speed += 100.0f;
		}
	}
	void UpdateUI(){
		countText.text = "Count: " + count.ToString();
		scoreText.text = "Score: " + score.ToString();
		if (wonGame) {
			winText.text = "You Win!!!";
		}
	}
}
