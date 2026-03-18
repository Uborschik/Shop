using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Services.InputSystem
{
    public class WalkableInputs : InputHandler, InputControls.IWalkableMovementActions
    {
        public Vector2 MovementDirection { get; private set; }

        public event Action Jump;

        public override void Enable() => Inputs.WalkableMovement.SetCallbacks(this);
        public override void Disable() => Inputs.WalkableMovement.RemoveCallbacks(this);

        public void OnMovement(InputAction.CallbackContext context)
        {
            MovementDirection = context.ReadValue<Vector2>();
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
                Jump?.Invoke();
        }
    }
}
