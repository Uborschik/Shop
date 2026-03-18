using Game.Core.Controllable;
using Game.Entities.Interactables;
using UnityEngine;

namespace Game.Entities.Pawns.Player
{
    public struct InteractionContext
    {
        public Hand Hand;
        public IBody Body;

        public InteractionContext(Hand hand, IBody body)
        {
            Hand = hand;
            Body = body;
        }
    }

    public class PlayerInteraction
    {
        private Hand hand;
        private IBody body;
        private RaycastHit hit;

        public void AttachTo(Hand hand, IBody body)
        {
            this.hand = hand;
            this.body = body;
        }

        public void UpdateRay(Ray lookRay)
        {
            if (hand == null) return;

            Physics.Raycast(lookRay, out hit, hand.RayDistance, hand.InteractionMask);
        }

        public void OnInteract(InteractionMode mode)
        {
            if (hand == null || hit.collider == null) return;

            if (hit.collider.TryGetComponentInParent<IInteractable>(out var interactable))
            {
                var context = new InteractionContext(hand, body);
                hand.Use(interactable, context, mode);
            }
        }

        public void DropItem()
        {
            if (hand == null) return;

            hand.DropItem();
        }
    }
}