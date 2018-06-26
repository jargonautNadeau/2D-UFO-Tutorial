using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCTalk : MonoBehaviour {
	public GameObject panel;
	private int index = 0;

	void Awake() {
		panel.SetActive (false);

	}

}
