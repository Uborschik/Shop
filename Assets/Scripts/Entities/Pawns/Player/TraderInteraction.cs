using Game.Services.InputSystem;
using System;
using UnityEngine;

namespace Game.Entities.Pawns.Player
{
    [Serializable]
    public class TraderInteraction
    {
        private readonly IInteractor interactor;
        private readonly Transform cameraTransform;
        private readonly float rayDistance;

        public TraderInteraction(IInteractor interactor, Transform cameraTransform, float rayDistance)
        {
            this.interactor = interactor;
            this.cameraTransform = cameraTransform;
            this.rayDistance = rayDistance;
        }

        public void OnInteract(InteractionMode mode)
        {
            var mask = interactor.ToolHolder.Tool.InteractionMask;

            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit hit, rayDistance, mask))
            {
                if (hit.collider.TryGetComponent<IInteractable>(out var pushable))
                {
                    pushable?.Interact(interactor, mode);
                }
            }
        }

        public void OnAltInteract()
        {
        }
    }
}