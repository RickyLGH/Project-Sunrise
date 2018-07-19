using UnityEngine;
using System.Collections;


namespace CompleteProject
{
    public class EnemyAttack : MonoBehaviour
    {
        public float timeBetweenAttacks = 0.5f;     // The time in seconds between each attack.
        public int attackDamage = 10;               // The amount of health taken away per attack.

        Animator anim;                              // Reference to the animator component.
        EnemyHealth enemyHealth;                    // Reference to this enemy's health.
        public EnemyMovement enemyMovement;         // Reference to this enemy's movement script.
        public bool playerInRange;                  // Whether the Ice char is within the trigger collider and can be attacked.
        public bool iceDamage;
        public bool fireDamage;
        public bool earthDamage;
        float timer;                                // Timer for counting up to the next attack.
        public PlayerShield Ps;
        [SerializeField]
        private GameObject[] targets;
        private GameObject playerToAtk;

        void Awake()
        {
            // Setting up the references.
            enemyHealth = GetComponent<EnemyHealth>();
            enemyMovement = GetComponent<EnemyMovement>();
            anim = GetComponent<Animator>();
            targets = new GameObject[3];
            targets[0] = GameObject.FindGameObjectWithTag("Ice");
            targets[1] = GameObject.FindGameObjectWithTag("Fire");
            targets[2] = GameObject.FindGameObjectWithTag("Earth");
            playerToAtk = targets[Random.Range(0, targets.Length)];
            Ps = playerToAtk.GetComponent<PlayerShield>();
        }


        void OnTriggerEnter(Collider other)
        {
            // If the entering collider is the player...
            if (other.gameObject.tag == "Ice")
            {
                // ... the player is in range.
                playerInRange = true;
                iceDamage = true;
                fireDamage = false;
                earthDamage = false;

            }
            if (other.gameObject.tag == "Fire")
            {
                // ... the player is in range.
                playerInRange = true;
                fireDamage = true;
                iceDamage = false;
                earthDamage = false;
            }
            if (other.gameObject.tag == "Earth")
            {
                // ... the player is in range.
                playerInRange = true;
                earthDamage = true;
                fireDamage = false;
                iceDamage = false;
            }
        }

        void OnTriggerExit(Collider other)
        {
            // If the entering collider is the player...
            if (other.gameObject.tag == "Fire")
            {
                // ... the player is in range.
                playerInRange = false;
                iceDamage = false;
            }
            if (other.gameObject.tag == "Ice")
            {
                // ... the player is in range.
                playerInRange = false;
                fireDamage = false;
            }
            if (other.gameObject.tag == "Earth")
            {
                // ... the player is in range.
                playerInRange = false;
                earthDamage = false;
            }
        }


        void Update()
        {
            anim.SetBool("Frozen", enemyMovement.frozen);

            // Add the time since Update was last called to the timer.
            timer += Time.deltaTime;

            // If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
            if (timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
            {
                // ... attack.
                Attack();
            }

            // If the player has zero or less health...
            if (!HealthManager.instance.allPlayersAlive)
            {
                // ... tell the animator the player is dead.
                anim.SetTrigger("PlayerDead");
            }
        }
        void Attack()
        {
            // Reset the timer.
            timer = 0f;
            if (Ps.Orb == true)
            {
                attackDamage = 3;
            }
            if (Ps.Orb == false)
            {
                attackDamage = 10;
                // If the player has health to lose...

                if (HealthManager.instance.allPlayersAlive && iceDamage == true)
                {
                    HealthManager.instance.iceHealth.TakeDamage(attackDamage);
                    HealthManager.instance.CheckUp();
                }

                if (HealthManager.instance.allPlayersAlive && fireDamage == true)
                {
                    HealthManager.instance.fireHealth.TakeDamage(attackDamage);
                    HealthManager.instance.CheckUp();
                }

                if (HealthManager.instance.allPlayersAlive && earthDamage == true)
                {
                    HealthManager.instance.earthHealth.TakeDamage(attackDamage);
                    HealthManager.instance.CheckUp();
                }

            }
        }
    } 
}
