using UnityEngine;

namespace Game.Core.Possession
{
    public interface IExitValidator
    {
        bool ValidateExitPoint(Vector3 desiredPoint, out Vector3 validPoint);
    }
}