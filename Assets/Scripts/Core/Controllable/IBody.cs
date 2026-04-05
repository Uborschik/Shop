using Game.Entities.Pawns.Player;
using UnityEngine;

namespace Game.Core.Controllable
{
    public interface IBody : IControllable
    {
        Transform Transform { get; }
        Hand Hand { get; }
        bool IsPhysicsEnabled { get; }

        void EnablePhysics();
        void DisablePhysics();
    }
}
