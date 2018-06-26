using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {

	public GameObject targetPortal;
	public bool visible;
	public bool locked;
	public int gemCost = 0;
	private PlayerController player;

	void Awake() {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
		if (targetPortal == null) {
			locked = true;
		}
	}

	public void buyPortal() {
		if (locked && player.getCount () >= gemCost) { 
			player.addCount (-gemCost);
			locked = false;
			visible = true;
		}
	}

}
