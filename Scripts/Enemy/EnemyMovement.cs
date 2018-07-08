using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;


namespace CompleteProject
{
	public class EnemyMovement : MonoBehaviour
    {
        public GameObject[] players;
        EnemyHealth enemyHealth;
		Broom BroomThing;                                   // Reference to this enemy's health.
		public UnityEngine.AI.NavMeshAgent nav;            // Reference to the nav mesh agent.

        public Transform target;
        public int targetNum;
		public SkinnedMeshRenderer Skin;
		public Material Frozen, Normal;
		public Transform MahHead;
		public Rigidbody rb;

		public bool slowed = false;
		public bool frozen = false;
        public int hitsUntilFrozen = 3;
		public float slowedSpeed1 = 1.5f;
        public float slowedSpeed2 = .5f;


        void Awake ()
        {
            // Set up the references.
            enemyHealth = GetComponent <EnemyHealth> ();
            players = GameObject.FindGameObjectsWithTag("Player");
            nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();
			rb = GetComponent <Rigidbody> ();

            players[0] = GameObject.FindGameObjectWithTag("Ice");
            players[1] = GameObject.FindGameObjectWithTag("Fire");
            players[2] = GameObject.FindGameObjectWithTag("Earth");

            targetNum = Random.Range(0, 3);
        }

        void Start()
        {
            target = players[targetNum].transform;
        }

        void Update ()
        {
			PathToPlayer ();
			SlowedFrozenCheck ();
			if (frozen) {
				//Frozen.transform.position = MahHead.transform.localPosition;
				//Frozen.transform.IsChildOf (MahHead);
				Skin.material = Frozen;

			}else {
				Skin.material = Normal;
			}
        }

        private void FixedUpdate()
        {
            Mathf.Clamp(rb.velocity.x, 1f, 10f);
            Mathf.Clamp(rb.velocity.y, 1f, 10f);
        }

        void PathToPlayer()
		{
			// If the enemy and the player have health left...
			if(enemyHealth.currentHealth > 0 && HealthManager.instance.allPlayersAlive && hitsUntilFrozen > 0)
			{
				// ... set the destination of the nav mesh agent to the player.
				nav.SetDestination (target.position);
				//Debug.Log(gameObject.name + " moving towards " + target.name);
			}
			// Otherwise...
			else
			{
				// ... disable the nav mesh agent.
				nav.enabled = false;
			}
		}

		void SlowedFrozenCheck()
		{
			if (slowed && hitsUntilFrozen == 2 && !frozen) 
			{
				nav.speed = slowedSpeed1;
                Debug.Log(gameObject.name + " nav speed is " + nav.speed);
			}

            if (slowed && hitsUntilFrozen == 1 && !frozen)
            {
                nav.speed = slowedSpeed2;
                Debug.Log(gameObject.name + " nav speed is " + nav.speed);
            }

            if (slowed && hitsUntilFrozen == 0 && !frozen) 
			{
				rb.constraints = RigidbodyConstraints.FreezeAll;
				nav.speed = 0;
                Debug.Log(gameObject.name + " nav speed is " + nav.speed);
                frozen = true;
				Instantiate (Frozen);
			}
		}
    }
}
