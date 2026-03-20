using Game.Services.InputSystem;
using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.Entities.Pawns.Player
{
    [Serializable]
    public class WalkableMovementConfig
    {
        public float Gravity = -9.81f;
        public float JumpSpeed = 5f;
        public float MoveSpeed = 5f;
        public float RotationSpeed = 10f;
    }

    public class WalkableMovement
    {
        private readonly CharacterController controller;
        private readonly WalkableInputs inputs;
        private readonly WalkableMovementConfig config;

        private Vector3 horizontalVelocity;
        private Quaternion horizontalRotation;
        private float verticalVelocity;
        private bool jumpPressed;

        public WalkableMovement(CharacterController controller, WalkableInputs inputs, WalkableMovementConfig config)
        {
            this.controller = controller;
            this.inputs = inputs;
            this.config = config;
        }

        public void Reset()
        {
            horizontalVelocity = Vector3.zero;
            verticalVelocity = 0f;
        }
        
        public void OnJump()
        {
            if (controller.isGrounded)
                jumpPressed = true;
        }

        public void Tick()
        {
            if (!TrySetHorizontalVelocity()) return;

            if (controller.isGrounded && verticalVelocity < 0)
                verticalVelocity = -0.5f;

            if (jumpPressed && controller.isGrounded)
            {
                verticalVelocity = config.JumpSpeed;
                jumpPressed = false;
            }

            verticalVelocity += config.Gravity * Time.fixedDeltaTime;

            var motion = controller.transform.TransformDirection(horizontalVelocity) + Vector3.up * verticalVelocity;
            controller.Move(motion * Time.fixedDeltaTime);
        }

        private bool TrySetHorizontalVelocity()
        {
            var input = inputs.MovementDirection;

            var localMoveDir = new Vector3(input.x, 0, input.y);

            if (localMoveDir.sqrMagnitude > 0.001f)
            {
                localMoveDir.Normalize();
                horizontalVelocity = localMoveDir * config.MoveSpeed;
            }
            else
            {
                horizontalVelocity = Vector3.zero;
            }

            return true;
        }
    }
}