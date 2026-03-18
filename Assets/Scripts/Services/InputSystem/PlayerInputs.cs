using Game.Entities;
using Game.Entities.Interactables;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Services.InputSystem
{
    public class PlayerInputs : InputHandler, InputControls.IPlayerActions
    {
        public Vector2 MouseDelta { get; private set; }

        public event Action Drop;
        public event Action<InteractionMode> Interact;
        public event Action<InteractionMode> AltInteract;

        public PlayerInputs()
        {
            Enable();
        }

        public override void Enable() => Inputs.Player.AddCallbacks(this);
        public override void Disable() => Inputs.Player.RemoveCallbacks(this);

        public void OnLook(InputAction.CallbackContext context)
        {
            MouseDelta = context.ReadValue<Vector2>();
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
