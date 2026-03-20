using Game.Core.Controllable;
using Game.Entities.Player;
using Game.Services.InputSystem;
using Unity.Cinemachine;
using UnityEngine;

namespace Game.Entities.Pawns.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class Trader : MonoBehaviour, IBody
    {
        [SerializeField] private WalkableMovementConfig movementConfig;

        private WalkableInputs inputs;
        private CharacterController characterController;
        private WalkableMovement movement;

        public ControlFlag CurrentFlags { get; private set; }
        public Transform Transform => transform;
        public bool IsPhysicsEnabled => characterController.enabled;

        private void Awake()
        {
            inputs = new();
            characterController = GetComponent<CharacterController>();
            movement = new(characterController, inputs, movementConfig);
        }

        private void Update()
        {
            if (CurrentFlags.HasFlag(ControlFlag.Movement))
                movement.Tick();
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

            if (grantedFlags.HasFlag(ControlFlag.Look))
            {
                CurrentFlags |= ControlFlag.Look;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

        public void Release(ControlFlag flagsToRelease)
        {
            if (flagsToRelease.HasFlag(ControlFlag.Movement))
            {
                CurrentFlags &= ~ControlFlag.Movement;
                movement.Reset();
                inputs.Disable();
                inputs.Jump -= movement.OnJump;
            }

            if (flagsToRelease.HasFlag(ControlFlag.Look))
            {
                CurrentFlags &= ~ControlFlag.Look;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

        #endregion

        #region IBody

        public void EnablePhysics() => characterController.enabled = true;
        public void DisablePhysics() => characterController.enabled = false;

        #endregion
    }
}