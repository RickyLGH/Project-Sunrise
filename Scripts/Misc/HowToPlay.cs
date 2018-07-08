using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HowToPlay : MonoBehaviour {
	public bool TimeFroze = true;
	public bool Game=true;
	public Canvas SKIDDYBAP;
	// Use this for initialization
	void Start () {
		Game = true;
		TimeFroze = true;

	}
	
	// Update is called once per frame
	void Update () {

		if ((TimeFroze == true) && (Input.GetButton("Submit"))){
			TimeFroze = false;
			SKIDDYBAP.enabled = !SKIDDYBAP.enabled;

		}
		if ((TimeFroze == true)) {
			Time.timeScale = 0;

		} else {
			Time.timeScale = 1;
		}
	}

}