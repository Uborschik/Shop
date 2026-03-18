using Game.Core.Controllable;
using System;
using UnityEngine;

namespace Game.Core.Possession
{
    public interface IVehicle : IControllable
    {
        ISeat DriverSeat { get; }
        Transform ExitPoint { get; }
        Transform Transform { get; }

        bool CanEnter { get; }
        bool CanExit { get; }

        event Action ExitRequested;

        void EnableVehicleInputs();
        void DisableVehicleInputs();
    }
}