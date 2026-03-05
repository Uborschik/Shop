using Game.Entities.Tools;
using Game.Services.Inventory;
using UnityEngine;

namespace Game.Entities.Objects
{
    public class Shelf : MonoBehaviour, IInteractable
    {
        [SerializeField] private PlacementGrid placement;

        private Container container;

        public void Start()
        {
            container = new(placement.Slots.Count);
        }

        public void Interact(ref TraderTool tool)
        {
            if (tool is IContainer container)
            {
                if (!this.container.TryGetPushIndex(out var index)) return;
                if (!container.TryPullItem(out var item)) return;

                item.transform.SetParent(placement.transform);
                item.transform.localPosition = placement.Slots[index];

                this.container.Push(index, item);
            }
        }
    }
}
