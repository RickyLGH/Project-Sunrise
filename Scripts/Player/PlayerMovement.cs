using UnityEngine;

namespace CompleteProject
{
	public class PlayerMovement : MonoBehaviour
    {
        public float speed = 6f;			// The speed that the player will move at.

		public PlayerAttack turnonbitch;

        Vector3 movement;                   // The vector to store the direction of the player's movement.
        Animator anim;                      // Reference to the animator component.
        Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
        int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
        float camRayLength = 100f;          // The length of the ray from the camera into the scene.
        public string xMoveAxis;           // Name of the Horizontal Movement input from the InputManager for the Player.
        public string yMoveAxis;           // Name of the Vertical Movement input from the InputManager for the Player.
        public string xAimAxis_Win;             // Name of the Horizontal Aiming input from the InputManager for the Player.
        public string yAimAxis_Win;             // Name of the Vertical Aiming input from the InputManager for the Player.
        public string xAimAxis_Mac;             // Name of the Horizontal Aiming input from the InputManager for the Player.
        public string yAimAxis_Mac;             // Name of the Vertical Aiming input from the InputManager for the Player.
        Vector3 turnDir;

        void Awake ()
        {
            // Create a layer mask for the floor layer.
            floorMask = LayerMask.GetMask ("Floor");


            // Set up references. w
            anim = GetComponent <Animator> ();
            playerRigidbody = GetComponent <Rigidbody> ();
			turnonbitch.enabled = true;
        }


        void FixedUpdate ()
        {
            // Store the input axes.
            float h = Input.GetAxisRaw(xMoveAxis);
            float v = Input.GetAxisRaw(yMoveAxis);

			if (HealthManager.instance.allPlayersAlive) 
			{
				
				// Move the player around the scene.
				Move (h, v);

				// Turn the player to face the mouse cursor.
				Turning ();

				// Animate the player.
				Animating (h, v);
	
			}
        }


        void Move (float h, float v)
        {
            // Set the movement vector based on the axis input.
            movement.Set (h, 0f, v);
            
            // Normalise the movement vector and make it proportional to the speed per second.
            movement = movement.normalized * speed * Time.deltaTime;

            // Move the player to it's current position plus the movement.
            playerRigidbody.MovePosition (transform.position + movement);
        }


        void Turning ()
        {
            // Create a ray from the mouse cursor on screen in the direction of the camera.
			Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);

            // Create a RaycastHit variable to store information about what was hit by the ray.
            RaycastHit floorHit;

            // Perform the raycast and if it hits something on the floor layer...
            if(Physics.Raycast (camRay, out floorHit, camRayLength, floorMask))
            {
                // Create a vector from the player to the point on the floor the raycast from the mouse hit.
                Vector3 playerToMouse = floorHit.point - transform.position;

                // Ensure the vector is entirely along the floor plane.
                playerToMouse.y = 0f;

                // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
                Quaternion newRotatation = Quaternion.LookRotation (playerToMouse);

                // Set the player's rotation to this new rotation.
                playerRigidbody.MoveRotation (newRotatation);
            }

            if (GameManager.instance.runningWindows)
            {
                turnDir = new Vector3(Input.GetAxisRaw(xAimAxis_Win), 0f, Input.GetAxisRaw(yAimAxis_Win));
            }

            if (GameManager.instance.runningMac)
            {
                turnDir = new Vector3(Input.GetAxisRaw(xAimAxis_Mac), 0f, Input.GetAxisRaw(yAimAxis_Mac));
            }

            if (turnDir != Vector3.zero)
            {
                // Create a vector from the player to the point on the floor the raycast from the mouse hit.
                Vector3 playerToMouse = (transform.position + turnDir) - transform.position;

                // Ensure the vector is entirely along the floor plane.
                playerToMouse.y = 0f;

                // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
                Quaternion newRotatation = Quaternion.LookRotation(playerToMouse);

                // Set the player's rotation to this new rotation.
                playerRigidbody.MoveRotation(newRotatation);
            }
        }

        void Animating (float h, float v)
        {
            // Create a boolean that is true if either of the input axes is non-zero.
            bool walking = h != 0f || v != 0f;

            // Tell the animator whether or not the player is walking.
            anim.SetBool ("IsWalking", walking);
        }
    }
}