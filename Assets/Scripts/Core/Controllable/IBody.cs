using UnityEngine;

namespace Game.Core.Controllable
{
    public interface IBody : IControllable
    {
        Transform Transform { get; }
        bool IsPhysicsEnabled { get; }

        void EnablePhysics();
        void DisablePhysics();
    }
}
