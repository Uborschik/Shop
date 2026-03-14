using Game.Entities.Items;
using Game.Entities.Items.Tools;
using System;
using UnityEngine;

namespace Game.Entities.Pawns.Player
{
    public class Hand : MonoBehaviour
    {
        [SerializeField] private LayerMask defaultMask;

        [field: SerializeField] public float RayDistance { get; protected set; }
        [field: SerializeField] public Item Item { get; private set; }

        public LayerMask InteractionMask { get; private set; }

        private void Awake()
        {
            Item = null;
            InteractionMask = defaultMask;
        }

        public void Use(IInteractable interactable, Pawn pawn, InteractionMode mode) => interactable?.Interact(pawn, mode);

        public void Pickup(Item item)
        {
            if (Item) DropItem();

            Item = item;
            Item.OnPickup(transform);

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