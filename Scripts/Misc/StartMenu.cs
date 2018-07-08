using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown) {
			SceneManager.LoadScene ("Character selection");
		}
	}	    public void StartSelection()
	{
		SceneManager.LoadScene("Character selection");
	}

	public void QuitGame()
	{
		Application.Quit();
		print("Game Quit");
	}
}
