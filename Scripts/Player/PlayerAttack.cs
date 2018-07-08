using UnityEngine;

namespace CompleteProject
{
	public class PlayerAttack : MonoBehaviour
    {
        public int damagePerAttack = 20;                  // The damage inflicted by each bullet.
        public float timeBetweenAttacks = 0.1f;        // The time between each shot.
        public float range = 100f;  // The distance the gun can fire.


        public GameObject Light;

        public float throwForce = 40f;

        public GameObject ShootHereAsshole;

        float timer; // A timer to determine when to fire.
        Ray shootRay = new Ray();                       // A ray from the gun end forwards.
        RaycastHit shootHit;                            // A raycast hit to get information about what was hit.
        int attackableMask;                             // A layer mask so the raycast only hits things on the shootable layer.
        [SerializeField]
        ParticleSystem gunParticles;
        LineRenderer gunLine;                           // Reference to the line renderer.
        AudioSource gunAudio;                           // Reference to the audio source.
        Light gunLight;                                 // Reference to the light component.
        public Light faceLight;								// Duh
        public float effectsDisplayTime = 0.2f;
        [SerializeField]
        // The proportion of the timeBetweenBullets that the effects will display for.
        public string attackButton_Win;//Name of the the shoot button for the Player in the InputManager.
        public string lightButton_Win;
        public string attackButton_Mac;//Name of the the shoot button for the Player in the InputManager.
        public string lightButton_Mac;
        public float slowedEnemySpeed;
        private bool isIce = false;
        private bool isFire = false;
        private bool isAir = false;
        public GameObject fireBall;
        public GameObject gun;
		public bool broomPushed = false;
        public MonoBehaviour broomScript;

        void Awake()
        {
            // Create a layer mask for the Shootable layer.
            attackableMask = LayerMask.GetMask("Shootable");

            // Set up the references.
            gunLine = GetComponent<LineRenderer>();
            gunAudio = GetComponent<AudioSource>();
            gunLight = GetComponent<Light>();
            //faceLight = GetComponentInChildren<Light> ();
            Light.SetActive(false);

            if (gameObject.name == "GunBarrelEnd_Ice")
            {
                isIce = true;
                isFire = false;
                isAir = false;

                ShootHereAsshole = gameObject;
            }

            if (gameObject.name == "GunBarrelEnd_Fire")
            {
                isIce = false;
                isFire = true;
                isAir = false;
            }

            if (gameObject.name == "GunBarrelEnd_Earth")
            {
                isIce = false;
                isFire = false;
                isAir = true;

                ShootHereAsshole = gameObject;
            }
        }

        void Update()
        {
            // Add the time since Update was last called to the timer.
            timer += Time.deltaTime;

            AttackButtonHandler();
            LightButtonHandler();

            // If the timer has exceeded the proportion of timeBetweenBullets that the effects should be displayed for...
            if (timer >= timeBetweenAttacks * effectsDisplayTime)
            {
                // ... disable the effects.
                DisableEffects();
            }
 
        }

        void AttackButtonHandler()
        {
            if (GameManager.instance.runningWindows)
            {
                if (Input.GetButtonDown(attackButton_Win) && timer >= timeBetweenAttacks && Time.timeScale != 0 && HealthManager.instance.allPlayersAlive && isIce)
                {
                    // ... shoot the gun.
                    IceShoot();
                }

                if (Input.GetButton(attackButton_Win) && timer >= timeBetweenAttacks && Time.timeScale != 0 && HealthManager.instance.allPlayersAlive && isFire)
                {
                    // ... shoot the gun.
                    Fireball();
                }

                if (Input.GetButton(attackButton_Win) && timer >= timeBetweenAttacks && Time.timeScale != 0 && HealthManager.instance.allPlayersAlive && isAir)
                {
                    // ... shoot the gun.
                    BroomPush();
                    broomPushed = true;
                }

                if (Input.GetAxis(attackButton_Win) > 0 && timer >= timeBetweenAttacks && Time.timeScale != 0 && HealthManager.instance.allPlayersAlive && isIce)
                {
                    // ... shoot the gun.
                    IceShoot();
                }

                if (Input.GetAxis(attackButton_Win) > 0 && timer >= timeBetweenAttacks && Time.timeScale != 0 && HealthManager.instance.allPlayersAlive && isFire)
                {
                    // ... shoot the gun.
                    Fireball();
                }

                if (Input.GetAxis(attackButton_Win) > 0 && timer >= timeBetweenAttacks && Time.timeScale != 0 && HealthManager.instance.allPlayersAlive && isAir)
                {
                    // ... shoot the gun.
                    BroomPush();
                    broomPushed = true;
                }

                if (Input.GetButtonUp(attackButton_Win))
                {
                    broomPushed = false;
                }
            }

            if (GameManager.instance.runningMac)
            {
				if (Input.GetButtonDown(attackButton_Mac) && timer >= timeBetweenAttacks && Time.timeScale != 0 && HealthManager.instance.allPlayersAlive && isIce)
				{
					// ... shoot the gun.
					IceShoot();
				}

				if (Input.GetButton(attackButton_Mac) && timer >= timeBetweenAttacks && Time.timeScale != 0 && HealthManager.instance.allPlayersAlive && isFire)
				{
					// ... shoot the gun.
					Fireball();
				}

				if (Input.GetButton(attackButton_Mac) && timer >= timeBetweenAttacks && Time.timeScale != 0 && HealthManager.instance.allPlayersAlive && isAir)
				{
					// ... shoot the gun.
					BroomPush();
					broomPushed = true;
				}
                if (Input.GetAxis(attackButton_Mac) > 0 && timer >= timeBetweenAttacks && Time.timeScale != 0 && HealthManager.instance.allPlayersAlive && isIce)
                {
                    // ... shoot the gun.
                    IceShoot();
                }

                if (Input.GetAxis(attackButton_Mac) > 0 && timer >= timeBetweenAttacks && Time.timeScale != 0 && HealthManager.instance.allPlayersAlive && isFire)
                {
                    // ... shoot the gun.
                    Fireball();
                }

                if (Input.GetAxis(attackButton_Mac) > 0 && timer >= timeBetweenAttacks && Time.timeScale != 0 && HealthManager.instance.allPlayersAlive && isAir)
                {
                    // ... shoot the gun.
                    BroomPush();
                    broomPushed = true;
                }

                if (Input.GetAxis(attackButton_Mac) == 0)
                {
                    broomPushed = false;
                }
            }
        }

