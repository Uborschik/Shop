using Game.Player;
using Game.Services.Storage;
using UnityEngine;

namespace Game.Objects
{
    public class Shelf : MonoBehaviour, IInteractable
    {
        [SerializeField] private Transform grid;

        private Inventory inventory;

        private void Awake()
        {
            inventory = new(grid);
        }

        public void Interact(Pawn pawn)
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

        public void AltInteract(Pawn pawn)
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
