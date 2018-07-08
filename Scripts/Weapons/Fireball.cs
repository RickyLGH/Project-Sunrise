using UnityEngine;

namespace CompleteProject
{
	public class Fireball : MonoBehaviour
    {
        bool hasImpacted;
        public GameObject fireEffect;
		public GameObject Explo;
        public float radius = 50;
        public int damage;
        Rigidbody rb;
		[SerializeField]
        GameObject target;
		public float time = 0f;


        // Use this for initialization
        void Start()
		{
			time = Time.deltaTime;
            rb = GetComponent<Rigidbody>();

			rb.AddForce(Vector3.forward,ForceMode.Acceleration);
        }

        private void Update()
        {
			transform.Translate(target.transform.position * time);


		}

        //private void OnTriggerEnter(Collider other)
        //{
        //    if (other.gameObject.tag == "Target")
        //    {
        //        Impact();
        //        hasImpacted = true;
        //    }
        //}

        private void OnCollisionEnter(Collision collision)
        {
            Impact();
            hasImpacted = true;
        }

        void Impact()
        {
			Instantiate (Explo, transform.position, transform.rotation);
            Instantiate(fireEffect, transform.position, transform.rotation);

            Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

			Debug.Log("I IS HEAR");

            foreach (Collider nearbyObjects in colliders)
            {
                EnemyHealth enemyHealth = nearbyObjects.GetComponent<EnemyHealth>();

                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(damage);
                }
            }
            Destroy(gameObject);
        }
    }
}
