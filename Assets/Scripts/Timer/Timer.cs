﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SC
{
    public class Timer : MonoBehaviour
    {
        [SerializeField]
        bool isStart;

        [SerializeField]
        float current;

        [SerializeField]
        float maximum;


        public delegate void _Func();
        public event _Func OnTimerStopped;


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
            if (!isStart) {
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

        public void Stop()
        {
            isStart = false;

            if (OnTimerStopped != null) {
                OnTimerStopped();
            }
        }
    }
}