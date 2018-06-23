using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SC
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        float moveSpeed;

        [SerializeField]
        float gamepadDeadZone;


        float axisX;
        float axisY;

        Vector2 inputAxis;
        Rigidbody2D rigid;


        void Awake()
        {
            rigid = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            _Input_Handler();
        }

        void FixedUpdate()
        {
        }

        void _Input_Handler()
        {
            axisX = Input.GetAxisRaw("Horizontal");

            if (axisX > gamepadDeadZone) {
                inputAxis.x = 1.0f;
            }
            else if (axisX < -gamepadDeadZone) {
                inputAxis.x = -1.0f;
            }
            else {
                inputAxis.x = 0.0f;
            }

            axisY = Input.GetAxisRaw("Vertical");

            if (axisY > gamepadDeadZone) {
                inputAxis.y = 1.0f;
            }
            else if (axisY < -gamepadDeadZone) {
                inputAxis.y = -1.0f;
            }
            else {
                inputAxis.y = 0.0f;
            }
        }
    }
}
