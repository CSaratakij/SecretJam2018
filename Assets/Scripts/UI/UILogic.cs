using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SC
{
    public class UILogic : MonoBehaviour
    {
        public void GameStart()
        {
            GameController.GameStart();
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}
