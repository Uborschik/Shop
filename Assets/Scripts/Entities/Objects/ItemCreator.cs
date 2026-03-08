using Game.Entities.Items;
using Game.Entities.Pawns;
using Game.Entities.Tools;
using Game.Services.InputSystem;
using System.Collections;
using UnityEngine;

namespace Game.Entities.Objects
{
    public class ItemCreator : MonoBehaviour, IInteractable
    {
        [SerializeField] private Item[] prefabs;
        [SerializeField] private Transform placement;
        [SerializeField] private float interval;

        private Item createdItem;
        private Coroutine spawnCoroutine;

        private void Start()
        {
            CreateItem();
        }

        public InteractionResult Interact(Pawn pawn, InteractionMode mode)
        {
            var tool = pawn.ToolHolder.Tool;

            switch (mode)
            {
                case InteractionMode.Primary:
                    return PrimaryAction(tool);
                default:
                    return InteractionResult.Failure;
            }

        }

        private InteractionResult PrimaryAction(Tool tool)
        {
            if (createdItem == null) return InteractionResult.Failure;

            if (tool is IGridStorageHolder holder)
            {
                if (holder.Storage.TryAddItem(createdItem))
                {
                    createdItem = null;

                    spawnCoroutine ??= StartCoroutine(SpawnAfterDelay());
                }
            }

            return InteractionResult.Success;
        }

        private void CreateItem()
        {
            if (prefabs == null || prefabs.Length == 0)
            {
                Debug.LogWarning("ItemCreator: Prefabs array is empty!");
                return;
            }

            var randomIndex = Random.Range(0, prefabs.Length);
            createdItem = Instantiate(prefabs[randomIndex], transform);
            createdItem.transform.position = placement.position;
            createdItem.SetActivePhysics(false);
        }

        private IEnumerator SpawnAfterDelay()
        {
            yield return new WaitForSeconds(interval);

            spawnCoroutine = null;
            CreateItem();
        }
    }
}