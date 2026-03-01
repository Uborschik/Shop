using Game.Items;
using UnityEngine;

namespace Game.Services.Storage
{
    public class Slot : MonoBehaviour
    {
        private Item item;

        public bool IsEmpty => item == null;

        public void Push(Item item)
        {
            if (!IsEmpty) return;

            item.transform.SetParent(transform);
            item.transform.localPosition = Vector3.zero;
            this.item = item;
        }

        public Item Pull()
        {
            if (IsEmpty) return null;

            item.transform.SetParent(null);
            return item;
        }
    }
}