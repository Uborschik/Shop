using Game.Entities.Interactables;
using Game.Entities.Items;
using Game.Entities.Items.Tools;
using System;
using UnityEngine;

namespace Game.Entities.Pawns.Player
{
    [Serializable]
    public class HandConfig
    {
        public Transform Transform;
        public LayerMask DefaultMask;
        public float RayDistance;
    }

    public class Hand
    {
        private readonly Trader trader;
        private readonly Transform handTransform;
        private readonly LayerMask defaultMask;
        private readonly float rayDistance;

        public LayerMask InteractionMask { get; private set; }
        public float RayDistance { get; private set; }
        public Item Item { get; private set; }

        public Hand(Trader trader, HandConfig config)
        {
            this.trader = trader;
            handTransform = config.Transform;
            defaultMask = config.DefaultMask;
            rayDistance = config.RayDistance;

            InteractionMask = defaultMask;
            RayDistance = rayDistance;
            Item = null;
        }

        public void Use(IInteractable interactable, InteractionContext context, InteractionMode mode) => interactable?.Interact(context, mode);

        public void Pickup(Item item)
        {
            if (Item) DropItem(trader.transform.parent);

            Item = item;
            Item.OnPickup(handTransform);

            if (item is Tool tool)
            {
                InteractionMask = tool.InteractionMask;
            }
        }

        public void DropItem(Transform parent)
        {
            if (!Item) return;

            Item.OnDrop(parent);
            Item = null;
            InteractionMask = defaultMask;
        }
    }
}