        void LightButtonHandler()
        {
            if (GameManager.instance.runningWindows)
            {
                if (Input.GetButtonDown(lightButton_Win))
                {
                    Light.SetActive(true);
                    Invoke("TurnLightOff", 1f);
                }
            }

            if (GameManager.instance.runningMac)
            {
                if (Input.GetButtonDown(lightButton_Mac))
                {
                    Light.SetActive(true);
                    Invoke("TurnLightOff", 1f);
                }
            }
        }

        public void DisableEffects()
        {
            // Disable the line renderer and the light.
            gunLine.enabled = false;
            faceLight.enabled = false;
            gunLight.enabled = false;
        }

        void TurnLightOff()
        {
            Light.SetActive(false);
        }

        void IceShoot()
        {
            // Reset the timer.
            timer = 0f;

            // Play the gun shot audioclip.
            gunAudio.Play();

            // Enable the lights.
            gunLight.enabled = true;
            faceLight.enabled = true;

            // Stop the particles from playing if they were, then start the particles.
            gunParticles.Stop();
            gunParticles.Play();

            // Enable the line renderer and set it's first position to be the end of the gun.
            gunLine.enabled = true;
            gunLine.SetPosition(0, transform.position);

            // Set the shootRay so that it starts at the end of the gun and points forward from the barrel.
            shootRay.origin = transform.position;
            shootRay.direction = transform.forward;

            // Perform the raycast against gameobjects on the shootable layer and if it hits something...
            if (Physics.Raycast(shootRay, out shootHit, range, attackableMask))
            {
                // Try and find an EnemyHealth script on the gameobject hit.
                EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();
                EnemyMovement enemyMovement = shootHit.collider.GetComponent<EnemyMovement>();

                // If the EnemyHealth component exist...
                if (enemyHealth != null && enemyMovement != null && !enemyMovement.frozen && !enemyMovement.slowed)
                {
                    // ... the enemy should take damage.
                    enemyHealth.Shot(damagePerAttack, shootHit.point);
                    enemyMovement.hitsUntilFrozen--;
                    enemyMovement.slowed = true;
                }

                if (enemyHealth != null && enemyMovement != null && !enemyMovement.frozen && enemyMovement.slowed)
                {
                    // ... the enemy should take damage.
                    enemyHealth.Shot(damagePerAttack, shootHit.point);
                    enemyMovement.hitsUntilFrozen--;
                }

                if (enemyHealth != null && enemyMovement != null && enemyMovement.frozen && enemyMovement.slowed)
                {
                    // ... the enemy should take damage.
                    enemyHealth.Shot(damagePerAttack, shootHit.point);
                }

                else if (enemyHealth == null && enemyMovement == null)
                {
                    //					enemyMovement.nav.speed = enemySpeed;
                    return;
                }

                // Set the second position of the line renderer to the point the raycast hit.
                gunLine.SetPosition(1, shootHit.point);
            }
            // If the raycast didn't hit anything on the shootable layer...
            else
            {
                // ... set the second position of the line renderer to the fullest extent of the gun's range.
                gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
            }
        }

        void Fireball()
        {
            // Reset the timer.
            timer = 0f;


            // Enable the lights.
            gunLight.enabled = true;
            faceLight.enabled = true;

            // Stop the particles from playing if they were, then start the particles.
            gunParticles.Stop();
            gunParticles.Play();

            // Enable the line renderer and set it's first position to be the end of the gun.
            gunLine.enabled = true;

            gunLine.SetPosition(0, ShootHereAsshole.transform.position);

            // Set the shootRay so that it starts at the end of the gun and points forward from the barrel.
            shootRay.origin = transform.position;
            shootRay.direction = transform.forward;

            GameObject _fireball = Instantiate(fireBall, gun.transform.position, gun.transform.rotation);
            Rigidbody rb = _fireball.GetComponent<Rigidbody>();
			rb.AddForce(Vector3.forward * throwForce);
            _fireball.transform.position = Vector3.MoveTowards(gun.transform.position, ShootHereAsshole.transform.position, throwForce * Time.deltaTime);
        }

        void BroomPush()
        {
            // Reset the timer.
            timer = 0f;

            // Enable the lights.
            gunLight.enabled = true;
            faceLight.enabled = true;

            // Enable the line renderer and set it's first position to be the end of the gun.
            gunLine.enabled = true;

            gunLine.SetPosition(0, transform.position);

            // Set the shootRay so that it starts at the end of the gun and points forward from the barrel.
            shootRay.origin = transform.position;
            shootRay.direction = transform.forward;

            if (Physics.Raycast(shootRay, out shootHit, range, attackableMask))
            {
                // Try and find an EnemyHealth script on the gameobject hit.
                EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();

                Debug.Log("Enemy Damaged");

                enemyHealth.Shot(damagePerAttack, shootHit.point);
            }

            if (broomPushed)
            {
                broomScript.gameObject.SetActive(true);
            }

            else
            {
				broomScript.gameObject.SetActive(false);
            }
        }
    }
}