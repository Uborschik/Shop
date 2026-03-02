using Game.Objects;
using System;
using UnityEngine;

namespace Game.Player
{
    public class PawnInteraction
    {
        private readonly Pawn pawn;
        private readonly Func<Transform> getCameraTransform;
        private readonly float rayDistance;

        public PawnInteraction(Pawn pawn, Func<Transform> getCameraTransform, float rayDistance)
        {
            this.pawn = pawn;
            this.getCameraTransform = getCameraTransform;
            this.rayDistance = rayDistance;
        }

        public void OnInteract()
        {
            if (Physics.Raycast(getCameraTransform().position, getCameraTransform().forward, out RaycastHit hit, rayDistance))
            {
                Debug.Log(hit.collider.name);
                var pushable = hit.collider.GetComponent<IInteractable>();
                pushable?.Interact(pawn);
            }
        }

        public void OnAltInteract()
        {
            if (Physics.Raycast(getCameraTransform().position, getCameraTransform().forward, out RaycastHit hit, rayDistance))
            {
                var pullable = hit.collider.GetComponent<IInteractable>();
                pullable?.AltInteract(pawn);
            }
        }
    }
}