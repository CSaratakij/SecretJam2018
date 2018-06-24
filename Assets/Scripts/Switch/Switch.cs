using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SC
{
    public class Switch : MonoBehaviour
    {
        public delegate void _Func();

        public event _Func OnTurnOn;
        public event _Func OnTurnOff;


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
            
            if (isTurnOn) {
                TurnOn();
            }
            else {
                TurnOff();
            }
        }

        public void Toggle(bool value)
        {
            isTurnOn = value;
        }
    }
}
