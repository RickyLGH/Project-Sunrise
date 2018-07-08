using UnityEngine;
using UnityEngine.SceneManagement;


namespace CompleteProject
{
	public class GameOverManager : MonoBehaviour
    {
        Animator anim;                          // Reference to the animator component.


        void Awake ()
        {
            // Set up the reference.
            anim = GetComponent <Animator> ();
        }


        void Update ()
        {
            // If the player has run out of health...
			if(!HealthManager.instance.allPlayersAlive)
            {
                // ... tell the animator the game is over.
                anim.SetTrigger ("GameOver");
            }
        }

        public void RestartLevel ()
        {
            // Reload the level that is currently loaded.
            SceneManager.LoadScene (0);
        }
    }
}