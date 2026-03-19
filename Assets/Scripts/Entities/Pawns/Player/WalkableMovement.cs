using Game.Services.InputSystem;
using System;
using Unity.Cinemachine;
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
        private readonly Transform cameraTransform;
        private readonly WalkableMovementConfig config;

        private Vector3 horizontalVelocity;
        private Quaternion horizontalRotation;
        private float verticalVelocity;
        private bool jumpPressed;

        public WalkableMovement(CharacterController controller, WalkableInputs inputs, Transform cameraTransform, WalkableMovementConfig config)
        {
            this.controller = controller;
            this.inputs = inputs;
            this.cameraTransform = cameraTransform;
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

            var motion = horizontalVelocity + Vector3.up * verticalVelocity;
            controller.Move(motion * Time.fixedDeltaTime);
        }

        private bool TrySetHorizontalVelocity()
        {
            if (cameraTransform == null) return false;

            var input = inputs.MovementDirection;

            var horizontalCameraForward = new Vector3(cameraTransform.forward.x, 0, cameraTransform.forward.z).normalized;
            horizontalRotation = Quaternion.LookRotation(horizontalCameraForward);

            var moveDir = (horizontalRotation * new Vector3(input.x, 0, input.y)).normalized;
            horizontalVelocity = moveDir * config.MoveSpeed;

            return true;
        }

        public void RotateTowardsMovement()
        {
            controller.transform.rotation = horizontalRotation;
        }
    }
}