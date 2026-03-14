using Game.Entities.Items;
using UnityEngine;

namespace Game.Entities.Pawns.NPCs
{
    public class Buyer : Pawn
    {
        [SerializeField] private float speed;
        [SerializeField] private Item item;
        [SerializeField] private string shoppingList;

        public Item Item => item;
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