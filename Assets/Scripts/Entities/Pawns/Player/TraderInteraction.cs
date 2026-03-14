using UnityEngine;

namespace Game.Entities.Pawns.Player
{
    public class TraderInteraction
    {
        private readonly Transform cameraTransform;

        private RaycastHit hit;

        public TraderInteraction(TraderCameraConfig cameraConfig)
        {
            cameraTransform = cameraConfig.TraderCamera.transform;
        }

        public void RayCast(Hand hand)
        {
            Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, hand.RayDistance, hand.InteractionMask);
        }

        public void OnInteract(Pawn pawn, InteractionMode mode)
        {
            if (hit.collider != null)
            {
                if (hit.collider.TryGetComponent<IInteractable>(out var interactable))
                {
                    pawn.Hand.Use(interactable, pawn, mode);
                }
            }
        }

        public void DropItem(Hand hand)
        {
            hand.DropItem();
        }
    }
}