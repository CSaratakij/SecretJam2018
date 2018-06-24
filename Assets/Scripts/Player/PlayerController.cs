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

        [SerializeField]
        LayerMask layerMask;


        bool isMoveAble;
        bool isInvinsible;

        float axisX;
        float axisY;

        Collider2D hit;

        Vector2 inputAxis;
        Vector2 expectPos;

        Rigidbody2D rigid;
        StockHealth stockHealth;

        Timer timer;

        SpriteRenderer spriteRenderer;
        Color color;


        void OnDestroy()
        {
            _Unsubscribe_Events();
        }

        void Awake()
        {
            rigid = GetComponent<Rigidbody2D>();
            stockHealth = GetComponent<StockHealth>();

            timer = GetComponent<Timer>();

            spriteRenderer = GetComponent<SpriteRenderer>();
            color = spriteRenderer.color;

            _Subscribe_Events();
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
            if (!GameController.IsGameStart) { return; }
            _Movement_Handler();

            if (isInvinsible) { return; }
            hit = Physics2D.OverlapBox(rigid.position + new Vector2(0.0f, Y_OFFSET), new Vector2(0.5f, 0.5f), 0.0f, layerMask);

            if (hit == null) { return; }

            if (hit.CompareTag("Spike")) {
                stockHealth.Remove(1);

                if (!isInvinsible) {
                    isInvinsible = true;
                    timer.Countdown();
                    StartCoroutine(_Flickering_Sprite_Callback());
                }
            }
            else if (hit.CompareTag("Bullet")) {
                stockHealth.Remove(1);

                if (!isInvinsible) {
                    isInvinsible = true;
                    timer.Countdown();
                    StartCoroutine(_Flickering_Sprite_Callback());
                }
            }
        }

        void _Input_Processing()
        {
            if (!isMoveAble) { return; }

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
            if (!isMoveAble) { return; }

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

        void _Subscribe_Events()
        {
            GameController.OnGameStart += _OnGameStart;
            GameController.OnGameOver += _OnGameOver;

            if (stockHealth) {
                stockHealth.OnStockHealthEmpty += _OnStockHealthEmpty;
            }

            if (timer) {
                timer.OnTimerStopped += _OnTimerStopped;
            }
        }

        void _Unsubscribe_Events()
        {
            GameController.OnGameStart -= _OnGameStart;
            GameController.OnGameOver -= _OnGameOver;

            if (stockHealth) {
                stockHealth.OnStockHealthEmpty -= _OnStockHealthEmpty;
            }

            if (timer) {
                timer.OnTimerStopped += _OnTimerStopped;
            }
        }

        void _OnGameStart()
        {
            isMoveAble = true;
        }

        void _OnGameOver()
        {
            rigid.position = expectPos;
            isMoveAble = false;
        }

        void _OnStockHealthEmpty()
        {
            GameController.GameOver();
        }

        void _OnTimerStopped()
        {
            isInvinsible = false;
        }

        IEnumerator _Flickering_Sprite_Callback()
        {
            color = spriteRenderer.color;

            while (isInvinsible && timer.Current > 0.36f) {

                color.a = 0.2f;
                spriteRenderer.color = color;

                yield return new WaitForSeconds(0.1f);

                color.a = 0.8f;
                spriteRenderer.color = color;

                yield return new WaitForSeconds(0.1f);
            }
            
            color.a = 1.0f;
            spriteRenderer.color = color;
        }
    }
}
