using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace CompleteProject
{
    public class RopedTest : MonoBehaviour
    {
        public Transform ice;
        public Transform fire;
        public Transform air;

        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(ice.position, fire.position);
            Gizmos.DrawLine(air.position, ice.position);
            Gizmos.DrawLine(air.position, fire.position);
        }
    }
}
