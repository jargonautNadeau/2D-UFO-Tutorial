using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour {
	public float speed = 3000.0f;
	// Use this for initialization
	void Start () {
		float x = Random.Range(-1f, 1f);
		float y = Random.Range(-1f, 1f);
		Vector2 initialPush = new Vector2(x, y);
		//if you need the vector to have a specific length:
		initialPush = initialPush.normalized * speed;
		gameObject.GetComponent<Rigidbody2D> ().AddForce (initialPush);
	}

}
