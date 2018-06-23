using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
    using UnityEditor;
#endif

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


        public delegate void _Func(GameObject obj);

        public event _Func OnEnter;
        public event _Func OnStay;
        public event _Func OnExit;

        public int TotalFound { get; private set; }
        public bool IsFound => TotalFound > 0;


        bool isEntered;
        Collider2D[] results;


#if UNITY_EDITOR
        void OnDrawGizmos() {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(transform.position + offset, size);
        }
#endif
        void Awake()
        {
            results = new Collider2D[1];
        }

        void OnDestroy()
        {
            OnEnter = null;
            OnStay = null;
            OnExit = null;
        }

        void FixedUpdate()
        {
            _Collision_Handler();
        }

        void _Collision_Handler()
        {
            TotalFound = Physics2D.OverlapBoxNonAlloc(transform.position + offset, size, 0.0f, results, layerMask);
            
            if (IsFound) {
                if (!isEntered) {
                    isEntered = true;
                    _FireEvent_OnEnter(results[0].gameObject);
                }

                _FireEvent_OnStay(results[0].gameObject);
            }
            else {
                if (isEntered) {
                    isEntered = false;
                    _FireEvent_OnExit(results[0].gameObject);
                }
            }
        }

        void _FireEvent_OnEnter(GameObject obj)
        {
            OnEnter?.Invoke(obj);
        }

        void _FireEvent_OnStay(GameObject obj)
        {
            OnStay?.Invoke(obj);
        }

        void _FireEvent_OnExit(GameObject obj)
        {
            OnExit?.Invoke(obj);
        }
    }
}
