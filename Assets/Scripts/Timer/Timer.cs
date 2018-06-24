using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SC
{
    public class Timer : MonoBehaviour
    {
        [SerializeField]
        bool isPause;

        [SerializeField]
        float current;

        [SerializeField]
        float maximum;


        public delegate void _Func();
        public event _Func OnTimerStopped;

        public float Current => current;
        public float Maximum => maximum;


        bool isStart;


        void Update()
        {
            _Countdown_Handler();
        }

        void OnDestroy()
        {
            OnTimerStopped = null;
        }

        void _Countdown_Handler()
        {
            if (!isStart || isPause) {
                return;
            }

            current -= 1.0f * Time.deltaTime;

            if (current <= 0.0f) {
                current = 0.0f;
                Stop();
            }
        }

        public void Countdown()
        {
            if (isStart) { return; }
            current = maximum;
            isStart = true;
        }

        public void Pause(bool value)
        {
            isPause = value;
        }

        public void Stop()
        {
            if (!isStart) { return; }
            isStart = false;
            OnTimerStopped?.Invoke();
        }
    }
}
