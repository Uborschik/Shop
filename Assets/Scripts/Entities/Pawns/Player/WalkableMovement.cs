using Game.Services.InputSystem;
using System;
using UnityEngine;

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

        private Transform cameraTransform;
        private Vector3 horizontalVelocity;
        private float verticalVelocity;
        private bool jumpPressed;

        public WalkableMovement(CharacterController controller, WalkableInputs inputs, WalkableMovementConfig config)
        {
            this.controller = controller;
            this.inputs = inputs;
            this.config = config;
        }

        public void SetCamera(Transform camera) => this.cameraTransform = camera;

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
            SetHorizontalVelocity();

            if (controller.isGrounded && verticalVelocity < 0)
                verticalVelocity = -0.5f;

            if (jumpPressed && controller.isGrounded)
            {
                verticalVelocity = config.JumpSpeed;
                jumpPressed = false;
            }

            verticalVelocity += config.Gravity * Time.fixedDeltaTime;

            var motion = horizontalVelocity + Vector3.up * verticalVelocity;
            controller.Move(motion * Time.fixedDeltaTime);
        }

        private void SetHorizontalVelocity()
        {
            if (cameraTransform == null) return;

            var input = inputs.MovementDirection;

            if (input.sqrMagnitude > 0.01f)
            {
                var camForward = Vector3.ProjectOnPlane(cameraTransform.forward, Vector3.up).normalized;
                var camRight = Vector3.ProjectOnPlane(cameraTransform.right, Vector3.up).normalized;

                var moveDir = (camForward * input.y + camRight * input.x).normalized;
                horizontalVelocity = moveDir * config.MoveSpeed;
            }
            else
            {
                horizontalVelocity = Vector3.zero;
            }
        }
    }
}