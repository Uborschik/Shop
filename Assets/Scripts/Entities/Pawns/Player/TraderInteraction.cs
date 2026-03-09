using Game.Services.InputSystem;
using System;
using UnityEngine;

namespace Game.Entities.Pawns.Player
{
    [Serializable]
    public class TraderInteraction
    {
        private readonly Pawn pawn;
        private readonly Transform cameraTransform;

        public TraderInteraction(Pawn pawn, Transform cameraTransform)
        {
            this.pawn = pawn;
            this.cameraTransform = cameraTransform;
        }

        public void OnInteract(InteractionMode mode)
        {
            var tool = pawn.ToolHolder.Tool;

            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit hit, tool.RayDistance, tool.InteractionMask))
            {
                if (hit.collider.TryGetComponent<IInteractable>(out var interactable))
                {
                    tool.Use(interactable, pawn, mode);
                }
            }
        }
    }
}