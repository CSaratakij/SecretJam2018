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

        [SerializeField]
        LayerMask wallLayerMask;


        bool isMoveAble;
        bool isInvinsible;

        float axisX;
        float axisY;

        Animator anim;

        Collider2D hit;
        Collider2D hitWall;

        Vector2 inputAxis;
        Vector2 expectPos;
        Vector2 previousPos;

        RaycastHit2D hitup;
        RaycastHit2D hitdown;
        RaycastHit2D hitright;
        RaycastHit2D hitleft;

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
            anim = GetComponent<Animator>();
            rigid = GetComponent<Rigidbody2D>();
            stockHealth = GetComponent<StockHealth>();

            timer = GetComponent<Timer>();

            spriteRenderer = GetComponent<SpriteRenderer>();
            color = spriteRenderer.color;

            _Subscribe_Events();
        }
        
        void Start()
        {
            expectPos = transform.position;
        }

        void Update()
        {
            _Input_Processing();
            _Input_Handler();

            if (!isMoveAble) { return; }
            _Animation_Handler();
        }

        void FixedUpdate()
        {
            if (!GameController.IsGameStart) { return; }
            _Movement_Handler();

            if (isInvinsible) { return; }
            _TakeDamage_Handler();
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
            if (Vector2.Distance(expectPos, rigid.position) <= 0.2f) {
                previousPos = expectPos;
                expectPos += inputAxis;;
            }

            var distance = Vector2.Distance(rigid.position, expectPos);

            if (distance > 0.0f) {
                var dir = expectPos - rigid.position;

                //Hacks
                var pos = rigid.position + dir * moveSpeed * Time.deltaTime;

                pos.x = Mathf.Clamp(pos.x, -8.5f, 8.5f); 
                pos.y = Mathf.Clamp(pos.y, -4.2f, 4.8f); 

                rigid.position = pos;
            }
        }

        void _Movement_Handler()
        {
            if (!isMoveAble) { return; }
            if (expectPos == previousPos) { return; }

            hitWall = Physics2D.OverlapBox(expectPos, new Vector2(0.3f, 0.3f), 0.0f, wallLayerMask);

            if (hitWall) {
                expectPos = previousPos;
            }
        }

        void _TakeDamage_Handler()
        {
            hit = Physics2D.OverlapBox(rigid.position + new Vector2(0.0f, Y_OFFSET), new Vector2(0.5f, 0.5f), 0.0f, layerMask);

            if (hit == null) { return; }
            if (stockHealth.IsEmpty) { return; }

            if (hit.CompareTag("Spike")) {
                stockHealth.Remove(1);

                if (!isInvinsible) {
                    _Flickering_Sprite();
                }
            }
            else if (hit.CompareTag("Bullet")) {
                stockHealth.Remove(1);

                if (!isInvinsible) {
                    _Flickering_Sprite();
                }
            }
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
            isMoveAble = false;
            anim.Play("Dead");
            StartCoroutine(_GameOver_Callback());
        }

        void _OnTimerStopped()
        {
            isInvinsible = false;
        }

        void _Flickering_Sprite()
        {
            isInvinsible = true;
            timer.Countdown();
            StartCoroutine(_Flickering_Sprite_Callback());
        }

        void _Animation_Handler()
        {
            if (inputAxis.x > 0.0f) {
                anim.Play("WalkRight");
            }
            else if (inputAxis.x < 0.0f) {
                anim.Play("WalkLeft");
            }
            else {
                anim.Play("Idle");
            }
        }

        IEnumerator _GameOver_Callback()
        {
            yield return new WaitForSeconds(1.3f);
            GameController.GameOverCondition = GameOverCondition.Dead;
            GameController.GameOver();
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
