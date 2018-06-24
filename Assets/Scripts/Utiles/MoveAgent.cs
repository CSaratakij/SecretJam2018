using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SC
{
    public class MoveAgent : MonoBehaviour
    {
        [SerializeField]
        bool isMovaAble;

        [SerializeField]
        float moveSpeed;

        [SerializeField]
        Vector3 moveDirection;


        public bool IsMoveAble => isMovaAble;
        public float MoveSpeed => moveSpeed;
        public Vector3 MoveDirection => moveDirection;


        void Update()
        {
            transform.Translate((moveDirection * moveSpeed) * Time.deltaTime);
        }

        public void AllowMove(bool value)
        {
            isMovaAble = value;
        }

        public void MoveAtDirection(Vector3 value)
        {
            moveDirection = value;
        }

        public void MoveAtSpeed(float value)
        {
            moveSpeed = value;
        }
    }
}
