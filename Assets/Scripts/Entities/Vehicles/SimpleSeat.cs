using Game.Core.Controllable;
using UnityEngine;

namespace Game.Entities.Vehicles
{
    public class SimpleSeat : ISeat
    {
        public Transform Transform { get; }
        public bool IsOccupied => currentBody != null;

        private IBody currentBody;

        public SimpleSeat(Transform transform)
        {
            Transform = transform;
        }

        public bool TryOccupy(IBody body)
        {
            if (IsOccupied) return false;

            currentBody = body;
            body.DisablePhysics();
            //body.AttachTo(Transform, Vector3.zero, Quaternion.identity);
            return true;
        }

        public void Vacate(IBody body)
        {
            if (currentBody != body) return;
            currentBody = null;
        }
    }
}