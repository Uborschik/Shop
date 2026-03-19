using System;

namespace Game.Services.InputSystem
{
    public abstract class InputHandler : IDisposable
    {
        protected readonly InputControls Inputs;

        public abstract void Enable();
        public abstract void Disable();

        public InputHandler()
        {
            Inputs = new();
        }

        public virtual void Dispose()
        {
            Inputs.Dispose();
        }
    }
}