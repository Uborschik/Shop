using Game.Entities.Tools;
using Game.Services.InputSystem;
using Game.Services.Inventory;
using System;
using UnityEngine;

namespace Game.Entities.Objects
{
    public class Shelf : MonoBehaviour, IInteractable, IGridStorageHolder
    {
        [SerializeField] private PlacementGrid placement;

        private GridStorage storage;

        public GridStorage Storage => storage;

        public void Start()
        {
            storage = new(placement);
        }

        public void Interact(IInteractor interactor, InteractionMode mode)
        {
            var tool = interactor.ToolHolder.Tool;

            switch (mode)
            {
                case InteractionMode.Primary:
                    PrimaryAction(tool);
                    break;
                case InteractionMode.Secondary:
                    SecondaryAction(tool);
                    break;
                default:
                    break;
            }
        }

        private void PrimaryAction(TraderTool tool)
        {
            if (tool is IGridStorageHolder holder)
            {
                if (!storage.TryGetPushIndex(out var index)) return;
                if (!holder.Storage.TryRemoveItem(out var item)) return;

                item.PushTo(placement.transform, placement.Slots[index], Quaternion.identity);

                storage.Push(index, item);
            }
        }

        private void SecondaryAction(TraderTool tool)
        {
            if (tool is IGridStorageHolder holder)
            {
                if (!storage.TryRemoveItem(out var item)) return;
                if (!holder.Storage.TryAddItem(item)) return;
            }
        }
    }
}
