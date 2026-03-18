using Game.Core.Controllable;
using Game.Entities.Pawns.Player;
using Game.Entities.Player;
using UnityEngine;

namespace Game.Core.Possession
{
    public class PossessionRouter
    {
        private readonly PlayerEntity playerEntry;

        private IBody currentBody;
        private IControllable movementController;

        public PossessionRouter(PlayerEntity player)
        {
            this.playerEntry = player;
        }

        public void OnStart(Trader initialBody)
        {
            currentBody = initialBody;
            movementController = initialBody;

            initialBody.SetCamera(playerEntry.PlayerCamera.Transform);

            movementController.Possess(ControlFlag.Movement);
            playerEntry.PossessBody(currentBody);
        }

        public void OnTick()
        {
            movementController?.OnTick(ControlFlag.Movement);
        }

        public void OnFixedTick()
        {
            playerEntry.OnFixedUpdate();
            movementController?.OnFixedTick(ControlFlag.Movement);
        }

        public void OnLateTick()
        {
            playerEntry.OnLateUpdate();
            movementController?.OnLateTick(ControlFlag.Movement);
        }

        public void EnterVehicle(IVehicle vehicle)
        {
            if (!vehicle.DriverSeat.TryOccupy(currentBody)) return;

            movementController.Release(ControlFlag.Movement);

            movementController = vehicle;
            movementController.Possess(ControlFlag.Movement);

            if (movementController is IBody body)
            {
                playerEntry.PossessBody(body);
            }

            vehicle.EnableVehicleInputs();
        }

        public void ExitVehicle(IVehicle vehicle)
        {
            if (!vehicle.CanExit) return;

            vehicle.DriverSeat.Vacate(currentBody);

            var exitPos = vehicle.ExitPoint.position;
            currentBody.DetachTo(exitPos + Vector3.up * 0.1f, Quaternion.identity);

            movementController.Release(ControlFlag.Movement);

            if (playerEntry.CurrentBody is IControllable controllable)
            {
                movementController = controllable;
                movementController.Possess(ControlFlag.Movement);
            }
            if (playerEntry.CurrentBody is IBody body)
            {
                currentBody = body;
                playerEntry.PossessBody(currentBody);
            }

            vehicle.DisableVehicleInputs();
        }
    }
}