using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SC
{
    public class PlayerController : MonoBehaviour
    {
        const float X_OFFSET = 0.5f;
        const float Y_OFFSET = -0.2f;

        [SerializeField]
        float moveSpeed;

        [SerializeField]
        float gamepadDeadZone;


        float axisX;
        float axisY;

        Vector2 inputAxis;
        Rigidbody2D rigid;


        Vector3 expectPos;


        void Awake()
        {
            rigid = GetComponent<Rigidbody2D>();
        }

        void Start()
        {
            expectPos = rigid.position;
        }

        void Update()
        {
            _Input_Processing();
            _Input_Handler();
        }

        void FixedUpdate()
        {
            _Movement_Handler();
        }

        void _Input_Processing()
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

        void _Input_Handler()
        {
            var distance = Vector3.Distance(rigid.position, expectPos);
            var deadZone = 0.1f;

            if (distance >= deadZone) {
                return;
            }

            if (inputAxis.magnitude > 0.0f) {
                expectPos = expectPos + new Vector3(inputAxis.x, inputAxis.y, 0.0f);
            }
        }

        void _Movement_Handler()
        {
            if (expectPos == transform.position) {
                return;
            }

            var lerpVector = Vector3.Lerp(rigid.position, expectPos, 0.18f);
            rigid.position = lerpVector;
        }
    }
}
