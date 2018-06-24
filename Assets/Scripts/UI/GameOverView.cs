using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SC
{
    public class GameOverView : MonoBehaviour
    {
        enum View
        {
            GameOver,
            Pass,
            TimeOut
        }

        [SerializeField]
        RectTransform[] views;


        void OnEnable()
        {
            _ShowView_Handler();
        }

        void Awake()
        {
            GameController.OnGameOver += _OnGameOver;
        }

        void OnDestroy()
        {
            GameController.OnGameOver -= _OnGameOver;
        }

        void _OnGameOver()
        {
            _ShowView_Handler();
        }

        void _ShowView_Handler()
        {
            _HideAllView();

            switch (GameController.GameOverCondition) {
                case GameOverCondition.Pass:
                    _Show(View.Pass);
                    break;

                case GameOverCondition.Dead:
                    _Show(View.GameOver);
                    break;

                case GameOverCondition.TimeOut:
                    _Show(View.TimeOut);
                    break;

                default:
                    break;
            }
        }

        void _HideAllView()
        {
            foreach (RectTransform rect in views) {
                if (!rect) {
                    continue;
                }

                rect.gameObject.SetActive(false);
            }
        }

        void _Show(View view)
        {
            views[(int)view].gameObject.SetActive(true);
        }
    }
}
