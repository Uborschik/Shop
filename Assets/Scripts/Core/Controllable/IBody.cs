using UnityEngine;

namespace Game.Core.Controllable
{
    public interface IBody
    {
        Transform Transform { get; }
        bool IsPhysicsEnabled { get; }

        void EnablePhysics();
        void DisablePhysics();
        void AttachTo(Transform parent, Vector3 localPosition, Quaternion localRotation);
        void DetachTo(Vector3 worldPosition, Quaternion worldRotation);
    }
}
