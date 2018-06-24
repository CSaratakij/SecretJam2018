using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SC
{
    public class PressurePlate : MonoBehaviour
    {
        [SerializeField]
        bool isUseToggleSwitch;

        RectDetector rectDetector;
        Switch switchComponent;


        void Awake()
        {
            rectDetector = GetComponent<RectDetector>();
            switchComponent = GetComponent<Switch>();

            _Subscribe_Events();
        }

        void _OnEnter(GameObject obj)
        {
            switchComponent.Toggle();
        }

        void _OnExit(GameObject obj)
        {
        }

        void OnDestroy()
        {
            _Unsubscribe_Events();
        }

        void _Subscribe_Events()
        {
            rectDetector.OnEnter += _OnEnter;
            rectDetector.OnExit += _OnExit;
        }

        void _Unsubscribe_Events()
        {
            rectDetector.OnEnter -= _OnEnter;
            rectDetector.OnExit -= _OnExit;
        }
    }
}
