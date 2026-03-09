using Game.Entities.Items;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Services.Inventory
{
    public class GridStorage
    {
        private readonly Container container;
        private readonly PlacementGrid placement;

        public GridStorage(PlacementGrid placement)
        {
            this.placement = placement;
            container = new Container(placement.Slots.Count);
        }

        public bool TryAddItem(Item item)
        {
            if (!container.TryGetPushIndex(out var index)) return false;

            PlaceItemVisuals(item, index);
            container.Push(index, item);
            return true;
        }

        public bool TryRemoveItem(out Item item)
        {
            if (!container.TryGetPullIndex(out var index))
            {
                item = null;
                return false;
            }

            item = container.Pull(index);
            return true;
        }

        public bool TryGetPushIndex(out int index) => container.TryGetPushIndex(out index);

        public bool TryGetPullIndex(out int index) => container.TryGetPullIndex(out index);

        public void Push(int index, Item item) => container.Push(index, item);

        public Item Pull(int index) => container.Pull(index);

        public Item[] GetAll() => container.Inventory;

        private void PlaceItemVisuals(Item item, int index)
        {
            item.PushTo(placement.transform, placement.Slots[index], Quaternion.identity);
        }
    }
}