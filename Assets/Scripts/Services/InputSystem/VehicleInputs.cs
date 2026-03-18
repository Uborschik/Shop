using System;
using UnityEngine.InputSystem;

namespace Game.Services.InputSystem
{
    public class VehicleInputs : InputHandler, InputControls.IVehicleMovementActions
    {
        public event Action Anchor;

        public float Throttle { get; private set; }
        public float Yaw { get; private set; }
        public float Pitch { get; private set; }

        public override void Enable() => Inputs.VehicleMovement.SetCallbacks(this);
        public override void Disable() => Inputs.VehicleMovement.RemoveCallbacks(this);

        public void OnAnchor(InputAction.CallbackContext context)
        {
            if(context.phase == InputActionPhase.Started)
            {
                Anchor?.Invoke();
            }
        }

        public void OnThrottle(InputAction.CallbackContext context)
        {
            Throttle = context.ReadValue<float>();
        }

        public void OnYaw(InputAction.CallbackContext context)
        {
            Yaw = context.ReadValue<float>();
        }

        public void OnPitch(InputAction.CallbackContext context)
        {
            Pitch = context.ReadValue<float>();
        }
    }
}
