using UnityEngine;

namespace Game.Entities.Pawns.NPCs
{
    public class Buyer : Pawn
    {
        [SerializeField] private float speed;

        public InteractionResult InteractWith(Transform target)
        {
            if (target.TryGetComponent<IInteractable>(out var interactable))
            {
                return interactable.Interact(this, InteractionMode.Secondary);
            }

            return InteractionResult.Failure;
        }
    }
}