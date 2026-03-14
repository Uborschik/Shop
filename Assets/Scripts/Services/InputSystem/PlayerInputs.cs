using Game.Entities;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Services.InputSystem
{
    public class PlayerInputs : InputHandler, InputControls.IPlayerActions
    {
        public Vector2 MovementDirection { get; private set; }
        public Vector2 MouseDelta { get; private set; }

        public event Action Jump;
        public event Action Drop;
        public event Action<InteractionMode> Interact;
        public event Action<InteractionMode> AltInteract;

        public PlayerInputs()
        {
            Inputs.Player.AddCallbacks(this);
        }

        public override void Dispose()
        {
            Inputs.Player.RemoveCallbacks(this);

            base.Dispose();
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
            if (context.phase == InputActionPhase.Performed)
                Jump?.Invoke();
        }

        public void OnDrop(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
                Drop?.Invoke();
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
                Interact?.Invoke(InteractionMode.Primary);
        }

        public void OnAltInteract(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
                AltInteract?.Invoke(InteractionMode.Secondary);
        }
    }
}
