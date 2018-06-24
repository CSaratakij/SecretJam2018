using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SC
{
    public class StockHealthView : MonoBehaviour
    {
        [SerializeField]
        Image[] imgHearts;

        [SerializeField]
        StockHealth stockHealth;


        void Awake()
        {
            if (stockHealth) {
                stockHealth.OnStockHealthChanged += _OnStockHealthChanged;
            }
        }

        void OnDestroy()
        {
            if (stockHealth) {
                stockHealth.OnStockHealthChanged -= _OnStockHealthChanged;
            }
        }

        void _OnStockHealthChanged(int value)
        {
            Show(value);
        }

        public void Show(int value)
        {
            for (int i = 0; i < imgHearts.Length; i++) {
                if (i + 1 <= value) {
                    imgHearts[i].gameObject.SetActive(true);
                }
                else {
                    imgHearts[i].gameObject.SetActive(false);
                }
            }
        }
    }
}
