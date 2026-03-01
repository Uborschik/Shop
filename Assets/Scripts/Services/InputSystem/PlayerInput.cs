using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Services.InputSystem
{
    public class PlayerInput : InputControls.IPlayerActions, IDisposable
    {
        public Vector2 MovementDirection { get; private set; }
        public Vector2 MouseDelta { get; private set; }

        public event Action Jump;

        private readonly InputControls inputs;

        public PlayerInput()
        {
            inputs = new();
            inputs.Player.Enable();
            inputs.Player.SetCallbacks(this);
        }

        public void Dispose()
        {
            inputs.Player.Disable();
        }

        public void OnMovement(InputAction.CallbackContext context)
        {
            MovementDirection = context.ReadValue<Vector2>();
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            MouseDelta = context.ReadValue<Vector2>();
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            Jump?.Invoke();
        }
    }
}