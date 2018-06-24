using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SC
{
    public class Goal : MonoBehaviour
    {
        RectDetector rectDetector;


        void Awake()
        {
            rectDetector = GetComponent<RectDetector>();

            if (rectDetector) {
                rectDetector.OnEnter += _OnEnter;
            }
        }

        void OnDestroy()
        {
            if (rectDetector) {
                rectDetector.OnEnter -= _OnEnter;
            }
        }

        void _OnEnter(GameObject obj)
        {
            if (obj.CompareTag("Player")) {
                GameController.GameOverCondition = GameOverCondition.Pass;
                GameController.GameOver();
            }
        }
    }
}
