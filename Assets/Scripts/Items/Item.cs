using Game.Objects;
using Game.Player;
using UnityEngine;

namespace Game.Items
{
    public class Item : MonoBehaviour, IInteractable
    {
        public void Interact(Pawn pawn)
        {
            if (!pawn.Inventory.CanPush()) return;

            pawn.Inventory.PushItem(this);
        }
        public void AltInteract(Pawn pawn)
        {
        }

    }
}
