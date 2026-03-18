using Game.Core.Controllable;
using Game.Core.Possession;
using Game.Services.InputSystem;
using System;
using UnityEngine;

namespace Game.Entities.Vehicles
{
    [RequireComponent(typeof(Rigidbody))]
    public class Boat : MonoBehaviour, IVehicle, IControllable
    {
        [Header("Config")]
        [SerializeField] private BoatMovementConfig movementConfig;
        [SerializeField] private Transform driverSeatPoint;
        [SerializeField] private Transform exitPoint;
        [SerializeField] private float exitValidationRadius = 1f;
        [SerializeField] private LayerMask groundMask;

        private Rigidbody rb;
        private SimpleSeat seat;
        private BoatMovement movement;
        private VehicleInputs inputs;

        public event Action ExitRequested;

        public ControlFlag CurrentFlags { get; private set; }
        public Transform Transform => transform;
        public bool IsPhysicsEnabled => !rb.isKinematic;

        public ISeat DriverSeat => seat;
        public Transform ExitPoint => exitPoint ?? driverSeatPoint;
        public bool CanEnter => !seat.IsOccupied;
        public bool CanExit => seat.IsOccupied && ValidateExit();

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            rb.useGravity = true;

            seat = new SimpleSeat(driverSeatPoint);
            inputs = new VehicleInputs();
            movement = new BoatMovement(rb, inputs, movementConfig);
        }

        public void EnableVehicleInputs() => inputs.Enable();
        public void DisableVehicleInputs() => inputs.Disable();

        #region IControllable

        public void Possess(ControlFlag grantedFlags)
        {
            if (grantedFlags.HasFlag(ControlFlag.Movement))
            {
                CurrentFlags |= ControlFlag.Movement;
            }
        }

        public void Release(ControlFlag flagsToRelease)
        {
            if (flagsToRelease.HasFlag(ControlFlag.Movement))
            {
                CurrentFlags &= ~ControlFlag.Movement;
            }
        }

        public void OnTick(ControlFlag availableFlags)
        {
            if (availableFlags.HasFlag(ControlFlag.Movement))
            {
                movement.Tick();
            }
        }

        public void OnFixedTick(ControlFlag availableFlags)
        {
            // Всегда обрабатываем физику (инерция), ввод — только с флагом
            if (availableFlags.HasFlag(ControlFlag.Movement))
            {
                movement.Tick();
            }

            movement.FixedTick();
        }

        public void OnLateTick(ControlFlag availableFlags) { }

        #endregion

        #region IBody

        public void EnablePhysics() => rb.isKinematic = false;
        public void DisablePhysics() => rb.isKinematic = true;

        public void AttachTo(Transform parent, Vector3 localPosition, Quaternion localRotation)
        {
            // Лодка не привязывается к другим объектам
        }

        public void DetachTo(Vector3 worldPosition, Quaternion worldRotation)
        {
            // Лодка не отсоединяется — она самостоятельна
        }

        #endregion

        private bool ValidateExit()
        {
            Vector3 startPos = ExitPoint.position + Vector3.up;
            bool hasGround = Physics.Raycast(startPos, Vector3.down, out var hit, 5f, groundMask);

            if (!hasGround) return false;

            return !Physics.CheckSphere(ExitPoint.position, exitValidationRadius);
        }
    }
}