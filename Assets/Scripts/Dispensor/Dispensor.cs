using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SC
{
    public class Dispensor : MonoBehaviour
    {
        [SerializeField]
        uint maxObject;

        [SerializeField]
        GameObject ejectObject;

        [SerializeField]
        Vector3 ejectOrigin;

        [SerializeField]
        Vector3 ejectDirection;

        [SerializeField]
        Switch switchComponent;


        GameObject[] objectPool;
        Timer timer;


        void Awake()
        {
            timer = GetComponent<Timer>();
            objectPool = new GameObject[maxObject];

            for (int i = 0; i < objectPool.Length; ++i) {
                objectPool[i] = Instantiate(ejectObject, transform.position + ejectOrigin, Quaternion.identity) as GameObject;
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
            switchComponent.TurnOn();
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
            timer.Stop();
        }

        void _OnTimerStopped()
        {
            if (!switchComponent) { return; }
            if (!switchComponent.IsTurnOn) { return; }

            for (int i = 0; i < objectPool.Length; ++i) {
                if (!objectPool[i].gameObject.activeSelf) {
                    objectPool[i].transform.position = transform.position + ejectOrigin;
                    objectPool[i].gameObject.SetActive(true);
                    break;
                }
            }

            timer.Countdown();
        }
    }
}
