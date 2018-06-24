using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SC
{
    public class Bullet : MonoBehaviour
    {
        Timer timer;
        RectDetector rectDetector;
        MoveAgent moveAgent;


        void Awake()
        {
            timer = GetComponent<Timer>();
            rectDetector = GetComponent<RectDetector>();
            moveAgent = GetComponent<MoveAgent>();

            _Subscribe_Events();
        }

        void OnEnable()
        {
            moveAgent.AllowMove(true);
        }

        void OnDisable()
        {
            moveAgent.AllowMove(false);
        }

        void OnDestroy()
        {
            _Unsubscribe_Events();
        }

        void _Subscribe_Events()
        {
            timer.OnTimerStopped +=_OnTimerStopped;
            rectDetector.OnEnter += _OnEnter;
        }

        void _Unsubscribe_Events()
        {
            timer.OnTimerStopped -=_OnTimerStopped;
            rectDetector.OnEnter -= _OnEnter;
        }

        void _OnEnter(GameObject obj)
        {
            timer.Countdown();
        }

        void _OnTimerStopped()
        {
            gameObject.SetActive(false);
        }
    }
}
