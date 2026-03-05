using Game.Entities.Tools;
using System;
using UnityEngine;

namespace Game.Entities.Pawns.Player
{
    public class TraderInteraction
    {
        private readonly Func<Transform> getCameraTransform;
        private readonly float rayDistance;

        private TraderTool tool;

        public TraderInteraction(Func<Transform> getCameraTransform, float rayDistance, TraderTool tool)
        {
            this.tool = tool;
            this.getCameraTransform = getCameraTransform;
            this.rayDistance = rayDistance;
        }

        public void OnInteract()
        {
            if (Physics.Raycast(getCameraTransform().position, getCameraTransform().forward, out RaycastHit hit, rayDistance, tool.InteractionMask))
            {
                if (hit.collider.TryGetComponent<IInteractable>(out var pushable))
                {
                    pushable?.Interact(ref tool);
                }
            }
        }

        public void OnAltInteract()
        {
        }
    }
}