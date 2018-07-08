using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CompleteProject
{
	public class Load : MonoBehaviour
	{
		public Image Ice;
		public Image Fire;
		public Image Air;

	    public bool iceSelected = false;
	    public bool fireSelected = false;
	    public bool airSelected = false;
	    public bool inConstruction = false;



	    private void Update()
		{
			print (airSelected);
			print (fireSelected);
			print (iceSelected);

			ConstructionSene ();

			Scene currentScene = SceneManager.GetActiveScene ();
			string sceneName = currentScene.name;

//	        if (Input.GetKeyDown(KeyCode.Space) && inConstruction)
//	        {
//	            inConstruction = false;
//	            SceneManager.LoadScene("Main Menu");
//	        }

			if (Input.GetButtonDown ("Fire_Select")) {
				Invoke ("FireSelect", 0);
				Fire.color = Color.red;
			}

			if (Input.GetButtonDown ("Air_Select")) {
				Invoke ("AirSelect", 0);
				Air.color = Color.green;
			}

			if (Input.GetButtonDown ("Ice_Select")) {
				Invoke ("IceSelect", 0);
				Ice.color = Color.blue;
			}

			if (iceSelected && fireSelected && airSelected) {
				Invoke ("StartGame", 1F);
			}
		}
	    void ConstructionSene()
		{
			Scene currentScene = SceneManager.GetActiveScene ();
			string sceneName = currentScene.name;

			if (sceneName == "How To Play" || sceneName == "Credits") {
				print ("This scene is currently under construction. Press Space to return to Main Menu");
				inConstruction = true;
			}
		}
		public void FireSelect(){
			fireSelected = true;
		}

		public void IceSelect(){
			iceSelected = true;
		}

		public void AirSelect(){
			airSelected = true;
		}

		public void StartGame(){
			SceneManager.LoadScene ("_Complete-Game");
		}
			
			
	    public void StartSelection()
	    {
	        SceneManager.LoadScene("Character selection");
		}

	    public void QuitGame()
	    {
	        Application.Quit();
	        print("Game Quit");
	    }


	    public void Credits()
	    {
	        //print("Credit Screen popup");
			SceneManager.LoadScene ("Credits");
	    }
	}
}