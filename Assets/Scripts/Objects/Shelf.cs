using Game.Player;
using Game.Services.Storage;
using UnityEngine;

namespace Game.Objects
{
    [RequireComponent(typeof(Inventory))]
    public class Shelf : MonoBehaviour, IInteractable
    {
        private Inventory inventory;

        private void Awake()
        {
            inventory = GetComponent<Inventory>();
        }

        public void Push(Pawn pawn)
        {
            if (pawn.Inventory.CanPull() && inventory.CanPush())
            {
                var item = pawn.Inventory.PullItem();

                if (item != null)
                {
                    inventory.PushItem(item);
                }
            }
        }

        public void Pull(Pawn pawn)
        {
            if (inventory.CanPull() && pawn.Inventory.CanPush())
            {
                var item = inventory.PullItem();

                if (item != null)
                {
                    pawn.Inventory.PushItem(item);
                }
            }
        }
    }
}
