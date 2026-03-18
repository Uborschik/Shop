//using Game.Entities.Pawns;
//using Game.Entities.Pawns.NPCs;
//using Game.Entities.Pawns.Player;

//namespace Game.Entities.Objects
//{
//    public class Cashbox : InteractableObject, IInteractable
//    {
//        private Buyer buyer;

//        public override InteractionResult Interact(Pawn pawn, InteractionMode mode)
//        {
//            if (pawn == null) return InteractionResult.Failure;
//            if (buyer == null)
//            {
//                if (pawn is Buyer potentialBuyer) buyer = potentialBuyer;

//                return InteractionResult.Running;
//            }
//            else if (pawn is Trader trader)
//            {
//                TryPay(trader);

//                return InteractionResult.Success;
//            }

//            return InteractionResult.Failure;
//        }

//        private void TryPay(Trader trader)
//        {
//            if (buyer.Item is IGridStorageHolder holder)
//            {
//                var items = holder.Storage.GetAll();

//                for (int i = 0; i < items.Length; i++)
//                {
//                }
//            }
//        }
//    }
//}
