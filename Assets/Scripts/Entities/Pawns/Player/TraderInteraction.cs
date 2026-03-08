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
            pawn.ToolHolder.Tool.Use(cameraTransform, pawn, mode);
        }
    }
}