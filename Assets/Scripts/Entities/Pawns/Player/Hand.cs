using Game.Entities.Interactables;
using Game.Entities.Items;
using Game.Entities.Items.Tools;
using UnityEngine;

namespace Game.Entities.Pawns.Player
{
    public class Hand : MonoBehaviour
    {

        [SerializeField] private Transform handTransform;
        [SerializeField] private LayerMask defaultMask;

        [field: SerializeField] public float RayDistance { get; protected set; }
        [field: SerializeField] public Item Item { get; private set; }

        public LayerMask InteractionMask { get; private set; }

        private void Awake()
        {
            Item = null;
            InteractionMask = defaultMask;
        }

        public void Use(IInteractable interactable, InteractionContext context, InteractionMode mode) => interactable?.Interact(context, mode);

        public void Pickup(Item item)
        {
            if (Item) DropItem();

            Item = item;
            Item.OnPickup(handTransform);

            if (item is Tool tool)
            {
                InteractionMask = tool.InteractionMask;
            }
        }

        public void DropItem()
        {
            if (!Item) return;

            Item.OnDrop();
            Item = null;
            InteractionMask = defaultMask;
        }
    }
}