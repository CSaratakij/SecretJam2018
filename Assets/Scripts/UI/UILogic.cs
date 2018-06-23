using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SC
{
    public class UILogic : MonoBehaviour
    {
        public void GameStart()
        {
            GameController.GameStart();
        }

        public void GameOver()
        {
            GameController.GameOver();
        }

        public void ExitGame()
        {
            Application.Quit();
        }

        public void ReloadGame()
        {
            var activeScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(activeScene.name);
        }
    }
}
