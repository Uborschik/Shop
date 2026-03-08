using Game.Entities.Pawns;
using Game.Entities.Tools;
using Game.Services.InputSystem;
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
            var tool = pawn.ToolHolder.Tool;

            switch (mode)
            {
                case InteractionMode.Primary:
                    return PrimaryAction(tool);
                case InteractionMode.Secondary:
                    return SecondaryAction(tool);
                default:
                    return InteractionResult.Failure;
            }
        }

        private InteractionResult PrimaryAction(Tool tool)
        {
            if (tool is IGridStorageHolder holder)
            {
                if (!storage.TryGetPushIndex(out var index)) return InteractionResult.Failure;
                if (!holder.Storage.TryRemoveItem(out var item)) return InteractionResult.Failure;

                item.PushTo(placement.transform, placement.Slots[index], Quaternion.identity);

                storage.Push(index, item);

                return InteractionResult.Success;
            }

            return InteractionResult.Failure;
        }

        private InteractionResult SecondaryAction(Tool tool)
        {
            if (tool is IGridStorageHolder holder)
            {
                if (!storage.TryRemoveItem(out var item)) return InteractionResult.Failure;
                if (!holder.Storage.TryAddItem(item)) return InteractionResult.Failure;

                return InteractionResult.Success;
            }

            return InteractionResult.Failure;
        }
    }
}
