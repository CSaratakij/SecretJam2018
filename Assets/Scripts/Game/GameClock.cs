using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SC
{
    public class GameClock : MonoBehaviour
    {
        Timer timer;


        void Awake()
        {
            timer = GetComponent<Timer>();
            _Subscribe_Events();
        }

        void OnDestroy()
        {
            _Unsubscribe_Events();
        }

        void _OnGameStart()
        {
            timer.Countdown();
        }

        void _OnTimerStopped()
        {
            GameController.GameOver();
        }

        void _Subscribe_Events()
        {
            if (timer) {
                timer.OnTimerStopped += _OnTimerStopped;
            }
            else {
                Debug.Log("Can't find timer..");
            }

            GameController.OnGameStart += _OnGameStart;
        }

        void _Unsubscribe_Events()
        {
            if (timer) {
                timer.OnTimerStopped -= _OnTimerStopped;
            }
            else {
                Debug.Log("Can't find timer..");
            }

            GameController.OnGameStart -= _OnGameStart;
        }
    }
}
