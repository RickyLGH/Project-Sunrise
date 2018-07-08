using UnityEngine;


namespace CompleteProject
{

	public class Broom : MonoBehaviour
    {
        public float pushForce = 20f;

		public string attackButton_Win;
        public string attackButton_Mac;

        public int damagePerAttack = 5;

        public ParticleSystem Woosh;

        void OnTriggerEnter(Collider col)
        {
            if (!HealthManager.instance.earthHealth.isDead)
            {
                if (Input.GetButton(attackButton_Win))
                {
                    Woosh.Play();
                    Invoke("Stop", 0.5f);
                }
                if (Input.GetButton(attackButton_Win) && col.gameObject.tag == "Enemy" || col.gameObject.tag == "IceBlock")
                {

                    EnemyHealth enemyHealth = col.gameObject.GetComponent<EnemyHealth>();

                    Rigidbody rb = col.gameObject.GetComponent<Rigidbody>();

                    rb.AddForce(Vector3.forward * pushForce, ForceMode.Impulse);
                    rb.constraints = RigidbodyConstraints.None;
                    enemyHealth.TakeDamage(damagePerAttack);
                    Invoke("StopFalling", .9f);
                }

                if (Input.GetButton(attackButton_Mac))
                {
                    Woosh.Play();
                    Invoke("Stop", 0.5f);
                }

                if (Input.GetButton(attackButton_Mac) && col.gameObject.tag == "Enemy" || col.gameObject.tag == "IceBlock")
                {

                    EnemyHealth enemyHealth = col.gameObject.GetComponent<EnemyHealth>();

                    Rigidbody rb = col.gameObject.GetComponent<Rigidbody>();

                    rb.AddForce(Vector3.forward * pushForce, ForceMode.Impulse);
                    rb.constraints = RigidbodyConstraints.None;
                    enemyHealth.TakeDamage(damagePerAttack);
                    Invoke("StopFalling", 0.9f);
                }
            }
        }

        void Stop()
        {
            Woosh.Stop();
        }

        void StopFalling(Collider col)
        {
            if (col.gameObject.tag == "IceBlock")
            {
                col.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            }

            if (col.gameObject.tag == "Enemy")
            {
                col.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            }
        }
    }
}
