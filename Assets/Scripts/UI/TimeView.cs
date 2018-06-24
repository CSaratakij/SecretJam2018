using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SC
{
    public class TimeView : MonoBehaviour
    {
        [SerializeField]
        Timer timer;

        [SerializeField]
        Text txtTime;


        void Update()
        {
            txtTime.text = ((int)timer.Current).ToString();
        }
    }
}
