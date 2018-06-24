using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
    using UnityEditor;
#endif

namespace SC
{
    public class Dispensor : MonoBehaviour
    {
        [SerializeField]
        bool isActivateOnStart;

        [SerializeField]
        uint maxObject;

        [SerializeField]
        GameObject ejectObject;

        [SerializeField]
        Vector3 ejectOrigin;

        [SerializeField]
        float ejectSpeed;

        [SerializeField]
        Vector3 ejectDirection;

        [SerializeField]
        Switch switchComponent;


        GameObject[] objectPool;
        MoveAgent[] moveAgentPool;

        Timer timer;

#if UNITY_EDITOR

         void OnDrawGizmos() {
            if (switchComponent) {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, switchComponent.gameObject.transform.position);
            }
         }
#endif

        void Awake()
        {
            timer = GetComponent<Timer>();

            objectPool = new GameObject[maxObject];
            moveAgentPool = new MoveAgent[maxObject];

            for (int i = 0; i < objectPool.Length; ++i) {
                objectPool[i] = Instantiate(ejectObject, transform.position + ejectOrigin, Quaternion.identity) as GameObject;
                moveAgentPool[i] = objectPool[i].GetComponent<MoveAgent>();
                objectPool[i].gameObject.SetActive(false);
            }

            _Subscribe_Events();
        }

        void OnDestroy()
        {
            _Unsubscribe_Events();
        }

        void _Subscribe_Events()
        {
            GameController.OnGameStart += _OnGameStart;
            GameController.OnGameOver += _OnGameOver;

            if (switchComponent) {
                switchComponent.OnTurnOn += _OnTurnOn;
                switchComponent.OnTurnOff += _OnTurnOff;
            }

            timer.OnTimerStopped += _OnTimerStopped;
        }

        void _Unsubscribe_Events()
        {
            GameController.OnGameStart -= _OnGameStart;
            GameController.OnGameOver -= _OnGameOver;

            if (switchComponent) {
                switchComponent.OnTurnOn -= _OnTurnOn;
                switchComponent.OnTurnOff -= _OnTurnOff;
            }

            timer.OnTimerStopped -= _OnTimerStopped;
        }

        void _OnGameStart()
        {
            if (isActivateOnStart) {
                switchComponent.TurnOn();
            }
            else {
                switchComponent.TurnOff();
            }
        }

        void _OnGameOver()
        {
            switchComponent.TurnOff();
        }

        void _OnTurnOn()
        {
            timer.Countdown();
        }

        void _OnTurnOff()
        {
            timer.Countdown();
        }

        void _OnTimerStopped()
        {
            if (!switchComponent) { return; }

            if (isActivateOnStart) {
                if (switchComponent.IsTurnOn) {
                    _Select_Available_Object();
                }
            }
            else {
                if (!switchComponent.IsTurnOn) {
                    _Select_Available_Object();
                }
            }

            timer.Countdown();
        }

        void _Select_Available_Object()
        {
            for (int i = 0; i < objectPool.Length; ++i) {
                if (!objectPool[i].gameObject.activeSelf) {
                    objectPool[i].transform.position = transform.position + ejectOrigin;

                    moveAgentPool[i].MoveAtSpeed(ejectSpeed);
                    moveAgentPool[i].MoveAtDirection(ejectDirection);

                    objectPool[i].gameObject.SetActive(true);
                    break;
                }
            }
        }
    }
}
