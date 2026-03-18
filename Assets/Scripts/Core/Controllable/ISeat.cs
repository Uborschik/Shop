using UnityEngine;

namespace Game.Core.Controllable
{
    public interface ISeat
    {
        bool IsOccupied { get; }
        Transform Transform { get; }

        bool TryOccupy(IBody body);
        void Vacate(IBody body);
    }
}
