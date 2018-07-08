using UnityEngine;
using UnityEngine.SceneManagement;

namespace CompleteProject
{
	public class HealthManager :MonoBehaviour
	{
		public static HealthManager instance;

		public PlayerHealth iceHealth;       // Reference to the Ice player's health.
		public PlayerHealth fireHealth;       // Reference to the Fire player's health.
		public PlayerHealth earthHealth;       // Reference to the Earth player's health.
		public bool allPlayersAlive = true;
		public GameObject[] enemiesOnScreen;

		void Awake ()
		{
			//Check if instance already exists
			if (instance == null)

				//if not, set instance to this
				instance = this;

			//If instance already exists and it's not this:
			else if (instance != this)

				//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
				//Destroy(gameObject);

			//Sets this to not be destroyed when reloading scene
			DontDestroyOnLoad(gameObject);
		}
		
		// Update is called once per frame
		void Update () 
		{
			enemiesOnScreen = GameObject.FindGameObjectsWithTag("Enemy");

			Scene currentScene = SceneManager.GetActiveScene();
			string sceneName = currentScene.name;

			if (sceneName == "Main Menu") 
			{
				allPlayersAlive = true;
			}
		}

		public void CheckUp()
		{
			if (iceHealth.currentHealth <= 0 && fireHealth.currentHealth <= 0 && earthHealth.currentHealth <= 0)
			{
				allPlayersAlive = false;
			}
		}
	}
}
