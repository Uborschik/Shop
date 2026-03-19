using Game.Core.Possession;
using Game.Entities.Pawns.Player;
using UnityEngine;
using VContainer;

namespace Game.Entities.Interactables
{
    public class VehicleInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField] private MonoBehaviour vehicleComponent;

        private IVehicle vehicle;

        private void Start()
        {
            vehicle = vehicleComponent as IVehicle;

            if (vehicle == null)
                Debug.LogError($"{name}: vehicleComponent не реализует IVehicle!");
        }

        public InteractionResult Interact(InteractionContext context, InteractionMode mode)
        {
            if (vehicle == null) return InteractionResult.Failure;

            if (mode == InteractionMode.Primary)
            {
                return InteractionResult.Success;
            }

            return InteractionResult.Success;
        }
    }
}