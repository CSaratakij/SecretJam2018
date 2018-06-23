using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SC
{
    public class PlayerController : MonoBehaviour
    {
        const float X_OFFSET = 0.5f;
        const float Y_OFFSET = -0.2f;
        const float MOVEABLE_DEADZONE = 0.1f;


        [SerializeField]
        float moveSpeed;

        [SerializeField]
        float gamepadDeadZone;


        bool isMoveAble;

        float axisX;
        float axisY;

        Vector2 inputAxis;
        Vector2 expectPos;

        Rigidbody2D rigid;


        void Awake()
        {
            _Subscribe_Events();
            rigid = GetComponent<Rigidbody2D>();
        }
        
        void OnDestroy()
        {
            _Unsubscribe_Events();
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

        void _Subscribe_Events()
        {
            GameController.OnGameStart += _OnGameStart;
            GameController.OnGameOver += _OnGameOver;
        }

        void _Unsubscribe_Events()
        {
            GameController.OnGameStart -= _OnGameStart;
            GameController.OnGameOver -= _OnGameOver;
        }

        void _OnGameStart()
        {
            isMoveAble = true;
        }

        void _OnGameOver()
        {
            isMoveAble = false;
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
            var distance = Vector2.Distance(rigid.position, expectPos);
            if (distance >= MOVEABLE_DEADZONE) { return; }

            if (inputAxis.magnitude > 0.0f) {
                expectPos += inputAxis;
            }
        }

        void _Movement_Handler()
        {
            if (!isMoveAble) { return; }
            if (expectPos == rigid.position) { return; }

            var lerpVector = Vector3.Lerp(rigid.position, expectPos, 0.18f);
            rigid.position = lerpVector;
        }
    }
}
