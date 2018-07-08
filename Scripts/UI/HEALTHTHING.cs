using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HEALTHTHING : MonoBehaviour {
	public GameObject PlayerHealthBar;
	public Transform target;
	public GameObject PlayerHealth;




	// Use this for initialization
	void Start () {
		PlayerHealth.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
	}

	private void Awake() {
		target = gameObject.transform;
	}

	// Update is called once per frame
	void Update () {
		PlayerHealth.transform.position  = Camera.main.WorldToScreenPoint(target.position);
	}
}