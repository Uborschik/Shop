using System;

namespace Game.Core.Controllable
{
    [Flags]
    public enum ControlFlag
    {
        None = 0,
        Movement = 1 << 0,
        Look = 1 << 1,
        Interact = 1 << 2,
        All = Movement | Look | Interact
    }
}
