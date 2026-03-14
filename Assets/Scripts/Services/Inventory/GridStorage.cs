using Game.Entities;
using Game.Entities.Items;
using UnityEngine;

namespace Game.Services.Inventory
{
    public class GridStorage
    {
        private readonly Container container;
        private readonly PlacementGrid placementGrid;

        public GridStorage(PlacementGrid placement)
        {
            this.placementGrid = placement;
            container = new Container(placement.Slots.Count);
        }

        public bool TryAddItem(Item item)
        {
            if (!container.TryGetPushIndex(out var push)) return false;

            PlaceItemVisuals(item, push);

            container.Push(push, item);

            return true;
        }

        public bool TryAddTo(GridStorage storage)
        {
            if (!storage.TryGetPullIndex(out var pull)) return false;
            if (!TryGetPushIndex(out var push)) return false;

            var item = storage.Pull(pull);

            Push(push, item);
            PlaceItemVisuals(item, push);

            return true;
        }

        public bool TryGetPushIndex(out int index) => container.TryGetPushIndex(out index);

        public bool TryGetPullIndex(out int index) => container.TryGetPullIndex(out index);

        public void Push(int index, Item item) => container.Push(index, item);

        public Item Pull(int index) => container.Pull(index);

        public Item[] GetAll() => container.Inventory;

        private void PlaceItemVisuals(Item item, int index)
        {
            var position = placementGrid.transform.position + placementGrid.Slots[index];
            item.OnPickup(placementGrid.transform, position);
        }
    }
}