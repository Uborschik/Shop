using Game.Entities.Items;
using Game.Entities.Pawns;
using Game.Entities.Pawns.NPCs;
using Game.Entities.Pawns.Player;
using Game.Services.InputSystem;
using UnityEngine;

namespace Game.Entities.Objects
{
    public class Cashbox : InteractableObject, IInteractable
    {
        private Buyer buyer;

        public void SetBuyer(Buyer buyer)
        {
            this.buyer = buyer;
        }

        public override InteractionResult Interact(Pawn pawn, InteractionMode mode)
        {
            if (pawn == null) return InteractionResult.Failure;
            if (pawn is Buyer buyer)
            {
                this.buyer = buyer; 
                Debug.Log("[Cashbox] Buyer is here!");
            }
            if (pawn is Trader trader)
            {
                Debug.Log("[Cashbox] Trader is here!");
            }


            return InteractionResult.Success;
        }
    }
}
