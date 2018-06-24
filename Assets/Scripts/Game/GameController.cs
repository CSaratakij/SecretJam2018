using System.Collections;
using System.Collections.Generic;

namespace SC
{
    public enum GameOverCondition
    {
        Dead,
        Pass,
        TimeOut
    }

    public sealed class GameController
    {
        public delegate void _Func();

        public static event _Func OnGameStart;
        public static event _Func OnGameOver;

        public static bool IsGameStart { get; private set; }
        public static bool IsStartOnce { get; private set; }


        public static GameOverCondition GameOverCondition = GameOverCondition.Dead;


        public static void GameStart()
        {
            if (IsGameStart) { return; }
            IsGameStart = true;
            OnGameStart?.Invoke();
        }

        public static void GameOver()
        {
            if (!IsGameStart) { return; }
            IsGameStart = false;
            IsStartOnce = true;
            OnGameOver?.Invoke();
        }
    }
}
