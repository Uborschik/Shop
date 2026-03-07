using Game.Entities.Pawns.NPCs;
using Game.Services.InputSystem;
using UnityEngine;

namespace Game.Entities.Objects
{
    public class Cashbox : MonoBehaviour, IInteractable
    {
        private Buyer buyer;

        public void SetBuyer(Buyer buyer)
        {
            this.buyer = buyer;
        }

        public void Interact(IInteractor interactor, InteractionMode mode)
        {
            if (buyer == null) return;

            Debug.Log("[Cashbox] Buyer is here!");
        }
    }
}
