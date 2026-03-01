using Game.Items;
using System.Linq;
using UnityEngine;

namespace Game.Services.Storage
{

    public class Inventory : MonoBehaviour
    {
        private Slot[] slots;

        public int Count => slots.Length;

        private void Awake()
        {
            slots = transform.GetComponentsInChildren<Slot>();
        }

        public bool CanPush() => slots.Any(s => s.IsEmpty);
        public bool CanPull() => slots.Any(s => !s.IsEmpty);

        public void PushItem(Item item)
        {
            var slot = slots.FirstOrDefault(s => s.IsEmpty);

            if (slot == null) return;

            slot.Push(item);
        }

        public Item PullItem()
        {
            var slot = slots.FirstOrDefault(s => !s.IsEmpty);

            if (slot == null) return null;

            return slot.Pull();
        }
    }
}