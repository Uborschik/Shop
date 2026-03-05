using System;
using UnityEngine;

namespace Game.Entities.Pawns.Player
{
    [Serializable]
    public class MovementConfig
    {
        public float Gravity = -9.81f;
        public float JumpSpeed = 5f;
        public float MoveSpeed = 5f;
    }

    public class TraderMovement
    {
        private readonly Func<Transform> getCameraTransform;
        private readonly CharacterController controller;
        private Vector3 moveDirection;
        private float verticalVelocity;
        private bool jumpPressed;

        private readonly MovementConfig config;

        public TraderMovement(Func<Transform> getCameraTransform, CharacterController controller, MovementConfig config)
        {
            this.getCameraTransform = getCameraTransform;
            this.controller = controller;

            this.config = config;
        }

        public void SetMovement(Vector2 inputDirection)
        {
            if (inputDirection.sqrMagnitude < 0.01f)
            {
                moveDirection = Vector3.zero;
                return;
            }

            var move = new Vector3(inputDirection.x, 0, inputDirection.y);
            move = getCameraTransform().TransformDirection(move);
            move.y = 0;
            moveDirection = move.normalized;
        }

        public void Jump()
        {
            if (controller.isGrounded)
                jumpPressed = true;
        }

        public void Tick()
        {
            if (controller.isGrounded && verticalVelocity < 0)
                verticalVelocity = -2f;

            if (jumpPressed && controller.isGrounded)
            {
                verticalVelocity = config.JumpSpeed;
                jumpPressed = false;
            }

            verticalVelocity += config.Gravity * Time.deltaTime;

            var motion = moveDirection * config.MoveSpeed + Vector3.up * verticalVelocity;
            controller.Move(motion * Time.deltaTime);
        }
    }
}