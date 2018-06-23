﻿using System.Collections;
using System.Collections.Generic;

namespace SC
{
    public sealed class GameController
    {
        public delegate void _Func();

        public static event _Func OnGameStart;
        public static event _Func OnGameOver;

        public static bool IsGameStart { get; private set; }


        public static void GameStart()
        {
            if (IsGameStart) { return; }
            IsGameStart = true;

            if (OnGameStart != null) {
                OnGameStart();
            }
        }

        public static void GameOver()
        {
            if (!IsGameStart) { return; }
            IsGameStart = false;

            if (OnGameOver != null) {
                OnGameOver();
            }
        }
    }
}
