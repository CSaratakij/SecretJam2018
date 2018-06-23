using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SC
{
    public class GameMenuController : MonoBehaviour
    {
        [SerializeField]
        RectTransform[] views;

        enum View
        {
            MainMenu,
            InGameMenu,
            GameOverMenu
        }

        void Awake()
        {
            _Subscribe_Events();
        }

        void Start()
        {
            _ShowOnly(View.MainMenu);
        }

        void OnDestroy()
        {
            _Unsubscribe_Events();
        }

        void _OnGameStart()
        {
            _ShowOnly(View.InGameMenu);
        }

        void _OnGameOver()
        {
            _ShowOnly(View.GameOverMenu);
        }

        void _Subscribe_Events()
        {
            GameController.OnGameStart += _OnGameStart;
            GameController.OnGameOver += _OnGameOver;
        }

        void _Unsubscribe_Events()
        {
            GameController.OnGameStart -= _OnGameStart;
            GameController.OnGameOver -= _OnGameOver;
        }

        void _Show(View view)
        {
            var targetView = views[(int)view];
            if (targetView != null) {
                targetView.gameObject.SetActive(true);
            }
        }

        void _ShowOnly(View view)
        {
            _HideAll();
            _Show(view);
        }

        void _Hide(View view)
        {
            views[(int)view].gameObject.SetActive(false);
        }

        void _HideAll()
        {
            foreach (RectTransform rect in views) {
                if (rect != null) {
                    rect.gameObject.SetActive(false);
                }
            }
        }
    }
}
