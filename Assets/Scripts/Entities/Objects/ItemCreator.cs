using Game.Entities.Items;
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

        public void Interact(IInteractor interactor, InteractionMode mode)
        {
            var tool = interactor.ToolHolder.Tool;

            switch (mode)
            {
                case InteractionMode.Primary:
                    PrimaryAction(tool);
                    break;
                case InteractionMode.Secondary:
                    break;
                default:
                    break;
            }

        }

        private void PrimaryAction(TraderTool tool)
        {
            if (createdItem == null) return;

            if (tool is IGridStorageHolder holder)
            {
                if (holder.Storage.TryAddItem(createdItem))
                {
                    createdItem = null;

                    spawnCoroutine ??= StartCoroutine(SpawnAfterDelay());
                }
            }
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