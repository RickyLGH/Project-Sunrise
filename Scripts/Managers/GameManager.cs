using UnityEngine;

namespace CompleteProject
{
	public class GameManager :MonoBehaviour
    {
        public static GameManager instance;

        public bool runningMac = false;
        public bool runningWindows = false;

        void Awake()
        {
            //Check if instance already exists
            if (instance == null)

                //if not, set instance to this
                instance = this;

            //If instance already exists and it's not this:
            else if (instance != this)

                //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
                Destroy(gameObject);

            //Sets this to not be destroyed when reloading scene
            DontDestroyOnLoad(gameObject);

            if(Application.platform == RuntimePlatform.WindowsEditor)
        {
                runningWindows = true;
            }

            if (Application.platform == RuntimePlatform.OSXEditor)
            {
				runningMac = true;
            }
        }
    }
}
