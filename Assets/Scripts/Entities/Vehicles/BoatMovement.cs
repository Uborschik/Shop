using Game.Services.InputSystem;
using System;
using UnityEngine;

namespace Game.Entities.Vehicles
{
    [Serializable]
    public class BoatMovementConfig
    {
        [Header("Acceleration")]
        public float Acceleration = 5f;
        public float Deceleration = 2f;
        public float MaxForwardSpeed = 8f;
        public float MaxBackwardSpeed = -4f;

        [Header("Rotation")]
        public float MaxRotationSpeed = 30f;
        public float RotationInertia = 2f;
        public float MinSpeedForRotation = 0.5f;

        [Header("Physics")]
        public float WaterDrag = 0.5f;
        public float AngularDrag = 2f;
    }

    public class BoatMovement
    {
        private readonly Rigidbody rb;
        private readonly VehicleInputs inputs;
        private readonly BoatMovementConfig config;

        private float currentSpeed;
        private float currentRotationVelocity;

        public float CurrentSpeed => currentSpeed;
        public float NormalizedSpeed => Mathf.Abs(currentSpeed) / config.MaxForwardSpeed;

        public BoatMovement(Rigidbody rb, VehicleInputs inputs, BoatMovementConfig config)
        {
            this.rb = rb;
            this.inputs = inputs;
            this.config = config;

            rb.useGravity = true;
            rb.linearDamping = config.WaterDrag;
            rb.angularDamping = config.AngularDrag;
        }

        public void Tick()
        {
            var throttle = inputs.Throttle;

            if (Mathf.Abs(throttle) > 0.01f)
            {
                currentSpeed += throttle * config.Acceleration * Time.deltaTime;
            }

            currentSpeed = Mathf.Clamp(currentSpeed, config.MaxBackwardSpeed, config.MaxForwardSpeed);
        }

        public void FixedTick()
        {
            var velocity = rb.transform.forward * currentSpeed;
            rb.linearVelocity = new Vector3(velocity.x, rb.linearVelocity.y, velocity.z);

            var absSpeed = Mathf.Abs(currentSpeed);

            if (absSpeed < config.MinSpeedForRotation)
            {
                currentRotationVelocity = Mathf.Lerp(currentRotationVelocity, 0, config.RotationInertia * Time.fixedDeltaTime);
            }
            else
            {
                var speedEffectiveness = Mathf.Clamp01((absSpeed - config.MinSpeedForRotation) / (config.MaxForwardSpeed - config.MinSpeedForRotation));
                speedEffectiveness = speedEffectiveness * speedEffectiveness;

                var steeringInput = inputs.Yaw;
                var targetRotation = steeringInput * config.MaxRotationSpeed * speedEffectiveness;

                currentRotationVelocity = Mathf.Lerp(currentRotationVelocity, targetRotation, config.RotationInertia * Time.fixedDeltaTime);
            }

            var directionMultiplier = currentSpeed > 0 ? 1f : -1f;
            rb.MoveRotation(rb.rotation * Quaternion.Euler(0, currentRotationVelocity * directionMultiplier * Time.fixedDeltaTime, 0));
        }
    }
}