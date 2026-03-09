using Game.Entities.Tools;
using UnityEngine;

namespace Game.Entities.Pawns.NPCs
{
    public class Buyer : Pawn
    {
        [SerializeField] private float speed;
        [SerializeField] private Tool tool;
        [SerializeField] private string shoppingList;

        public Tool Tool => tool;
        public string ShoppingList => shoppingList;

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