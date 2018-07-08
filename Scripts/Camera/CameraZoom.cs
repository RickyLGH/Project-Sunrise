using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

namespace CompleteProject
{
	[RequireComponent(typeof(Camera))]
	public class CameraZoom : NetworkBehaviour 
	{
		public List<Transform> targets;

		public Vector3 offset;
		public float smoothTime = .5f;

		public float minZoom = 10f;
		public float maxZoom = 40f;
		public float zoomLimiter = 50f;

		private Vector3 veclocity;
		private Camera cam;
        private GameObject ice;
        private GameObject fire;
        private GameObject air;

		void Start()
		{
			cam = GetComponent<Camera> ();
        }

        private void Update()
        {
            ice = GameObject.FindGameObjectWithTag("Ice");
            fire = GameObject.FindGameObjectWithTag("Fire");
            air = GameObject.FindGameObjectWithTag("Earth");

			targets[0] = ice.transform;
            targets[1] = fire.transform;
            targets[2] = air.transform;

        }

        void LateUpdate ()
		{
			if (targets.Count == 0) 
			{
				return;
			}
			
			Move ();
			//Zoom ();
		}

		//void Zoom ()
		//{
		//	float newZoom = Mathf.Lerp (maxZoom, minZoom, GetGreatestDistance () / zoomLimiter);
		//	cam.fieldOfView = Mathf.Lerp (cam.fieldOfView, newZoom, Time.deltaTime);
		//}

		void Move()
		{
			Vector3 centerPoint = GetCenterPoint ();

			Vector3 newPosition = centerPoint + offset;

			transform.position = Vector3.SmoothDamp (transform.position, newPosition, ref veclocity, smoothTime);
		}

		float GetGreatestDistance()
		{
			var bounds = new Bounds(targets[0].position, Vector3.zero);
			for(int i = 0; i < targets.Count; i++)
			{
				bounds.Encapsulate (targets[i].position);
			}

			return bounds.size.x;
		}

		Vector3 GetCenterPoint ()
		{
			if (targets.Count == 1) 
			{
				return targets [0].position;
			}

			var bounds = new Bounds (targets [0].position, Vector3.zero);
			for (int i = 0; i < targets.Count; i++)
			{
				bounds.Encapsulate (targets[i].position);
			}

			return bounds.center;
		}

//		private Camera cameraRef;
//	    public GameObject[] playerPos;
//		Vector3 zeroRot = Vector3.zero;
//
//	    void Start()
//	    {
//	        cameraRef = GetComponent<Camera>();
//			//playerPos = GameObject.FindGameObjectsWithTag("Player");
//	        //Debug.Log(playerPos[0].transform.position);
//	        //Debug.Log(playerPos[1].transform.position);
//	        //Debug.Log(playerPos[2].transform.position);
//	        //Debug.Log(cameraRef.tag);
//	        StartCoroutine(ZoomInOut());
//	    }
//
//	    IEnumerator ZoomInOut()
//	    {
//	        while (true)
//	        {
//	            if (playerPos[0] != null && playerPos[1] != null && playerPos[2] != null)
//	            {
//					Vector3 lookPoint = Vector3.Lerp(playerPos[0].transform.position, playerPos[2].transform.position, 0.5f);
//	                cameraRef.transform.LookAt(lookPoint);
//
//					float distance = Vector3.Distance(playerPos[0].transform.position, playerPos[2].transform.position);
//	                if (distance > (cameraRef.orthographicSize * 2))
//	                {
//	                    cameraRef.orthographicSize += 0.05f;
//	                     if (distance < (cameraRef.orthographicSize * 2))
//	                    {
//	                        cameraRef.orthographicSize -= 0.1f;
//	                    }
//	                }
//	                else
//
//	                if (distance < (cameraRef.orthographicSize))
//	                {
//	                    cameraRef.orthographicSize -= 0.05f;
//	                }
//
//	                yield return new WaitForSeconds(0.02f);
//	            }
//	        }
//	    }
	}
}