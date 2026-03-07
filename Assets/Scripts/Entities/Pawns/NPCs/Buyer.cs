using Game.Entities.Items;
using Game.Services.Inventory;
using UnityEngine;

namespace Game.Entities.Pawns.NPCs
{
    public class Buyer : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private int slotCount;

        private Container container;

        private void Awake()
        {
            container = new(slotCount);
        }

        public bool TryBuy(Transform target)
        {
            if (target.TryGetComponent<IInteractable>(out var i))
            {
                return true;
            }

            return false;
        }

        public bool TryPullItem(out Item item)
        {
            if (container == null || !container.TryGetPullIndex(out var index))
            {
                item = null;
                return false;
            }

            item = container.Pull(index);
            return true;
        }

        public bool TryPushItem(Item item)
        {
            if (!container.TryGetPushIndex(out var index)) return false;

            item.transform.SetParent(transform);
            item.gameObject.SetActive(false);

            container.Push(index, item);
            return true;
        }
    }
}