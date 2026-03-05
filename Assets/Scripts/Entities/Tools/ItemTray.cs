using Game.Entities.Items;
using Game.Services.Inventory;
using UnityEngine;

namespace Game.Entities.Tools
{
    public class ItemTray : TraderTool, IContainer
    {
        [SerializeField] private PlacementGrid placement;

        private Container container;

        private void Start()
        {
            container = new(placement.Slots.Count);
        }

        public bool TryPushItem(Item item)
        {
            if (!container.TryGetPushIndex(out var index)) return false;

            item.transform.SetParent(placement.transform);
            item.transform.localPosition = placement.Slots[index];
            item.transform.rotation = placement.transform.rotation;

            container.Push(index, item);
            return true;
        }

        public bool TryPullItem(out Item item)
        {
            if (!container.TryGetPullIndex(out var index))
            {
                item = null;
                return false;
            }

            item = container.Pull(index);
            return true;
        }
    }
}
