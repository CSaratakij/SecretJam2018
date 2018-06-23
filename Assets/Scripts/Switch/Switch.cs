using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SC
{
    public class Switch : MonoBehaviour
    {
        [SerializeField]
        UnityEvent OnTurnOn;

        [SerializeField]
        UnityEvent OnTurnOff;


        bool isTurnOn;


        public bool IsTurnOn => isTurnOn;


        public void TurnOn()
        {
            if (isTurnOn) { return; }
            isTurnOn = true;
            OnTurnOn?.Invoke();
        }

        public void TurnOff()
        {
            if (!isTurnOn) { return; }
            isTurnOn = false;
            OnTurnOff?.Invoke();
        }

        public void Toggle()
        {
            isTurnOn = !isTurnOn;
        }
    }
}
