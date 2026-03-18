using Game.Core.Controllable;
using UnityEngine;

namespace Game.Core.Possession
{
    public class VehicleEntrySystem
    {
        private readonly PossessionRouter possession;
        private readonly IExitValidator exitValidator;

        public VehicleEntrySystem(PossessionRouter possession, IExitValidator exitValidator)
        {
            this.possession = possession;
            this.exitValidator = exitValidator;
        }

        public bool TryEnterVehicle(IBody driverBody, IControllable driverController, IVehicle vehicle, ControlFlag vehicleControlFlags)
        {
            if (!vehicle.CanEnter) return false;

            var seat = vehicle.DriverSeat;

            if (!seat.TryOccupy(driverBody)) return false;

            //possession.TransferControl(vehicle, vehicleControlFlags);

            vehicle.ExitRequested += () => TryExitVehicle(driverBody, driverController, vehicle);

            return true;
        }

        public bool TryExitVehicle(IBody driverBody, IControllable driverController, IVehicle vehicle)
        {
            if (!vehicle.CanExit) return false;

            if (!exitValidator.ValidateExitPoint(vehicle.ExitPoint.position, out var validPoint))
                return false;

            //possession.ReturnControl(vehicle.CurrentFlags);

            vehicle.DriverSeat.Vacate(driverBody);
            driverBody.EnablePhysics();
            driverBody.DetachTo(validPoint, Quaternion.LookRotation(vehicle.Transform.forward));

            vehicle.ExitRequested -= () => TryExitVehicle(driverBody, driverController, vehicle);

            return true;
        }
    }
}