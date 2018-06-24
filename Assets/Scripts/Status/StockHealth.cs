using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SC
{
    public class StockHealth : MonoBehaviour
    {
        [SerializeField]
        int current;

        [SerializeField]
        int maximum;


        public delegate void _Func(int value);
        public delegate void _FuncNoParam();

        public event _Func OnStockHealthChanged;
        public event _FuncNoParam OnStockHealthEmpty;

        public int Current => current;
        public int Maximum => maximum;
        public bool IsEmpty => current <= 0;


        public void Clear()
        {
            current = 0;
            OnStockHealthEmpty?.Invoke();
            OnStockHealthChanged?.Invoke(current);
        }

        public void FullRestore()
        {
            current = maximum;
            OnStockHealthChanged?.Invoke(current);
        }

        public void Restore(int value)
        {
            current = ((current + value) > maximum) ? maximum : current + value;
            OnStockHealthChanged?.Invoke(current);
        }

        public void Remove(int value)
        {
            current = ((current - value) < 0) ? 0 : current - value;

            if (current == 0) {
                OnStockHealthEmpty?.Invoke();
            }

            OnStockHealthChanged?.Invoke(current);
        }
    }
}
