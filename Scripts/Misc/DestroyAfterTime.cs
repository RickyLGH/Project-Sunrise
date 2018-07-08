using UnityEngine;
using System.Collections;

public class DestroyAfterTime : MonoBehaviour
{
    public float timeBeforeDestroy;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(DestroyObject());
	}
	
    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(timeBeforeDestroy);
        Destroy(gameObject);
    }
}
