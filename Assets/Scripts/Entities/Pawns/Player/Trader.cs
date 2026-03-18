using Game.Core.Controllable;
using Game.Services.InputSystem;
using UnityEngine;

namespace Game.Entities.Pawns.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class Trader : MonoBehaviour, IControllable, IBody
    {
        [SerializeField] private WalkableMovementConfig movementConfig;

        private CharacterController characterController;
        private WalkableInputs inputs;
        private WalkableMovement movement;

        public ControlFlag CurrentFlags { get; private set; }
        public Transform Transform => transform;
        public bool IsPhysicsEnabled => characterController.enabled;

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
            inputs = new();
            movement = new(characterController, inputs, movementConfig);
        }

        public void SetCamera(Transform transform)
        {
            movement.SetCamera(transform);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        #region IControllable

        public void Possess(ControlFlag grantedFlags)
        {
            if (grantedFlags.HasFlag(ControlFlag.Movement))
            {
                CurrentFlags |= ControlFlag.Movement;
                inputs.Enable();

                inputs.Jump += movement.OnJump;
            }
        }

        public void Release(ControlFlag flagsToRelease)
        {
            if (flagsToRelease.HasFlag(ControlFlag.Movement))
            {
                CurrentFlags &= ~ControlFlag.Movement;
                inputs.Disable();
                movement.Reset();

                inputs.Jump -= movement.OnJump;
            }
        }

        public void OnTick(ControlFlag availableFlags)
        {
            if (availableFlags.HasFlag(ControlFlag.Movement)) movement.Tick();
        }

        public void OnFixedTick(ControlFlag availableFlags) { }
        public void OnLateTick(ControlFlag availableFlags) { }

        #endregion

        #region IBody

        public void EnablePhysics() => characterController.enabled = true;

        public void DisablePhysics() => characterController.enabled = false;

        public void AttachTo(Transform parent, Vector3 localPosition, Quaternion localRotation)
        {
            transform.SetParent(parent);
            transform.localPosition = localPosition;
            transform.localRotation = localRotation;
            DisablePhysics();
        }

        public void DetachTo(Vector3 worldPosition, Quaternion worldRotation)
        {
            transform.SetParent(null);
            transform.position = worldPosition;
            transform.rotation = worldRotation;
            EnablePhysics();
        }

        #endregion
    }
}