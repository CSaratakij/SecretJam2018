using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SC
{
    public class RectDetector : MonoBehaviour
    {
        [SerializeField]
        Vector3 offset;

        [SerializeField]
        Vector2 size;
        
        [SerializeField]
        LayerMask layerMask;


        public int TotalFound { get; private set; }
        public bool IsFound => TotalFound > 0;


        void FixedUpdate()
        {
            //OverlapBox non alloc
        }
    }
}
