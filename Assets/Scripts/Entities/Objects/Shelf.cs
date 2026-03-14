using Game.Entities.Items;
using Game.Entities.Pawns;
using Game.Services.Inventory;
using UnityEngine;

namespace Game.Entities.Objects
{
    public class Shelf : InteractableObject, IGridStorageHolder
    {
        [SerializeField] private PlacementGrid placement;

        private GridStorage storage;

        public GridStorage Storage => storage;

        public void Start()
        {
            storage = new(placement);
        }

        public override InteractionResult Interact(Pawn pawn, InteractionMode mode)
        {
            var item = pawn.Hand.Item;

            switch (mode)
            {
                case InteractionMode.Primary:
                    return PrimaryAction(item);
                case InteractionMode.Secondary:
                    return SecondaryAction(item);
                default:
                    return InteractionResult.Failure;
            }
        }

        private InteractionResult PrimaryAction(Item item)
        {
            if (item is IGridStorageHolder holder)
            {
                if (!storage.TryAddTo(holder.Storage)) return InteractionResult.Failure;
            }

            return InteractionResult.Failure;
        }

        private InteractionResult SecondaryAction(Item item)
        {
            if (item is IGridStorageHolder holder)
            {
                if (!(holder.Storage.TryAddTo(storage))) return InteractionResult.Failure;
            }

            return InteractionResult.Failure;
        }
    }
}
